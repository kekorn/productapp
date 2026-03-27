# ProductApp - Docker & Minikube Quick Start Guide

## 🚀 Gyors Indítás Dockerrel (5 perc)

### 1. Előfeltételek
- ✅ Docker Desktop telepítve és futtatva
- ✅ Git klón: `https://github.com/Kekorn/ProductApp`

### 2. Indítás

```
# Nyissa meg a PowerShell-t és navigáljon a project könyvtárba
cd C:\Users\kekor\source\repos\Kekorn\ProductApp

# Indítsa az összes service-t
docker-compose up -d --build
```

### 3. Ellenőrzés

```
# Ellenőrizze a container állapotát
docker-compose ps

# Nyissa meg a böngészőben
# BlazorUI: http://localhost:8080
```

### 4. Leállítás
```
# Leállítás (adatok megmaradnak)
docker-compose down

# Teljes törlés (adatok is)
docker-compose down -v
```
---

## ☸️ Gyors Indítás Minikube-bal (Kubernetes)

### 1. Előfeltételek
- ✅ Docker Desktop telepítve és futtatva
- ✅ Minikube és kubectl telepítve
- ✅ Kubernetes manifest fájlok (pl. a `k8s/` mappában)

### 2. Indítás

```
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

```
# Podok állapotának lekérdezése
kubectl get pods

# Service-ek lekérdezése
kubectl get svc

# BlazorUI megnyitása a böngészőben Minikube segítségével
minikube service blazorui-service
```

### Leállítás
```
# Erőforrások törlése a Kubernetes fürtből
kubectl delete -f k8s/

# Minikube leállítása
minikube stop
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



---

## 🐳 Saját Docker Hub Repository Használata

Ha a saját Docker Hub fiókodba szeretnéd feltölteni (push) az image-eket (így bárhonnan le tudod pull-olni őket), kövesd az alábbi lépéseket:

### 1. Bejelentkezés a Docker Hub-ba
```powershell
# Bekéri a Docker Hub felhasználónevedet és jelszavadat/tokenedet
docker login
```

### 2. Helyi build és Tag-elés (Címkézés)
A Docker Hub megköveteli, hogy az image-ek neve a felhasználóneveddel kezdődjön. Cseréld le a `sajat_felhasznalonev` részt a tiédre:

```powershell
$DOCKER_USER="sajat_felhasznalonev"

# Helyi image-ek felcímkézése a felhasználóneveddel
docker tag productapp-webapi $DOCKER_USER/productapp-webapi:latest
docker tag productapp-authapi $DOCKER_USER/productapp-authapi:latest
docker tag productapp-blazorui $DOCKER_USER/productapp-blazorui:latest
```

### 3. Feltöltés (Push)
```powershell
docker push $DOCKER_USER/productapp-webapi:latest
docker push $DOCKER_USER/productapp-authapi:latest
docker push $DOCKER_USER/productapp-blazorui:latest
```

> **Fontos:** Ha utána ezeket a publikált image-eket szeretnéd használni, ne felejtsd el frissíteni a `docker-compose.yml` (`image: sajat_felhasznalonev/...`) és a `k8s/productapp-manifest.yaml` fájlokban az image referenciákat!

### 4. Publikált image-ek használata Minikube-ban

Miután feltöltötted az image-eket a Docker Hub-ra, már nincs szükség a Minikube belső Docker démonjának használatára, a k8s közvetlenül a Docker Hub-ról fogja letölteni azokat. 

Ehhez módosítanod kell a Kubernetes manifest fájlt (`k8s/productapp-manifest.yaml`), hogy az új, Docker Hub-os image-re mutasson.

**Példa a `productapp-manifest.yaml` módosítására:**
```yaml
      containers:
      - name: webapi
        # Ez volt régen:
        # image: productapp-webapi
        # Ez lesz az új (cseréld le a felhasználónevedre!):
        image: sajat_felhasznalonev/productapp-webapi:latest
        # Érdemes az imagePullPolicy-t is beállítani, hogy biztosan a legfrissebbet töltse le:
        imagePullPolicy: Always
```

Miután frissítetted a manifest fájlt minden service-nél (webapi, authapi, blazorui), egyszerűen alkalmazd újra a konfigurációt:
```powershell
kubectl apply -f k8s/
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
