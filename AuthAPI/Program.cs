using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthAPI.Data;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDB") 
    ?? "mongodb://mongodb:27017";
var mongoDatabaseName = builder.Configuration["MongoDB:DatabaseName"] ?? "ProductApp";

// MongoDB Beállítása és AppDbContext regisztrálása
var mongoClient = new MongoClient(mongoConnectionString);
builder.Services.AddSingleton<IMongoClient>(mongoClient);
builder.Services.AddScoped(sp => new AppDbContext(
    sp.GetRequiredService<IMongoClient>(),
    mongoDatabaseName
));

// JWT Authentication beállítása és kulcs definiálása a token generáláshoz 
var key = Encoding.ASCII.GetBytes("ThisIsAMySuperSecretKeyForJWTTokenGenerationsInDotNet8");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// MongoDB inicializálása és alapértelmezett felhasználók létrehozása, ha még nem léteznek
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.InitializeAsync();

    // Seed default users
    var existingAdmin = await db.Users.Find(u => u.Username == "admin").FirstOrDefaultAsync();
    if (existingAdmin == null)
    {
        await db.Users.InsertOneAsync(new User { Username = "admin", Password = "admin", Role = "Admin" });
    }

    var existingUser = await db.Users.Find(u => u.Username == "user").FirstOrDefaultAsync();
    if (existingUser == null)
    {
        await db.Users.InsertOneAsync(new User { Username = "user", Password = "user", Role = "User" });
    }
}

// CORS beállítása, hogy minden origin engedélyezve legyen a fejlesztés során 
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed(origin => true).AllowCredentials());

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Bejelentkezési endpoint, amely ellenőrzi a felhasználó hitelesítő adatait és
// JWT tokent generál, ha helyesek a hitelesítő adatok 
app.MapPost("/login", async (LoginRequest request, AppDbContext db) =>
{
    var user = await db.Users.Find(x => x.Username == request.Username && x.Password == request.Password).FirstOrDefaultAsync();
    if (user == null)
        return Results.Unauthorized();

    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role ?? "User")
        }),
        Expires = DateTime.UtcNow.AddHours(2),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return Results.Ok(new AuthResponse { Token = tokenHandler.WriteToken(token), Role = user.Role });
});

app.Run();
