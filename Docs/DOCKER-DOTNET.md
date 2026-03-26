# Docker Build Best Practices

## Multi-stage Dockerfile szerkezet

Az összes projekthez multi-stage buildeket használunk:

```dockerfile
# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "AppName.dll"]
```

**Előnyei:**
- Kisebb végső Docker image
- Jobban támogatott biztonsági gyakorlatok
- Gyorsabb fejlesztés-telepítés ciklus

## Docker Compose environment változók

A docker-compose.yml a következő modelleket használja:

```yaml
services:
  myservice:
    environment:
      ASPNETCORE_ENVIRONMENT: "Production"
      ConnectionStrings__dbConnect: "..."
      ApiBaseUrl: "http://other-service:8080/"
```

## Local Development

Helyi fejlesztéshez továbbra is Visual Studio-ban futtathat:

```bash
# Visual Studio-ban F5 megnyomása
# vagy

dotnet run --project BlazorUI/BlazorUI.csproj
```

## Docker vs. Local Development

| Aspektus | Local | Docker |
|----------|-------|--------|
| Port | 7211, 7297, etc. | 8080 |
| Connection String | `mongodb://localhost:27017` | `mongodb://mongodb:27017` |
| API Base URL | `https://localhost:7211` | `http://webapi:8080` |
| Build sebesség | Gyorsabb | Lassabb (első ízben) |
| Debugging | Visual Studio debugger | Docker logs |

## .NET 8 specifikus Docker jó gyakorlatok

### 1. Health Checks

```csharp
// Program.cs
builder.Services.AddHealthChecks()
    .AddDbContextCheck<AppDbContext>();

var app = builder.Build();
app.MapHealthChecks("/health");
```

### 2. Logging

```csharp
// Program.cs
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
```

### 3. HTTPS és tanúsítványok

Docker-ben az HTTPS setuphoz:

```bash
# Fejlesztési tanúsítványok az adattárolóban
docker run -it -v ~/.aspnet/https:/root/.aspnet/https myapp
```

## Teljesítmény optimalizálása

### 1. Layer caching

```dockerfile
# Az első másolás előtt, mert az gyakran változik
COPY ["BlazorUI/BlazorUI.csproj", "BlazorUI/"]
RUN dotnet restore "BlazorUI/BlazorUI.csproj"

# Csak ezután az egész
COPY . .
```

### 2. Image méret csökkentése

```bash
# Ellenőrizze az image méretet
docker images | grep productapp

# Multi-stage build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
...
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
# Csak a final stage mérete számít!
```

## Debugging Docker-ben

### 1. Interaktív shell

```bash
docker exec -it productapp-webapi /bin/sh
```

### 2. Logok

```bash
docker logs --follow productapp-webapi
docker logs --tail 50 productapp-webapi
```

### 3. Inspect

```bash
docker inspect productapp-webapi
```

## Biztonsági ajánlások

### 1. Titkos kulcsok

Ne hardcode-olja a jelszavakat:

```bash
# Használjon environment변수okat
docker-compose.override.yml (gitignore-ban)
```

### 2. Non-root user

```dockerfile
RUN useradd -m appuser
USER appuser
```

### 3. Read-only file system

```yaml
services:
  myapp:
    read_only: true
    tmpfs: /tmp
```

## Resource limits

```yaml
services:
  webapi:
    deploy:
      resources:
        limits:
          cpus: '1'
          memory: 1G
        reservations:
          cpus: '0.5'
          memory: 512M
```

## Network Policies

```yaml
networks:
  productapp-network:
    driver: bridge
    driver_opts:
      com.docker.network.bridge.name: br-productapp
```

## Troubleshooting

### Problem: "Connection refused"

```bash
# Ellenőrizze, hogy az összes service fut-e
docker-compose ps

# Ellenőrizze a network-öt
docker network inspect productapp-network
```

### Problem: "Port already in use"

```bash
# Keresse meg a használó folyamatot
lsof -i :8080

# Vagy módosítsa a docker-compose.yml-ben a portot
```

### Problem: "Service can't reach database"

```bash
# Ellenőrizze a MongoDB health check-et
docker logs productapp-mongodb

# Az authapi várjon a mongodb health check-re
```

## Database Migration Docker-ben

A migration az AuthAPI Program.cs-ben automatikusan fut:

```csharp
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    // Seed data...
}
```

## Continuous Integration

### Azure Pipelines

```yaml
trigger:
  - main

pool:
  vmImage: 'ubuntu-latest'

steps:
- task: Docker@2
  inputs:
    command: 'build'
    Dockerfile: 'WebAPI/Dockerfile'
    tags: 'latest'
```

## Referenciák

- [Microsoft .NET Docker Images](https://hub.docker.com/_/microsoft-dotnet)
- [Docker .NET Guide](https://docs.docker.com/language/dotnet/)
- [ASP.NET Core with Docker](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/docker/)
