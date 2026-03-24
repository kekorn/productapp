# Docker

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

# Dockerfile alapfelépése

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

1:16