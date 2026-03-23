# Migráció: MyUser → User átnevezés

## Dátum: 2026-03-23

### Változások:

#### 1. Model átnevezés
- `Shared\Models\MyUser.cs` → `Shared\Models\User.cs`
- Osztálynév: `MyUser` → `User`

#### 2. DTO átnevezés
- `Shared\Dto\MyUserDto.cs` → `Shared\Dto\UserDto.cs`
- Osztálynév: `MyUserDto` → `UserDto`

#### 3. MongoDB Kollekcióadatok
- MongoDB kollekcióa: `MyUsers` → `Users`

#### 4. AuthAPI frissítések
- `AuthAPI\Data\AppDbContext.cs`:
  - Property: `MyUsers` → `Users`
  - Collection name: `"MyUsers"` → `"Users"`
  - Model type: `MyUser` → `User`

- `AuthAPI\Program.cs`:
  - Seed data frissítése: új `User` objektumok
  - Login endpoint: `db.Users` helyett `db.MyUsers`

### Adatmigrációs lépések (MongoDB):

Ha már van adatod a `MyUsers` kollekcióban, az alábbi MongoDB parancsokat futtasd:

```javascript
// MongoDB CLI-ben vagy MongoDB Compass-ban
use ProductApp;

// Kollekcióa átnevezése (ha még nem létezik Users)
db.MyUsers.renameCollection("Users");

// Vagy ha manuálisan szeretnéd:
// db.MyUsers.aggregate([{$out: "Users"}])
// db.MyUsers.drop()
```

### Előfigyelmeztetés:
- Az adatok megmaradnak az átnevezés után
- A `_id` mezők nem változnak
- Nincs szükség a Users kollekcióban sema módosításra

### Ellenőrzés:
```bash
# Indítás után ellenőrizd, hogy az új Users kollekcióban vannak-e az adatok
docker-compose up -d --build
```

