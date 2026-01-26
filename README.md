 * Odaberes Repozitorijum projekat u konzli videces u gornjem delu konzole
 * Kucas:
 * Init-Migration -Context EvidencijaNezaposlenihDBContext DataBase
 * Update-database -Context EvidencijaNezaposlenihDBContext
	
 * Odaberes interface projekat u konzli videces u gornjem delu konzole
 * Init-Migration -Context IdentitetiDBContext Identiteti
 * Update-database -Context IdentitetiDBContext

// NIJE U FUNKCIJI ZA TRENUTNU IMPLMENTACIJU
# Project startup - Visual studio
* Docker startup proj
* Run project
# Project startup - Terminal
* docker compose --env-file .env.dev -f docker-compose.release.yml  up -d

# PUBLISH
```
release.ps1 x.x.x
```

