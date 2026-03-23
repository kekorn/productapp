using MongoDB.Driver;
using WebAPI.Data;
using WebAPI.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDB") 
    ?? "mongodb://mongodb:27017";
var mongoDatabaseName = builder.Configuration["MongoDB:DatabaseName"] ?? "ProductApp";

// Configure MongoDB
var mongoClient = new MongoClient(mongoConnectionString);
builder.Services.AddSingleton<IMongoClient>(mongoClient);
builder.Services.AddScoped(sp => new AppDbContext(
    sp.GetRequiredService<IMongoClient>(),
    mongoDatabaseName
));

builder.Services.AddScoped<DbInit>();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Initialize MongoDB collections
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await dbContext.InitializeAsync();
    
    var dbInit = scope.ServiceProvider.GetRequiredService<DbInit>();
    await dbInit.InitialAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
