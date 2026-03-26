# ☸️ ProductApp - Minikube Lokális Futtatási Útmutató

Ez az útmutató bemutatja, hogyan futtatható a **ProductApp** (MongoDB, WebAPI, AuthAPI, BlazorUI) egy lokális Kubernetes klaszteren, a Minikube segítségével.

## 📋 1. Előfeltételek
- [Docker Desktop](https://www.docker.com/products/docker-desktop) vagy más minikube által támogatott konténer motor telepítve.
- [Minikube](https://minikube.sigs.k8s.io/docs/start/) telepítve.
- `kubectl` parancssori eszköz telepítve.

## 🚀 2. Minikube Indítása

Nyisson egy terminált (PowerShell) rendszergazdaként, majd indítsa el a Minikube-ot konténeres (pl. docker) driver-rel:

```powershell
minikube start --driver=docker
```

Ellenőrizze, hogy a klaszter megfelelően elindult-e:
```powershell
kubectl get nodes
```

## 🐳 3. Docker Image-ek Buildelése Minikube-hoz

Ahhoz, hogy a Minikube azonnal lássa az Ön által buildelt lokális Docker image-eket anélkül, hogy azokat fel kellene töltenie (push) egy külső registry-be (pl. Docker Hub, Azure CR), a terminált a Minikube belső Docker démonjához kell kötni.

**PowerShell esetén futtassa ezt:**
```powershell
minikube docker-env | Invoke-Expression
```

Ezután lépjen be a projekt gyökérmappájába és buildelje az image-eket a `docker-compose` vagy `docker build` segítségével:
```powershell
# Csak az image-ek buildelése (így a Minikube registry-jébe kerülnek)
docker-compose build
```

*(Ha le akar válni a Minikube Docker démonjáról egy új PowerShell ablak nyitásával, vagy a `minikube docker-env -u | Invoke-Expression` paranccsal teheti meg.)*

## 📦 4. Alkalmazás Telepítése (Deployment)

1. Győződjön meg róla, hogy a Kubernetes manifest fájlok (pl. `k8s-deployment.yaml`) készen állnak. Ezeknek tartalmaznia kell a **MongoDB**, **WebAPI**, **AuthAPI** és **BlazorUI** deployment és service leírásait.
2. Alkalmazza a konfigurációt:

```powershell
kubectl apply -f k8s-deployment.yaml
# vagy ha mappában vannak a specifikációk:
kubectl apply -f k8s/
```

3. Ellenőrizze a pod-ok állapotát (várja meg, amíg a státuszuk `Running` nem lesz):
```powershell
kubectl get pods
```

## 🌐 5. Szolgáltatások Elérése

Mivel a Minikube lokális, a szolgáltatások eléréséhez a Minikube segítségét kell kérnünk:

**A futó Service-ek listázása:**
```powershell
kubectl get svc
```

**Egy adott szolgáltatás (pl. a BlazorUI) megnyitása a böngészőben:**
```powershell
minikube service blazorui
```
*Ez a parancs automatikusan csinál egy alagutat és megnyitja a böngészőt a megfelelő URL-en.*

**Alternatíva - Port-forward (Közvetlen port hozzárendelés a podhoz/servicehöz):**
```powershell
kubectl port-forward svc/blazorui 8080:8080
```
*Így a `http://localhost:8080` címen lesz elérhető az alkalmazás.*

## 📊 6. Minikube Dashboard (Irányítópult)

A Kubernetes beépített webes vizuális felületének megnyitása a könnyebb adminisztrációért:
```powershell
minikube dashboard
```
*Ez egy új böngésző fület nyit, ahol grafikusan is áttekintheti a podokat, service-eket és logokat.*

## 🛑 7. Leállítás és Clean-up

Ha végzett a fejlesztéssel/teszteléssel, leállíthatja a klasztert erőforrás-megtakarítás céljából:

```powershell
# Klaszter leállítása (adatok és beállítások megmaradnak, legközelebb gyorsabb lesz)
minikube stop

# Klaszter és a hozzá tartozó összes adat/image teljes törlése
minikube delete
```
