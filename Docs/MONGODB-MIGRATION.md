# ProductApp - MongoDB Migráció

## 📋 Végzett Módosítások

### 1. **Adatbázis Engine váltás**
- ❌ **Eltávolítva**: Microsoft SQL Server + Entity Framework Core
- ✅ **Hozzáadva**: MongoDB + MongoDB.Driver

### 2. **NuGet Csomagok**

#### WebAPI
```xml
<!-- Eltávolítva -->
- Microsoft.EntityFrameworkCore (v9.0.10)
- Microsoft.EntityFrameworkCore.Proxies (v9.0.10)
- Microsoft.EntityFrameworkCore.SqlServer (v9.0.10)
- Microsoft.EntityFrameworkCore.Tools (v9.0.10)

<!-- Hozzáadva -->
+ MongoDB.Driver (v2.26.0)
```

#### AuthAPI
```xml
<!-- Eltávolítva -->
- Microsoft.EntityFrameworkCore.Proxies (v8.0.0)
- Microsoft.EntityFrameworkCore.SqlServer (v8.0.0)

<!-- Hozzáadva -->
+ MongoDB.Driver (v2.26.0)
```

#### Shared
```xml
<!-- Hozzáadva -->
+ MongoDB.Bson (v2.26.0)
```

### 3. **Modell Módosítások**

#### Product.cs
```csharp
// Volt
public int ProductID { get; set; }
public int CategoryID { get; set; }

// Lett
[BsonId]
[BsonRepresentation(BsonType.ObjectId)]
public string? Id { get; set; }

public string CategoryId { get; set; }
```

#### Category.cs
```csharp
// Volt
public int CategoryID { get; set; }

// Lett
[BsonId]
[BsonRepresentation(BsonType.ObjectId)]
public string? Id { get; set; }
```

#### MyUser.cs
```csharp
// Volt
public int UserID { get; set; }

// Lett
[BsonId]
[BsonRepresentation(BsonType.ObjectId)]
public string? Id { get; set; }
```

### 4. **AppDbContext módosítások**

#### WebAPI/AuthAPI - volt (EF Core)
```csharp
public class AppDbContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
}
```

#### WebAPI/AuthAPI - lett (MongoDB)
```csharp
public class AppDbContext
{
    public IMongoCollection<Category> Categories => _database.GetCollection<Category>("Categories");
    public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
}
```

### 5. **Service Réteg Módosítások**

#### CategoryService.cs
```csharp
// Volt
context.Categories.Where(c => c.CategoryID == id)

// Lett
context.Categories.Find(c => c.Id == id.ToString())
```

#### ProductService.cs
```csharp
// Volt
context.Products.Find(id)

// Lett
context.Products.Find(p => p.Id == id.ToString())
```

#### DbInit.cs
```csharp
// Volt
context.Database.ExecuteSqlRaw("DBCC CHECKIDENT...")

// Lett (nincs szükség - MongoDB automatikusan generálja az ObjectId-kat)
```

### 6. **Program.cs módosítások**

#### WebAPI/AuthAPI
```csharp
// Volt
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Lett
var mongoClient = new MongoClient(mongoConnectionString);
builder.Services.AddSingleton<IMongoClient>(mongoClient);
builder.Services.AddScoped(sp => new AppDbContext(
    sp.GetRequiredService<IMongoClient>(),
    mongoDatabaseName
));
```

### 7. **Docker Compose módosítások**

#### Volt
```yaml
services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      SA_PASSWORD: "ProductApp@2024"
    ports:
      - "1433:1433"
```

#### Lett
```yaml
services:
  mongodb:
    image: mongo:latest
    ports:
      - "27017:27017"
```

### 8. **appsettings.json módosítások**

#### Volt
```json
{
  "ConnectionStrings": {
    "dbConnect": "Server=.\\sqlexpress;Database=ProductApp;..."
  }
}
```

