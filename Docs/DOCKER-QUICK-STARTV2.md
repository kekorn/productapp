# ProductApp - Docker & Minikube Quick Start Guide

## 🚀 Gyors Indítás Dockerrel (5 perc)

### 1. Előfeltételek
- ✅ Docker Desktop telepítve és futtatva
- ✅ Git klón: `https://github.com/Kekorn/ProductApp`

### 2. Indítás

```powershell
# Nyissa meg a PowerShell-t és navigáljon a project könyvtárba
cd C:\Users\kekor\source\repos\Kekorn\ProductApp

# Indítsa az összes service-t
docker-compose up -d --build
```

### 3. Ellenőrzés

```powershell
# Ellenőrizze a container állapotát
docker-compose ps

# Nyissa meg a böngészőben
# BlazorUI: http://localhost:8080
```

---

## ☸️ Gyors Indítás Minikube-bal (Kubernetes)

### 1. Előfeltételek
- ✅ Docker Desktop telepítve és futtatva
- ✅ Minikube és kubectl telepítve
- ✅ Kubernetes manifest fájlok (pl. a `k8s/` mappában)

### 2. Indítás

```powershell
# Minikube indítása
minikube start

# 1. Állítsd be a környezeti változókat a minikube-hoz
& minikube -p minikube docker-env | Invoke-Expression

# 2. Buildeld le az applikációkat a Minikube Docker démonába
docker-compose build

# 3. Futtasd az alkalmazást az újonnan elkészített k8s/ mappával
kubectl apply -f k8s/
```

### 3. Ellenőrzés

```powershell
# Podok állapotának lekérdezése
kubectl get pods

# Service-ek lekérdezése
kubectl get svc

# BlazorUI megnyitása a böngészőben Minikube segítségével
minikube service blazorui-service
```

---

## 📋 Service-ek

| Service | Port | URL Dockerben |
|---------|------|---------------|
| **BlazorUI** (Frontend) | 8080 | http://localhost:8080 |
| **WebAPI** (Termékek) | 7211 | http://localhost:7211 |
| **AuthAPI** (Autentikáció) | 7297 | http://localhost:7297 |
| **MongoDB** | 27017 | localhost:27017 |

---

## 🔑 Bejelentkezési adatok

- **Username**: `admin` / **Password**: `admin` (Admin)
- **Username**: `user` / **Password**: `user` (User)

---

## 📊 Logok megtekintése

### Docker
```powershell
# Összes service logja
docker-compose logs -f

# Egy service logja
docker-compose logs -f blazorui
```

### Kubernetes (Minikube)
```powershell
# Egy adott pod logjainak megtekintése
kubectl logs -f <pod-nev>
```

---

## 🛑 Leállítás

### Docker
```powershell
# Leállítás (adatok megmaradnak)
docker-compose down

# Teljes törlés (adatok is)
docker-compose down -v
```

### Minikube
```powershell
# Erőforrások törlése a Kubernetes fürtből
kubectl delete -f k8s/

# Minikube leállítása
minikube stop
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
mongodb://mongodb:27017
```

**Kubernetes-ben (példa):**
```
mongodb://mongodb-service:27017
```

**Helyi fejlesztés:**
```
mongodb://localhost:27017
```

---

## ❓ Hibaelhárítás

### "Address already in use" (Docker)
```yaml
# Változtassa meg a portot a docker-compose.yml-ben
ports:
  - "8081:8080"  # Helyett 8080, használjon 8081-et
```

### "Connection refused"
```powershell
# Ellenőrizze, hogy a MongoDB elindult Dockerben:
docker-compose logs mongodb

# Vagy Kubernetes esetén nézze meg a pod állapotát:
kubectl get pods

# Várjon ~10 másodpercet a MongoDB inicializálásához
```

### "Permission denied"
```powershell
# Windows-on PowerShell-t futtassa rendszergazdaként
```

---

## 📚 További információ

- **Részletes Docker útmutató**: Lásd `DOCKER.md`
- **DevOps deployment**: Lásd `DEVOPS.md`
- **.NET Docker best practices**: Lásd `DOCKER-DOTNET.md`
- **Minikube útmutató**: Lásd `Minikube.md`

---

## 🎯 Közös parancsok

### Docker
```powershell
# Rebuild egy service-t
docker-compose build webapi; docker-compose up -d

# Teljes reset
docker-compose down -v; docker-compose up -d --build

# Egy container-be belépni
docker exec -it productapp-webapi /bin/sh
```

### Kubernetes
```powershell
# Belépés egy pod interaktív shelljébe
kubectl exec -it <pod-nev> -- /bin/sh

# Port továbbítása helyi gépre (pl. teszteléshez)
kubectl port-forward svc/webapi-service 7211:80
```

---

## ✅ Próbamodell adatok

Az AuthAPI automatikusan létrehozza az alábbi felhasználókat:

| Username | Password | Role |
|----------|----------|------|
| admin | admin | Admin |
| user | user | User |

---

**Készen áll?** Nyomja meg a `docker-compose up -d --build` vagy a `minikube start` parancsot! 🚀
