# Project startup - DEV
* docker compose --env-file .env.dev -f docker-compose.yml  up -d
# Project startup - PROD
* docker compose --env-file .env.dev -f docker-compose.release.yml  up -d

# PUBLISH
```
release.ps1 x.x.x
```