#### Lett
```json
{
  "ConnectionStrings": {
    "MongoDB": "mongodb://mongodb:27017"
  },
  "MongoDB": {
    "DatabaseName": "ProductApp"
  }
}
```

---

## 🔄 Az Adatok Migrálása

### Régi adatbázis (SQL Server) exportálása
```sql
-- SQL Server Management Studio-ban
SELECT * FROM Products
SELECT * FROM Categories
SELECT * FROM MyUsers
```

### Új adatbázisba (MongoDB) importálása

Ha van meglévő adat az SQL Server-en, az alábbi módszerrel importálhatod:

1. **CSV exportálás az SQL Server-ből**
   ```sql
   -- SQL Server Management Studio
   -- Results to File (Ctrl+Shift+F)
   ```

2. **MongoDB Compass-ban importálás**
   - Letöltés: https://www.mongodb.com/products/tools/compass
   - CSV importálás a gyűjteménybe

3. **Vagy Python scripttel importálás**
   ```python
   import csv
   from pymongo import MongoClient
   
   client = MongoClient('mongodb://localhost:27017')
   db = client['ProductApp']
   
   with open('products.csv') as f:
       reader = csv.DictReader(f)
       db.Products.insert_many(list(reader))
   ```

---

## ✅ Tesztlista

- [ ] Projekt sikeresen lefordítva
- [ ] Docker image-ek sikeresen buildelve
- [ ] MongoDB container elindul (`docker-compose up -d`)
- [ ] WebAPI elérhető (http://localhost:7211/swagger/ui)
- [ ] AuthAPI elérhető (http://localhost:7297/swagger/ui)
- [ ] Bejelentkezés működik (admin/admin)
- [ ] Kategóriák listázása működik
- [ ] Termékek listázása működik
- [ ] Kategória létrehozása működik
- [ ] Termék létrehozása működik
- [ ] Szerkesztés működik
- [ ] Törlés működik

---

## 📚 MongoDB Hasznos Parancsok

### MongoDB Compass-sal (GUI)
```
1. Letöltés: https://www.mongodb.com/products/tools/compass
2. Connection: mongodb://localhost:27017
3. Database: ProductApp
4. Collections: Products, Categories, MyUsers
```

### MongoDB CLI-vel
```bash
# MongoDB shell elindítása
mongosh

# Adatbázis kiválasztása
use ProductApp

# Kollekcióban lévő dokumentumok
db.Products.find()
db.Categories.find()
db.MyUsers.find()

# Dokumentum szűrése
db.Products.find({ PName: "iPhone" })

# Szám lekérdezése
db.Products.countDocuments()

# Kollekcióból törlés
db.Products.deleteMany({})
```

---

## 🔍 Hibakeresés

### "Connection refused" hiba
```bash
# Ellenőrizze, hogy MongoDB fut-e
docker-compose ps

# Logok megtekintése
docker-compose logs mongodb
```

### "Kollekcióhoz nem lehet hozzáférni"
```bash
# Ellenőrizze az AppDbContext inicializálásban:
await dbContext.InitializeAsync();  // Ez létrehozza a gyűjteményeket
```

### Entity Framework referenciadatok
Az egyik referenciadatot (Category.Products, Product.Category) előfordulhat, hogy nem működik helyesen MongoDB-ben. 
Ezt manuális join-nal lehet megoldani a Service rétegben.

---

## 🚀 Indítás

```bash
# Teljes reset
docker-compose down -v
docker-compose up -d --build

# Logok megtekintése
docker-compose logs -f

# MongoDB eszközökhöz
docker exec -it productapp-mongodb mongosh
```

---

## 📖 Referenciák

- [MongoDB Documentation](https://docs.mongodb.com/)
- [MongoDB Driver for .NET](https://github.com/mongodb/mongo-csharp-driver)
- [MongoDB Compass](https://www.mongodb.com/products/tools/compass)
