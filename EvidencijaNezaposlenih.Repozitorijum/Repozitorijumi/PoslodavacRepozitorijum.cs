using EvidencijaNezaposlenih.ModeliPodataka.DTO;
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

        public async Task<List<Poslodavac>> DajSvePoFilteru(object naziv)
        {
            if (naziv is string nazivP)
            {
                    var data = await _ctx.Poslodavci.Where(x => x.Naziv.Contains((string)nazivP)).ToListAsync();
                    return data;
            }
            else
            {
                var data = await _ctx.Poslodavci.ToListAsync();
                return data;
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
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    _ctx.Poslodavci.Add(obj);
                    _ctx.SaveChanges();
                    transaction.Commit();
                    return obj;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Transaction failed", ex);
                }
            }
        }

        public Poslodavac? Izmeni(Poslodavac obj)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    var poslodavac = _ctx.Poslodavci.Find(obj.PIB);

                    if (poslodavac != null)
                    {
                        poslodavac.Adresa = obj.Adresa;
                        poslodavac.Naziv = obj.Naziv;

                        _ctx.Poslodavci.Update(poslodavac);
                        _ctx.SaveChanges();
                    }

                    transaction.Commit();
                    return poslodavac;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Transaction failed", ex);
                }
            }
        }

        public async Task<Poslodavac?> Obrisi(object PK)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    var poslodavac = await _ctx.Poslodavci.FirstOrDefaultAsync(x => x.PIB == Int32.Parse(PK.ToString()));

                    if (poslodavac != null)
                    {
                        _ctx.Poslodavci.Remove(poslodavac);
                        await _ctx.SaveChangesAsync();
                    }

                    transaction.Commit();
                    return poslodavac;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Transaction failed", ex);
                }
            }
        }

        public async Task<Poslodavac> PronadjiPoNazivu(object filter) 
            => await _ctx.Poslodavci.FirstOrDefaultAsync(x => x.Naziv == (string)filter);

        public async Task DodajStorred(PoslodavacUnos obj)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    var paramPIB = new SqlParameter("@PIB", obj.PIB);
                    var paramNaziv = new SqlParameter("@Naziv", obj.Naziv);
                    var paramGrad = new SqlParameter("@Grad", obj.Grad);
                    var paramAdresa = new SqlParameter("@Adresa", obj.Adresa);

                    var data = await _ctx.Database.ExecuteSqlRawAsync("EXEC AddPoslodavac @PIB, @Naziv, @Grad, @Adresa",
                                                               paramPIB, paramNaziv, paramGrad, paramAdresa);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Transaction failed", ex);
                }
            }
        }

        public void Snimi()
        {
             _ctx.SaveChanges();
        }


    }
}
