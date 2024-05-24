
# BITNO

 * Otvoris NP kozolu 
 * Tools -> NuGet package manager -> NPM console
   
 * Odaberes Repozitorijum projekat u konzli videces u gornjem delu konzole
 * Kucas:
 * Init-Migration -Context EvidencijaNezaposlenihDBContext DataBase
 * Update-database -Context EvidencijaNezaposlenihDBContext
	
 * Odaberes interface projekat u konzli videces u gornjem delu konzole
 * Init-Migration -Context IdentitetiDBContext Identiteti
 * Update-database -Context IdentitetiDBContext

 * i trebala bi da se pojavi baza na localhost-u (u sql server object exploreru to ti je u view pa ga nadjes, cisto da imas db i da vidis kako modeli trenba da izgledaju)

## kreirati pogled sa sledecim kodom
CREATE VIEW [dbo].PoslodavacPrikaz 
    AS SELECT PIB, Naziv, Grad, Adresa  FROM Poslodavci

## STORED PROCEDURA sa sledecim kodom.....
CREATE PROCEDURE [dbo].AddPoslodavac @PIB INT, @Naziv NVARCHAR(255), @Grad NVARCHAR(255), @Adresa NVARCHAR(255) AS BEGIN INSERT INTO Poslodavci (PIB, Naziv, Grad, Adresa) VALUES (@PIB, @Naziv, @Grad, @Adresa); END;
