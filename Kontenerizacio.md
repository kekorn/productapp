
[Certified KUBERNETES Application Developer Full Course for Beginners](https://www.youtube.com/watch?v=4rxIiOmKmiE&t=2052s)

[Kubernetes Zero to Hero: The Complete Beginner’s Guide (2025 Edition)](https://www.youtube.com/watch?v=MTHGoGUFpvE)
[TechWorld with Nana](https://www.youtube.com/@TechWorldwithNana/playlists)

# [Docker](https://www.docker.com/)

A Docker egy nyílt forráskódú platform, amely lehetővé teszi a fejlesztők számára, hogy alkalmazásokat és szolgáltatásokat konténerekbe csomagoljanak, szállítsanak és futtassanak. A Docker segítségével könnyen létrehozhatunk, tesztelhetünk és telepíthetünk alkalmazásokat különböző környezetekben, anélkül hogy aggódnunk kellene a függőségek vagy a környezeti különbségek miatt. A Docker konténerek izolált környezetet biztosítanak az alkalmazások számára, így azok függetlenül működhetnek egymástól és a gazda operációs rendszertől. A Docker egyre népszerűbbé válik a fejlesztők és a vállalatok körében, mivel lehetővé teszi számukra, hogy gyorsabban szállítsák az új szolgáltatásokat és hatékonyabban kezeljék az alkalmazásaikat.

**Docker disztribúciók:**
- `Docker Desktop`: Windows és MacOS rendszerekre, amely tartalmazza a Docker Engine-t
- `Docker Engine`: Linux rendszerekre, amely a Docker konténerek futtatásáért felelős
- `Docker Compose`: Több konténerből álló alkalmazások definiálására és futtatására szolgáló eszköz
- `Docker Swarm`: Docker konténerek klaszterezésére és skálázására szolgáló eszköz
- `Rancher`: Egy nyílt forráskódú konténer menedzsment platform, amely lehetővé teszi a Docker konténerek kezelését és skálázását több környezetben, beleértve a helyi gépeket, a felhőszolgáltatókat és a Kubernetes klasztereket is.
- `Podman`: Egy nyílt forráskódú konténer menedzsment eszköz, amely lehetővé teszi a Docker konténerek kezelését és futtatását anélkül, hogy szükség lenne egy démonra. A Podman kompatibilis a Docker CLI-vel, így a Docker parancsokat használhatjuk a Podman-nal is.

## Minta környezet kialakítása:
```
mkdir -p /var/www/html 
echo hello from docker >> /var/www/html/index.html

vagy 

New-Item -ItemType Directory -Force -Path "./var/www/html"
"hello from docker" | Out-File -FilePath "./var/www/html/index.html" -Encoding utf8 -Append

Docker konténer létrehozása:
docker run -d -p 8081:80 --name="myapache" -v /var/www/html:/var/www/html httpd
```
**Konténer kezelés:**
- `docker ps`: Futó konténerek listázása
- `docker ps -a`: Összes konténer listázása
- `docker stop <id>`: Konténer leállítása: 
- `docker start <id>`: Konténer indítása: 
- `docker restart <id>`: Konténer újraindítása: 
- `docker kill <id>`: Konténer erőltetett bezárása
- `docker rm <id>`: Konténer törlése
- `docker rm -f <id>`: Konténer törlése, ha fut, akkor előbb leállítja
- `docker run -d --name mycontainer myimage`: Konténer indítása egy image-ből
- `docker run -d --name mycontainer -p 8080:80 myimage`: Konténer indítása egy image-ből, port mappinggel   
- `docker run -d --name mycontainer -v /host/path:/container/path myimage`: Konténer indítása egy image-ből, volume mappinggel

**Konténer interakció:**
- `docker exec -it <id> sh`: Interaktív shell megnyitása a konténerben
- `docker cp <id>:/path/in/container /path/on/host`: Fájl másolása a konténerből a gazda gépre
- `docker cp /path/on/host <id>:/path/in/container`: Fájl másolása a gazda gépről a konténerbe
- `docker attach <id>`: Csatlakozás egy futó konténerhez (CTRL+P, CTRL+Q a leváláshoz)
- `docker commit <id> myimage`: Konténer állapotának mentése egy új image-be
- `docker save <id> > mycontainer.tar`: Konténer mentése egy tar fájlba
- `docker load < mycontainer.tar`: Konténer betöltése egy tar fájlból
- `docker export <id> > mycontainer.tar`: Konténer fájlrendszerének exportálása egy tar fájlba
- `docker import mycontainer.tar`: Konténer fájlrendszerének importálása egy tar fájlból egy új image-be
- `buildah`: Konténer imagek építésére szolgáló eszköz, amely lehetővé teszi a Dockerfile nélküli image építést is, és nem igényel démon futtatását.
- `docker build -t myimage .`: Docker image építése a jelenlegi könyvtárban található Dockerfile alapján, és elnevezése "myimage"-re

**Konténer információk:**
- `docker logs <id>`: Konténer naplóinak megtekintése
- `docker logs -f <id>`: Konténer naplóinak folyamatos megtekintése (follow)
- `docker stats <id>`: Konténer erőforrás használatának megtekintése (CPU, memória, hálózat)
- `buildahost <id>`: Konténer host információinak megtekintése
- `docker port <id>`: Konténer portjainak megtekintése
- `docker top <id>`: Konténerben futó folyamatok megtekintése
- `docker inspect <id>`: Konténer részletes információinak megtekintése
- `docker inspect --format='{{.NetworkSettings.IPAddress}}' <id>`: Konténer IP címének lekérdezése
- `docker inspect --format='{{.State.Pid}}' <id>`: Konténer PID-jének lekérdezése
- `ps`: Gazda operációs rendszer folyamatai

**Image kezelés:**
- `docker image ls`: Docker imagek listázása
- `docker image inspect <ID>`: dokker image részletes információinak megtekintése
- `docker history <id>`, vagy `docker history image:tag`: Docker image rétegeinek megtekintése
- `docker image rm <ID>`: Image törlés
- `docker image rm -f <ID>`: Image törlés a csatlakoztatott konténerekkel együtt (leállítottakkal is)
- `docker build -t myimage .`: Docker image építése a jelenlegi könyvtárban található Dockerfile alapján, és elnevezése "myimage"-re
- `docker build -t myimage:1.0 .`: Docker image építése a jelenlegi könyvtárban található Dockerfile alapján, és elnevezése "myimage" névvel és "1.0" taggel


docker run -it busybox 
(CTRL+P, CTRL+Q ) lecsatlakozás, de fut tovább
(exit) leállít

## MariaDB konténer indítása:
*docker run -d --name mydb -e MYSQL_ROOT_PASSWORD=password -p 3306:3306 mariadb*
```
docker run: Új konténer indítása egy image-ből.
Detached mode = a konténer háttérben fut. Ha nem használnád, akkor a MariaDB logsorai betöltenék a terminálodat.
A konténer neve: mydb. Ez azért fontos, mert így könnyebb:
    - hivatkozni rá (docker stop mydb, docker logs mydb)
    - docker compose nélkül is kezelni
Környezeti változó beállítása a konténerben: -e (environment variable) MYSQL_ROOT_PASSWORD (a MariaDB root felhasználó jelszava) Ez az alapértelmezett módja a MariaDB/Mysql konténer konfigurációjának
mariadb: Az image neve, amelyből a konténer indul.
port beállítása: -p 3306:3306
```

## Dockerfile alapfelépése

**A Dockerfile általában az alábbi fő részekből áll:**
1. Alapkép megadása – FROM
2. Készítő információk (opcionális) – LABEL
3. Fájlok másolása a konténerbe – COPY, ADD
4. Parancsok futtatása a build során – RUN
5. Környezeti változók – ENV
6. Munkakönyvtár beállítása – WORKDIR
7. Portok megnyitása – EXPOSE
8. Függőségek telepítése – általában RUN
9. Indító parancs – CMD vagy ENTRYPOINT

**Példa egy egyszerű Dockerfile-ra:**
```

# 1. Alapkép
FROM node:18-alpine

# 2. Metaadatok
LABEL maintainer="kornel@example.com"
LABEL version="1.0"

# 3. Munkakönyvtár beállítása
WORKDIR /app

# 4. Csomagok másolása (COPY, ADD)
COPY package*.json ./

# 5. Parancs futtatása build közben, pl. telepítés
RUN npm install

# 6. Környezeti változók beállítása
ENV APP_ENV=production

# 7. Forráskód másolása
COPY . .

# 8. Port megnyitása (Ez csak dokumentációs jellegű; a futtatásnál -p kapcsolóval számít.)
EXPOSE 3000

# 9. Indítás
CMD ["npm", "start"]

(# 9. Vagy ENTRYPOINT használata (ha a parancs nem változik) kötelezően futó parancs. (Funkcionálisan azonos a CMD-vel, de „fixebb”.) :
ENTRYPOINT ["python"]
CMD ["app.py"])

```

|Funkció|CMD|ENTRYPOINT|
|-------|---|----------|
|Felülírható docker run paraméterrel|✔️ Igen|❌ Nem|
|Alkalmas alapértelmezett parancsra|✔️|✔️|
|„Fix” indító rendszer|❌|✔️|


---
# [Kubernetes](https://kubernetes.io/)

Kubernetes egy nyílt forráskódú konténer-orchestration rendszer, amelyet a Google fejlesztett ki, és amely lehetővé teszi a konténerek automatizált telepítését, skálázását és kezelését. A Kubernetes segítségével könnyen kezelhetjük a konténerizált alkalmazásokat nagy méretekben, és biztosíthatjuk azok megbízhatóságát és rendelkezésre állását. A Kubernetes számos szolgáltatást kínál, mint például a szolgáltatás felfedezése, terheléselosztás, automatikus skálázás és önjavítás. A Kubernetes egyre népszerűbbé válik a vállalatok körében, mivel lehetővé teszi számukra, hogy hatékonyabban kezeljék a konténerizált alkalmazásaikat és gyorsabban szállítsák az új szolgáltatásokat.**

**Kubernetes disztribúciók:**
- [`Minikube`](https://minikube.sigs.k8s.io/docs/): Egy könnyű Kubernetes környezet, amely helyi gépeken futtatható, ideális fejlesztéshez és teszteléshez.
- `Kind`: Kubernetes in Docker, egy eszköz, amely lehetővé teszi Kubernetes klaszterek létrehozását Docker konténerekben, szintén helyi fejlesztéshez és teszteléshez.
- `3s`: Egy könnyű Kubernetes disztribúció, amelyet a Rancher fejlesztett ki, és amely ideális IoT és edge computing környezetekben való használatra.
- `OpenShift`: Egy Kubernetes-alapú platform, amelyet a Red Hat fejlesztett ki, és amely kiegészítő szolgáltatásokat és eszközöket kínál a Kubernetes környezetek kezeléséhez.
- `EKS (Amazon Elastic Kubernetes Service)`: Egy felhőalapú Kubernetes szolgáltatás, amelyet az Amazon Web Services kínál, és amely lehetővé teszi a Kubernetes klaszterek egyszerű létrehozását és kezelését az AWS infrastruktúráján.
- `Google Anthos`: Egy felhőalapú Kubernetes szolgáltatás, amelyet a Google Cloud Platform kínál, és amely lehetővé teszi a Kubernetes klaszterek egyszerű létrehozását és kezelését a Google Cloud infrastruktúráján.
- `Redhat OpenShift`: Egy Kubernetes-alapú platform, amelyet a Red Hat fejlesztett ki, és amely kiegészítő szolgáltatásokat és eszközöket kínál a Kubernetes környezetek kezeléséhez, különösen vállalati környezetekben.
- `Suse Rancher`: Egy nyílt forráskódú konténer menedzsment platform, amely lehetővé teszi a Kubernetes klaszterek kezelését és skálázását több környezetben, beleértve a helyi gépeket, a felhőszolgáltatókat és a Kubernetes klasztereket is.
- `Canonical Kubernetes`: Egy Kubernetes disztribúció, amelyet a Canonical fejlesztett ki, és amely ideális helyi fejlesztéshez és teszteléshez, valamint kisebb méretű Kubernetes környezetekhez.

## [`Minikube`](https://minikube.sigs.k8s.io/docs/) telepítése és használata

### [Letöltés](https://storage.googleapis.com/minikube/releases/latest/minikube-installer.exe) vagy parancssori telepítés és inicializázió:

**Telepítés**
```
New-Item -Path 'c:\' -Name 'minikube' -ItemType Directory -Force
$ProgressPreference = 'SilentlyContinue'; Invoke-WebRequest -OutFile 'c:\minikube\minikube.exe' -Uri 'https://github.com/kubernetes/minikube/releases/latest/download/minikube-windows-amd64.exe' -UseBasicParsing
```

**Környezeti véktozók hozzáadása**
```
$oldPath = [Environment]::GetEnvironmentVariable('Path', [EnvironmentVariableTarget]::Machine)
if ($oldPath.Split(';') -inotcontains 'C:\minikube'){
  [Environment]::SetEnvironmentVariable('Path', $('{0};C:\minikube' -f $oldPath), [EnvironmentVariableTarget]::Machine)
}
```

**Indítás**
```
.\minikube start
```


**Hozzáférés a minikube klaszterben futó Kubernetes irányítópulthoz:**
```
.\minikube.exe dashboard
```


**Kuberentes alapfogalmak:**
- `Pod`: A Kubernetes legkisebb egysége, amely egy vagy több konténert tartalmaz, és amely egy közös hálózati és tárolási erőforrást használ.
- `Service`: Egy Kubernetes erőforrás, amely lehetővé teszi a Pod-ok közötti kommunikációt és a külső forgalom irányítását a Pod-okhoz.
- `Deployment`: Egy Kubernetes erőforrás, amely lehetővé teszi a Pod-ok replikációját és frissítését, valamint a kívánt állapot fenntartását.
- `Namespace`: Egy Kubernetes erőforrás, amely lehetővé teszi a Kubernetes erőforrások logikai elkülönítését és csoportosítását.
- `ConfigMap`: Egy Kubernetes erőforrás, amely lehetővé teszi a konfigurációs adatok tárolását és kezelését a Kubernetes környezetben.
- `ReplicaSet`: Egy Kubernetes erőforrás, amely lehetővé teszi a Pod-ok replikációját és a kívánt szám fenntartását.
- `Secret`: Egy Kubernetes erőforrás, amely lehetővé teszi a bizalmas adatok, például jelszavak és API kulcsok tárolását és kezelését a Kubernetes környezetben.
- `Volume`: Egy Kubernetes erőforrás, amely lehetővé teszi a tárolási erőforrások használatát a Kubernetes környezetben, például helyi fájlrendszerek, hálózati fájlrendszerek vagy felhőalapú tárhelyek.
- `Node`: Egy Kubernetes erőforrás, amely egy fizikai vagy virtuális gépet reprezentál a Kubernetes klaszterben, és amelyen a Pod-ok futnak.
- `Kubelet`: Egy Kubernetes komponens, amely a Node-okon fut, és amely felelős a Pod-ok életciklusának kezeléséért és a Kubernetes API-val való kommunikációért.
- `Kubectl`: Egy parancssori eszköz, amely lehetővé teszi a Kubernetes klaszterek kezelését és a Kubernetes erőforrások létrehozását, frissítését és törlését.
- `Kubernetes API`: Egy RESTful API, amely lehetővé teszi a Kubernetes erőforrások kezelését és a Kubernetes klaszterek irányítását.
- `Kubernetes Dashboard`: Egy webes felület, amely lehetővé teszi a Kubernetes klaszterek vizuális kezelését és a Kubernetes erőforrások megtekintését.
- `Kubernetes Operator`: Egy Kubernetes komponens, amely lehetővé teszi a Kubernetes erőforrások automatizált kezelését és a Kubernetes klaszterek irányítását egy adott alkalmazás vagy szolgáltatás számára.
- `Kubernetes Ingress`: Egy Kubernetes erőforrás, amely lehetővé teszi a külső forgalom irányítását a Kubernetes klaszterben futó szolgáltatásokhoz, például HTTP vagy HTTPS forgalom esetén.
- `Kubernetes StatefulSet`: Egy Kubernetes erőforrás, amely lehetővé teszi a Pod-ok állapotának fenntartását és a stabil hálózati azonosítók használatát, például adatbázisok vagy más állapotfüggő alkalmazások esetén.
- `Kubernetes DaemonSet`: Egy Kubernetes erőforrás, amely lehetővé teszi a Pod-ok futtatását minden Node-on a Kubernetes klaszterben, például loggyűjtők vagy hálózati szolgáltatások esetén.
- `Kubernetes Job`: Egy Kubernetes erőforrás, amely lehetővé teszi a Pod-ok futtatását egy adott feladat végrehajtására, például adatfeldolgozás vagy batch feladatok esetén.
- `Kubernetes CronJob`: Egy Kubernetes erőforrás, amely lehetővé teszi a Pod-ok futtatását időzített feladatok végrehajtására, például napi vagy heti ütemezés esetén.
- `Kubernetes Horizontal Pod Autoscaler`: Egy Kubernetes erőforrás, amely lehetővé teszi a Pod-ok automatikus skálázását a terhelés alapján, például CPU vagy memória használat esetén.
- `Kubernetes Cluster Autoscaler`: Egy Kubernetes erőforrás, amely lehetővé teszi a Node-ok automatikus skálázását a terhelés alapján, például Pod-ok száma vagy erőforrás használat esetén.


**Kuberentes alapfogalmak másképp:**
- `Deployment`: Az alkalmazást reprezentáló objektum, amely lehetővé teszi a Pod-ok replikációját és frissítését, valamint a kívánt állapot fenntartását.
- `ReplicaSet`: Replikákat tartalmazó replikakészlet, amely biztosítja, hogy a kívánt számú Pod mindig futjon.
- `Pod`: A Kubernetes legkisebb egysége, felhőszolgáltatáshoz szükséges képességeket biztosító konténerek gyűjteménye, amely egy közös hálózati és tárolási erőforrást használ.
- `Node`: Egy Kubernetes erőforrás, amely egy fizikai vagy virtuális gépet reprezentál a Kubernetes klaszterben, és amelyen a Pod-ok futnak.
- `On Premises`: Olyan környezet, ahol a Kubernetes klaszter saját infrastruktúrán, helyi gépeken vagy adatközpontban fut.
- `Service`: Egy Kubernetes erőforrás, amely lehetővé teszi a Pod-ok közötti kommunikációt és a külső forgalom irányítását a Pod-okhoz.
- `Namespace`: Egy Kubernetes erőforrás, amely lehetővé teszi a Kubernetes erőforrások logikai elkülönítését és csoportosítását.
- `ConfigMap`: Egy Kubernetes erőforrás, amely lehetővé teszi a konfigurációs adatok tárolását és kezelését a Kubernetes környezetben.
- `Secret`: Egy Kubernetes erőforrás, amely lehetővé teszi a bizalmas adatok, például jelszavak és API kulcsok tárolását és kezelését a Kubernetes környezetben.
- `Volume`: Egy Kubernetes erőforrás, amely lehetővé teszi a tárolási erőforrások használatát a Kubernetes környezetben, például helyi fájlrendszerek, hálózati fájlrendszerek vagy felhőalapú tárhelyek.
- `Kubelet`: Egy Kubernetes komponens, amely a Node-okon fut, és amely felelős a Pod-ok életciklusának kezeléséért és a Kubernetes API-val való kommunikációért.
- `Kubectl`: Egy parancssori eszköz, amely lehetővé teszi a Kubernetes klaszterek kezelését és a Kubernetes erőforrások létrehozását, frissítését és törlését.
- `Kubernetes API`: Egy RESTful API, amely lehetővé teszi a Kubernetes erőforrások kezelését és a Kubernetes klaszterek irányítását.
- `Kubernetes Dashboard`: Egy webes felület, amely lehetővé teszi a Kubernetes klaszterek vizuális kezelését és a Kubernetes erőforrások megtekintését.
- `Kubernetes Operator`: Egy Kubernetes komponens, amely lehetővé teszi a Kubernetes erőforrások automatizált kezelését és a Kubernetes klaszterek irányítását egy adott alkalmazás vagy szolgáltatás számára.
- `Kubernetes Ingress`: Egy Kubernetes erőforrás, amely lehetővé teszi a külső forgalom irányítását a Kubernetes klaszterben futó szolgáltatásokhoz, például HTTP vagy HTTPS forgalom esetén.
- `Kubernetes StatefulSet`: Egy Kubernetes erőforrás, amely lehetővé teszi a Pod-ok állapotának fenntartását és a stabil hálózati azonosítók használatát, például adatbázisok vagy más állapotfüggő alkalmazások esetén.
- `Kubernetes DaemonSet`: Egy Kubernetes erőforrás, amely lehetővé teszi a Pod-ok futtatását minden Node-on a Kubernetes klaszterben, például loggyűjtők vagy hálózati szolgáltatások esetén.
- `Kubernetes Job`: Egy Kubernetes erőforrás, amely lehetővé teszi a Pod-ok futtatását egy adott feladat végrehajtására, például adatfeldolgozás vagy batch feladatok esetén.
- `Kubernetes CronJob`: Egy Kubernetes erőforrás, amely lehetővé teszi a Pod-ok futtatását időzített feladatok végrehajtására, például napi vagy heti ütemezés esetén.
- `Kubernetes Horizontal Pod Autoscaler`: Egy Kubernetes erőforrás, amely lehetővé teszi a Pod-ok automatikus skálázását a terhelés alapján, például CPU vagy memória használat esetén.
- `Kubernetes Cluster Autoscaler`: Egy Kubernetes erőforrás, amely lehetővé teszi a Node-ok automatikus skálázását a terhelés alapján, például Pod-ok száma vagy erőforrás használat esetén.


**Kuberentes parancsok:**
- `kubeadm`: Egy eszköz, amely lehetővé teszi a Kubernetes klaszterek létrehozását és kezelését, különösen a helyi gépeken vagy a saját infrastruktúrán.
- `kubectl --help`: A kubectl parancs súgójának megjelenítése
- `kubectl get pods`: Pod-ok listázása
- `kubectl get services`: Szolgáltatások listázása
- `kubectl get deployments`: Deployment-ek listázása
- `kubectl get namespaces`: Namespace-ek listázása
- `kubectl get configmaps`: ConfigMap-ek listázása
- `kubectl get secrets`: Secret-ek listázása
- `kubectl get volumes`: Volume-ok listázása
- `kubectl get nodes`: Node-ok listázása
- `kubectl describe pod <pod-name>`: Pod részletes információinak megtekintése
- `kubectl describe service <service-name>`: Szolgáltatás részletes információinak megtekintése
- `kubectl describe configmap <configmap-name>`: ConfigMap részletes információinak megtekintése
- `kubectl describe secret <secret-name>`: Secret
- `kubectl describe node <node-name>`: Node részletes információinak megtekintése
- `kubectl describe namespace <namespace-name>`: Namespace részletes információinak megtekintése

---
Deployment létrehozása egy adott image-ből: `kubectl create deploy <deployment-name> --image=<image-name>`

`kubectl get all`: Minden erőforrás listázása

`kubectl api-resources`:Az összes elérhető Kubernetes erőforrás típus listázása 

git clone https://github.com/sandervanvugt/ckad 