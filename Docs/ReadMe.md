## Indítás Minikube-bal

### 1. Előfeltételek
- Docker Desktop telepítve és futtatva (Ha a docker drivert szeretnénk használni, de nem kötelező)
- Minikube telepítve
- Kubernetes manifest fájl(ok) (pl. a `k8s/` mappában)

### 2. Indítás
A mellékelt Kubernetes manifest fájlnak létre kell hozni egy mappát, bemásolni és
futtatni a következő parancsokat.
```
# Minikube indítása
minikube start

# Ha a productapp-manifest.yaml fájlt tartalmazó könyvtár vagyunk:
kubectl apply -f .

# Ha nem a productapp-manifest.yaml fájlt tartalmazó könyvtár vagyunk:
kubectl apply -f <productapp-manifest.yaml fájlt tartalmazó könyvtár>
```

### 3. Ellenőrzés

```
# Podok állapotának lekérdezése:
kubectl get pods

# Ha minden pod futó (running) állapotban van, akkor a BlazorUI szolgáltatás indítható.
# Csak néhány másodpercet vehet igénybe, amíg minden pod elindul.
minikube service blazorui-service
```

### Leállítás
```
# Erőforrások törlése a Kubernetes fürtből

# Ha a productapp-manifest.yaml fájlt tartalmazó könyvtár vagyunk:
kubectl delete -f .

# Ha nem a productapp-manifest.yaml fájlt tartalmazó könyvtár vagyunk:
kubectl delete -f <productapp-manifest.yaml fájlt tartalmazó könyvtár>


# Minikube leállítása
minikube stop
```
---

## A belépéshez szükséges autentikációs adatok:

- **Username**: `admin` / **Password**: `admin` (Admin)
- **Username**: `user` / **Password**: `user` (User)

### Felhasználói útmutató: 
Login mellett, a képernyő jobb felső sarkában található Manual nevű linkkel érhető el. 

### Github Repository:
https://github.com/kekorn/productapp.git

### Github Container Registry: 
- ghcr.io/kekorn/productapp/webapi
- ghcr.io/kekorn/productapp/authapi
- ghcr.io/kekorn/productapp/blazorui
---
