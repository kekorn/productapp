# Docker & DevOps - Telepítési Útmutató

## Quick Start

### 1. Docker Desktop telepítése
- [Windows](https://docs.docker.com/desktop/install/windows-install/)
- [Mac](https://docs.docker.com/desktop/install/mac-install/)
- [Linux](https://docs.docker.com/desktop/install/linux-install/)

### 2. Alkalmazás indítása

```bash
cd C:\Users\kekor\source\repos\Kekorn\ProductApp
docker-compose up -d --build
```

### 3. Ellenőrzés

```bash
# Container-ek állapota
docker-compose ps

# Alkalmazás elérhető
http://localhost:8080
```

## Részletes Telepítés

### DevelopmentEnvironemnt

1. **Klón és belépés**
```bash
git clone https://github.com/Kekorn/ProductApp.git
cd ProductApp
```

2. **Build & Start**
```bash
docker-compose up -d --build
```

3. **Adatbázis inicializálása**
Az AuthAPI automatikusan inicializálja az adatbázist:
- Username: `admin` / Password: `admin` (Admin role)
- Username: `user` / Password: `user` (User role)

### Production Deployment

#### Azure Container Registry-re való push

```bash
# Bejelentkezés az ACR-be
az acr login --name <registry-name>

# Tag a képekhez
docker tag productapp-webapi <registry-name>.azurecr.io/webapi:latest
docker tag productapp-authapi <registry-name>.azurecr.io/authapi:latest
docker tag productapp-blazorui <registry-name>.azurecr.io/blazorui:latest

# Push az ACR-be
docker push <registry-name>.azurecr.io/webapi:latest
docker push <registry-name>.azurecr.io/authapi:latest
docker push <registry-name>.azurecr.io/blazorui:latest
```

#### Azure Container Instances-ben való futtatás

```bash
# WebAPI
az container create \
  --resource-group <rg-name> \
  --name productapp-webapi \
  --image <registry-name>.azurecr.io/webapi:latest \
  --cpu 1 --memory 1 \
  --registry-login-server <registry-name>.azurecr.io \
  --registry-username <username> \
  --registry-password <password> \
  --environment-variables \
    ASPNETCORE_ENVIRONMENT=Production \
    ConnectionStrings__dbConnect="mongodb://<db-server>:27017" \
  --ports 8080

# Hasonlóan az AuthAPI és BlazorUI számára
```

#### Docker Swarm-ben való telepítés

```bash
# Initialize Swarm
docker swarm init

# Stack deploy
docker stack deploy -c docker-compose.yml productapp
```

#### Kubernetes-ben való telepítés

1. **kubectl telepítése**
```bash
# Windows
choco install kubernetes-cli

# Vagy manual: https://kubernetes.io/docs/tasks/tools/
```

2. **Kubernetes manifest készítése** (k8s-deployment.yaml)
```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: productapp-webapi
spec:
  replicas: 2
  selector:
    matchLabels:
      app: webapi
  template:
    metadata:
      labels:
        app: webapi
    spec:
      containers:
      - name: webapi
        image: <registry>.azurecr.io/webapi:latest
        ports:
        - containerPort: 8080
        env:
        - name: ConnectionStrings__dbConnect
          value: "mongodb://mongodb:27017"
---
apiVersion: v1
kind: Service
metadata:
  name: webapi
spec:
  selector:
    app: webapi
  ports:
  - protocol: TCP
    port: 8080
    targetPort: 8080
  type: LoadBalancer
```

3. **Deploy**
```bash
kubectl apply -f k8s-deployment.yaml
```

## Monitorozás

### Container logok
```bash
# Valós idejű logok
docker-compose logs -f

# Egy service loga
docker-compose logs -f webapi
```

### Container statisztikák
```bash
docker stats
```

### Health check
```bash
# Container állapota
docker-compose ps

# Specifikus container ellenőrzése
docker exec productapp-webapi curl http://localhost:8080/health
```

## Cleanup

```bash
# Leállítás és törlés
docker-compose down -v

# Összes nem használt docker resource törlése
docker system prune -a
```

## CI/CD Integráció

### GitHub Actions Example

```yaml
name: Docker Build & Push

on:
  push:
    branches: [master]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      
      - name: Build and push WebAPI
        run: |
          docker build -f WebAPI/Dockerfile -t ${{ secrets.ACR_LOGIN_SERVER }}/webapi:${{ github.sha }} .
          docker push ${{ secrets.ACR_LOGIN_SERVER }}/webapi:${{ github.sha }}
```

## Referenciák

- [Docker Dokumentáció](https://docs.docker.com/)
- [Docker Compose Dokumentáció](https://docs.docker.com/compose/)
- [Azure Container Registry](https://docs.microsoft.com/en-us/azure/container-registry/)
- [Kubernetes](https://kubernetes.io/)
