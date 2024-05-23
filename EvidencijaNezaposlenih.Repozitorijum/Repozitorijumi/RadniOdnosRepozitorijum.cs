using EvidencijaNezaposlenih.ModeliPodataka.Modeli;
using EvidencijaNezaposlenih.Repozitorijum.Context;
using EvidencijaNezaposlenih.Repozitorijum.Interfejsi;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.Repozitorijum.Repozitorijumi
{
    public class RadniOdnosRepozitorijum : IRadniOdnosRepozitorijum
    {
        private readonly EvidencijaNezaposlenihDBContext _ctx;

        public RadniOdnosRepozitorijum(EvidencijaNezaposlenihDBContext ctx)
        {
            _ctx = ctx;
        }
        public Task<IEnumerable<RadniOdnos>> DajSve()
        {
            throw new NotImplementedException();
        }

        public async Task<List<RadniOdnos>> DajSvePoFilteru(object filter)
        {
            if (filter is string filterStr)
            {
                var filterParts = filterStr.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (filterParts.Length == 2)
                {
                    var ID_N = filterParts[0];
                    var ID_P = filterParts[1];

                    return await _ctx.RadniOdnosi
                        .Where(c => c.NezaposleniID == ID_N &&
                        c.ID == Int32.Parse(ID_P)).ToListAsync();
                }
                else if (filterParts.Length == 1)
                {
                    return await _ctx.RadniOdnosi
                        .Where(c => c.NezaposleniID == filterParts[0] ||
                        c.ID == Int32.Parse(filterParts[1])).ToListAsync();
                }
                else { throw new ArgumentException("Los format filtera"); }
            }
            else { throw new ArgumentException("Filter nije String"); }
        }

        public async Task<RadniOdnos?> DajSvePoPrimarnomKljucu(object PK)
        {
            string PKString = (string)PK;
            var PKParts = PKString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var radniOdnos = await _ctx.RadniOdnosi
            .FirstOrDefaultAsync(c => c.NezaposleniID == PKParts[0].ToString() &&
            c.ID == Int32.Parse(PKParts[1]));

            if (radniOdnos == null)
            {
                return null;
            }
            return radniOdnos;
        }

        public RadniOdnos Dodaj(RadniOdnos obj)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    _ctx.RadniOdnosi.Add(obj);
                    _ctx.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Transaction failed", ex);
                }
            }
            return obj;
        }

        public RadniOdnos? Izmeni(RadniOdnos obj)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    var radniOdnos = _ctx.RadniOdnosi.FirstOrDefault(c => c.NezaposleniID == obj.NezaposleniID && c.ID == obj.ID);
                    if (radniOdnos != null)
                    {
                        _ctx.RadniOdnosi.Update(obj);
                    }
                    else
                    {
                        _ctx.RadniOdnosi.Add(obj);
                    }
                    _ctx.SaveChanges();
                    transaction.Commit();
                    return radniOdnos;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Transaction failed", ex);
                }
            }
        }

        public async Task<RadniOdnos?> Obrisi(object PK)
        {
            using (var transaction = _ctx.Database.BeginTransaction())
            {
                try
                {
                    string PKString = (string)PK;
                    var PKParts = PKString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                    var radniOdnos = await _ctx.RadniOdnosi.FirstOrDefaultAsync(c => c.NezaposleniID == PKParts[0] && c.ID == Int32.Parse(PKParts[1]));
                    if (radniOdnos != null)
                    {
                        _ctx.RadniOdnosi.Remove(radniOdnos);
                        await _ctx.SaveChangesAsync();
                    }

                    transaction.Commit();
                    return null;
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
