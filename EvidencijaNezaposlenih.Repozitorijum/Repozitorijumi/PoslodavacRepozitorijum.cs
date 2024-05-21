﻿using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.ModeliPodataka.Modeli;
using EvidencijaNezaposlenih.Repozitorijum.Context;
using EvidencijaNezaposlenih.Repozitorijum.Interfejsi;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EvidencijaNezaposlenih.Repozitorijum.Repozitorijumi
{
    public class PoslodavacRepozitorijum : IPoslodavacRepozitorijum
    {
        private readonly EvidencijaNezaposlenihDBContext _ctx;

        public PoslodavacRepozitorijum(EvidencijaNezaposlenihDBContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));
        }

        public async Task<IEnumerable<Poslodavac>> DajSve()
        {
            return await _ctx.Poslodavci.ToListAsync();
        }

        public async Task<List<Poslodavac>> DajSvePoFilteru(object filter)
        {
            if (filter is string naziv)
            {
                var data = await _ctx.Poslodavci.Where(x => x.Naziv.Contains((string)filter)).ToListAsync();
                return data;
            }
            else
            {
                throw new ArgumentException("Filter mora biti String.");
            }   
        }

        public async Task<IEnumerable<PoslodavacPrikaz>> DajSvePogled(object pogled)
        {
            var poslodavci = await _ctx.PoslodavacPrikaz.ToListAsync();

            return poslodavci;
        }

        public async Task<Poslodavac?> DajSvePoPrimarnomKljucu(object PK)
        {
            if (PK is int ID)
            {
                return await _ctx.Poslodavci.FindAsync(ID);
            }
            else
            {
                throw new ArgumentException("Primarni kljuc mora biti INT.");
            }
        }

        public Poslodavac Dodaj(Poslodavac obj)
        {
            _ctx.Poslodavci.Add(obj);
            return obj;
        }

        public Poslodavac? Izmeni(Poslodavac obj)
        {
            var poslodavac = _ctx.Poslodavci.Find(obj.PIB);

            if (poslodavac != null)
            {
                poslodavac.Adresa = obj.Adresa;
                poslodavac.Naziv = obj.Naziv;

                _ctx.Poslodavci.Update(poslodavac);
            }
            return poslodavac;
        }

        public async Task<Poslodavac?> Obrisi(object PK)
        {
            var poslodavac = await _ctx.Poslodavci.FindAsync(PK);

            if (poslodavac != null)
            {
                _ctx.Poslodavci.Remove(poslodavac);
            }

            return poslodavac;
        }

        public async Task<Poslodavac> PronadjiPoNazivu(object filter) 
            => await _ctx.Poslodavci.FirstOrDefaultAsync(x => x.Naziv == (string)filter);

        public async Task DodajStorred(PoslodavacUnos obj)
        {
            var paramPIB = new SqlParameter("@PIB", obj.PIB);
            var paramNaziv = new SqlParameter("@Naziv", obj.Naziv);
            var paramGrad = new SqlParameter("@Grad", obj.Grad);
            var paramAdresa = new SqlParameter("@Adresa", obj.Adresa);

            var data = await _ctx.Database.ExecuteSqlRawAsync("EXEC AddPoslodavac @PIB, @Naziv, @Grad, @Adresa",
                                                   paramPIB, paramNaziv, paramGrad, paramAdresa);
        }

        public void Snimi()
        {
             _ctx.SaveChanges();
        }


    }
}
