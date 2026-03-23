# ProductApp - Docker Quick Start Guide

## 🚀 Gyors Indítás (5 perc)

### 1. Előfeltételek
- ✅ Docker Desktop telepítve és futtatva
- ✅ Git klón: `https://github.com/Kekorn/ProductApp`

### 2. Indítás

```bash
# Nyissa meg a PowerShell-t és navigáljon a project könyvtárba
cd C:\Users\kekor\source\repos\Kekorn\ProductApp

# Indítsa az összes service-t
docker-compose up -d --build
```

### 3. Ellenőrzés

```bash
# Ellenőrizze a container állapotát
docker-compose ps

# Nyissa meg a böngészőben
# BlazorUI: http://localhost:8080
```

---

## 📋 Service-ek

| Service | Port | URL |
|---------|------|-----|
| **BlazorUI** (Frontend) | 8080 | http://localhost:8080 |
| **WebAPI** (Termékek) | 7211 | http://localhost:7211 |
| **AuthAPI** (Autentikáció) | 7297 | http://localhost:7297 |
| **SQL Server** | 1433 | localhost:1433 |

---

## 🔑 Bejelentkezési adatok

- **Username**: `admin` / **Password**: `admin` (Admin)
- **Username**: `user` / **Password**: `user` (User)

---

## 📊 Logok megtekintése

```bash
# Összes service loga
docker-compose logs -f

# Egy service loga
docker-compose logs -f blazorui
docker-compose logs -f webapi
docker-compose logs -f authapi
```

---

## 🛑 Leállítás

```bash
# Leállítás (adatok megmaradnak)
docker-compose down

# Teljes törlés (adatok is)
docker-compose down -v
```

---

## 🛠️ Fejlesztés

### Docker-ben futó alkalmazás szerkesztése

1. Szerkessze a fájlokat az IDE-ben
2. Fordítsa újra: `docker-compose up -d --build`
3. Frissítse a böngészőt

### Adatbázis URL-ek

**Docker-ben:**
```
sqlserver:1433
User: sa
Password: ProductApp@2024
```

**Helyi fejlesztés:**
```
.\sqlexpress
Trusted_Connection: True
```

---

## ❓ Hibaelhárítás

### "Address already in use"
```bash
# Változtassa meg a portot a docker-compose.yml-ben
ports:
  - "8081:8080"  # Helyett 8080, használjon 8081-et
```

### "Connection refused"
```bash
# Ellenőrizze, hogy az SQL Server elindult
docker-compose logs sqlserver

# Várjon ~30 másodpercet az SQL Server inicializálásához
```

### "Permission denied"
```bash
# Windows-on PowerShell-t futtassa rendszergazdaként
```

---

## 📚 További információ

- **Részletes Docker útmutató**: Lásd `DOCKER.md`
- **DevOps deployment**: Lásd `DEVOPS.md`
- **.NET Docker best practices**: Lásd `DOCKER-DOTNET.md`

---

## 🎯 Közös parancsok

```bash
# Rebuild egy service-t
docker-compose build webapi && docker-compose up -d

# Teljes reset
docker-compose down -v && docker-compose up -d --build

# Egy container-be belépni
docker exec -it productapp-webapi /bin/sh

# Container kimenete megtekintése
docker-compose logs --tail 100 webapi
```

---

## ✅ Próbamodell adatok

Az AuthAPI automatikusan létrehozza az alábbi felhasználókat:

| Username | Password | Role |
|----------|----------|------|
| admin | admin | Admin |
| user | user | User |

---

**Készen áll?** Nyomja meg a `docker-compose up -d --build` parancsot! 🚀
