using EvidencijaNezaposlenih.Modeli.Modeli;
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
            return await _context.Nezaposleni.ToListAsync();
        }

        public async Task<Nezaposleni?> DajSvePoFilteru(object filter)
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
                        .FirstOrDefaultAsync(nezaposleni =>
                            nezaposleni.Ime.Contains(ime) &&
                            nezaposleni.Prezime.Contains(prezime));
                }
                else if (filterParts.Length == 1)
                {
                    // Filter sadrži ili ime ili prezime
                    return await _context.Nezaposleni
                        .FirstOrDefaultAsync(nezaposleni =>
                            nezaposleni.Ime.Contains(filterParts[0]) ||
                            nezaposleni.Prezime.Contains(filterParts[0]));
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
            if (PK is string idString)
            {
                return await _context.Nezaposleni.FindAsync(idString);
            }
            else
            {
                throw new ArgumentException("Invalid data type for primary key.");
            }
        }

        public Nezaposleni Dodaj(Nezaposleni obj)
        {
            _context.Nezaposleni.Add(obj);
            _context.SaveChanges();
            return obj;
        }

        public Nezaposleni? Izmeni(Nezaposleni obj)
        {
            var existingNezaposleni = _context.Nezaposleni.Find(obj.ID);
            if (existingNezaposleni != null)
            {
                existingNezaposleni.Ime = obj.Ime;
                existingNezaposleni.Prezime = obj.Prezime;
                existingNezaposleni.DatumRodjenja = obj.DatumRodjenja;
                existingNezaposleni.BrojTelefona = obj.BrojTelefona;
                existingNezaposleni.Adresa = obj.Adresa;

                _context.SaveChanges();
            }

            return existingNezaposleni;
        }

        public async Task<Nezaposleni?> Obrisi(object PK)
        {
            var nezaposleniToDelete = await _context.Nezaposleni.FindAsync(PK);
            if (nezaposleniToDelete != null)
            {
                _context.Nezaposleni.Remove(nezaposleniToDelete);
                _context.SaveChanges();
            }

            return nezaposleniToDelete;
        }

        public void Snimi()
        {
            _context.SaveChanges();
        }
    }
}
