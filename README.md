# Project startup - DEV
* docker compose up --build
# Project startup - PROD
* docker compose -f docker-compose.yml -f docker-compose.prod.yml up -d

# BUILD
```
docker build -t grga023/evidencijanezaposlenih_internetprogramiranje:x.x.x -f Evidencijanezaposlenih.Interface/Dockerfile .
docker build -t grga023/evidencijanezaposlenih_internetprogramiranje:latest -f Evidencijanezaposlenih.Interface/Dockerfile .
docker images
docker push grga023/evidencijanezaposlenih_internetprogramiranje:x.x.x
docker push grga023/evidencijanezaposlenih_internetprogramiranje:latest
```