# Docker Containerizáció - ProductApp

## Áttekintés

Az alkalmazás teljes Docker containerizációja. A megoldás a következő komponenseket tartalmazza:

- **BlazorUI**: Blazor webalkalmás (port: 8080)
- **WebAPI**: Termék API (port: 7211)
- **AuthAPI**: Autentikációs API (port: 7297)
- **SQL Server**: Adatbázis (port: 1433)

## Előfeltételek

- Docker Desktop telepítve és futtatva
- Docker Compose telepítve

## Indítás

### A teljes alkalmazás indítása:

```bash
docker-compose up -d
```

### Csak az újraépítéssel indítás:

```bash
docker-compose up -d --build
```

### Logok megtekintése:

```bash
# Összes service loga
docker-compose logs -f

# Egy adott service loga
docker-compose logs -f blazorui
docker-compose logs -f webapi
docker-compose logs -f authapi
docker-compose logs -f sqlserver
```

## Leállítás

```bash
docker-compose down
```

### A volume-ok törlésével leállítás:

```bash
docker-compose down -v
```

## Szolgáltatások elérése

### BlazorUI (Frontend)
- **URL**: http://localhost:8080
- **Intern**: http://blazorui:8080

### WebAPI
- **URL**: http://localhost:7211
- **Swagger**: http://localhost:7211/swagger/ui
- **Intern**: http://webapi:8080

### AuthAPI
- **URL**: http://localhost:7297
- **Swagger**: http://localhost:7297/swagger/ui
- **Intern**: http://authapi:8080

### SQL Server
- **Host**: localhost
- **Port**: 1433
- **User**: sa
- **Password**: ProductApp@2024
- **Database**: ProductApp

## Environment változók

### BlazorUI
```
ASPNETCORE_ENVIRONMENT=Production
ApiBaseUrl=http://webapi:8080/api/
AuthApiUrl=http://authapi:8080
```

### WebAPI & AuthAPI
```
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__dbConnect=Server=sqlserver;Database=ProductApp;User Id=sa;Password=ProductApp@2024;Encrypt=False;TrustServerCertificate=True
```

## Dockerfile-ok

Minden projekt multi-stage buildeket használ:
1. **Build stage**: Az SDK alapján fordítja a projektet
2. **Publish stage**: A fordított projektet publikálja
3. **Runtime stage**: Csak a runtime-ot tartalmazza

## Hálózatkezelés

Az összes service a `productapp-network` Docker hálózaton kommunikál, amely lehetővé teszi a service nevek alapján történő feloldást.

## Adatperzisztencia

Az SQL Server adatai a `sqlserver_data` Docker volume-ban tárolódnak, amely megmarad a container leállítása után.

## Fejlesztés

Helyi fejlesztéshez továbbra is használhatja a közvetlenül futtatott alkalmazásokat. Az `appsettings.json` fájlok Docker container üzemben dolgoznak, de a helyi fejlesztési beállítások módosítása nélkül.

## Hibaelhárítás

### Az alkalmazás nem indul
1. Ellenőrizze, hogy az összes port elérhető
2. Tekintse meg a logokat: `docker-compose logs`
3. Ellenőrizze, hogy a Docker démon fut

### Az adatbázis nem kapcsolódik
1. Várjon néhány másodpercet az SQL Server indulására
2. Ellenőrizze a connection stringet
3. Tekintse meg az SQL Server logokat: `docker-compose logs sqlserver`

### Port ütközés
Módosítsa a `docker-compose.yml` fájlban a port mappings értékeit a `ports` szekció alatt.
