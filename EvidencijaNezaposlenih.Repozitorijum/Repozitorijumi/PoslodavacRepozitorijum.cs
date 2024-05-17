using EvidencijaNezaposlenih.ModeliPodataka.Modeli;
using EvidencijaNezaposlenih.Repozitorijum.Context;
using EvidencijaNezaposlenih.Repozitorijum.Interfejsi;
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


        public async Task<Poslodavac?> DajSvePoPrimarnomKljucu(object PK)
        {
            if (PK is int PIB)
            {
                return await _ctx.Poslodavci.FindAsync(PIB);
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

        public void Snimi()
        {
            _ctx.SaveChanges();
        }
    }
}
