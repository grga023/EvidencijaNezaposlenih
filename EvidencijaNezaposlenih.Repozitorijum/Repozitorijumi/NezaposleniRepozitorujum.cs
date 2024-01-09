using EvidencijaNezaposlenih.ModeliPodataka.Modeli;
using EvidencijaNezaposlenih.Repozitorijum.Context;
using EvidencijaNezaposlenih.Repozitorijum.Interfejsi;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.Repozitorijum.Repozitorijumi
{
    public class NezaposleniRepozitorujum : INezaposleniRepozitorijum
    {
        private readonly EvidencijaNezaposlenihDBContext _context;

        public NezaposleniRepozitorujum(EvidencijaNezaposlenihDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Nezaposleni>> DajSve()
        {
            return await _context.Nezaposleni
                .Include(x => x.RadniOdnos)
                .ThenInclude(x => x.Poslodavac)
                .ToListAsync();
        }

        public async Task<List<Nezaposleni>?> DajSvePoFilteru(object filter)
        {
            if (filter is string filterString)
            {

                var filterParts = filterString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (filterParts.Length == 2)
                {
                    // Filter sadrži i ime i prezime
                    string ime = filterParts[0];
                    string prezime = filterParts[1];

                    return await _context.Nezaposleni
                        .Include(x => x.RadniOdnos)
                        .ThenInclude(x => x.Poslodavac)
                        .Where(nezaposleni =>
                            nezaposleni.Ime.Contains(ime) &&
                            nezaposleni.Prezime.Contains(prezime))
                        .ToListAsync();
                }
                else if (filterParts.Length == 1)
                {
                    // Filter sadrži ili ime ili prezime
                    var data =  await _context.Nezaposleni
                        .Include(x => x.RadniOdnos)
                        .ThenInclude(x => x.Poslodavac)
                        .Where(nezaposleni =>
                            nezaposleni.Ime == filterParts[0] ||
                            nezaposleni.Prezime == filterParts[0])
                        .ToListAsync();
                    return data;
                }
                else
                {
                    throw new ArgumentException("Invalid format for filter. Use 'Ime Prezime'.");
                }
            }
            else
            {
                throw new ArgumentException("Invalid data type for filter.");
            }
        }


        public async Task<Nezaposleni?> DajSvePoPrimarnomKljucu(object PK)
        {
            return await _context.Nezaposleni.Include(x => x.RadniOdnos)
                         .ThenInclude(x => x.Poslodavac).FirstOrDefaultAsync(x => x.ID == PK);
        }

        public Nezaposleni Dodaj(Nezaposleni obj)
        {
            _ = _context.Nezaposleni.Add(obj).Entity;
            return obj;
        }

        public Nezaposleni? Izmeni(Nezaposleni obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            return obj;
        }

        public async Task<Nezaposleni?> Obrisi(object PK)
        {
            var nezaposleniToDelete = await DajSvePoPrimarnomKljucu(PK);
            if (nezaposleniToDelete != null)
            {
                _context.Nezaposleni.Remove(nezaposleniToDelete);
            }

            return nezaposleniToDelete;
        }

        public void Snimi()
        {
            _context.SaveChanges();
        }
    }
}
