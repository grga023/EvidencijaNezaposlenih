﻿using EvidencijaNezaposlenih.ModeliPodataka.Modeli;
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

        public async Task<IEnumerable<Nezaposleni>?> DajSve()
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
                            (nezaposleni.Ime.Contains(ime) &&
                            nezaposleni.Prezime.Contains(prezime)) ||
                            nezaposleni.Ime.Contains(prezime) &&
                            nezaposleni.Prezime.Contains(ime))
                        .ToListAsync();
                }
                else if (filterParts.Length == 1)
                {
                    // Filter sadrži ili ime ili prezime
                    var data =  await _context.Nezaposleni
                        .Include(x => x.RadniOdnos)
                        .ThenInclude(x => x.Poslodavac)
                        .Where(nezaposleni =>
                            nezaposleni.Ime.Contains(filterParts[0]) ||
                            nezaposleni.Prezime.Contains(filterParts[0]))
                        .ToListAsync();
                    return data;
                }
                else
                {
                    return await _context.Nezaposleni
                                    .Include(x => x.RadniOdnos)
                                    .ThenInclude(x => x.Poslodavac)
                                    .ToListAsync();
                }
            }
            else
            {
                throw new ArgumentException("Invalid data type for filter.");
            }
        }

        public async Task<Nezaposleni?> DajSvePoJMBG(object filter)
        {
            var data = await _context.Nezaposleni
                .Include(x => x.RadniOdnos)
                .ThenInclude(x => x.Poslodavac).FirstOrDefaultAsync(nezaposleni => nezaposleni.JMBG == (string)filter);
            return data;
        }

        public async Task<Nezaposleni?> DajSvePoPrimarnomKljucu(object PK)
        {
            var data = await _context.Nezaposleni.Include(x => x.RadniOdnos)
                         .ThenInclude(x => x.Poslodavac)
                         .FirstOrDefaultAsync(x => x.ID == PK.ToString());
            return data;
        }

        public Nezaposleni Dodaj(Nezaposleni obj)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _ = _context.Nezaposleni.Add(obj).Entity;
                    _context.SaveChanges();
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

        public Nezaposleni? Izmeni(Nezaposleni obj)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Nezaposleni.Update(obj);
                    _context.SaveChanges();
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

        public async Task<Nezaposleni?> Obrisi(object PK)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var nezaposleniToDelete = await _context.Nezaposleni.FirstOrDefaultAsync(x => x.ID == PK.ToString());
                    if (nezaposleniToDelete != null)
                    {
                        _context.Nezaposleni.Remove(nezaposleniToDelete);
                        _context.SaveChanges();
                    }
                    transaction.Commit();
                    return nezaposleniToDelete;
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
            _context.SaveChanges();
        }
    }
}
