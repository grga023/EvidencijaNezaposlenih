using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.ModeliPodataka.Modeli;
using EvidencijaNezaposlenih.Repozitorijum.Interfejsi;
using EvidencijaNezaposlenih.Servisi.Interfejsi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.Servisi.Servisi
{
    public class PoslodavacServis : IPoslodavacServis
    {
        private readonly IPoslodavacRepozitorijum _poslodavacRepozitorijum;

        public PoslodavacServis(IPoslodavacRepozitorijum poslodavacRepozitorijum)
        {
            _poslodavacRepozitorijum = poslodavacRepozitorijum;
        }

        public Task Azuriraj(NezaposleniIzmena obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PoslodavacPrikaz>> DajSve()
        {
            var data = await _poslodavacRepozitorijum.DajSvePogled("");
            if (data == null)
                throw new ArgumentException("SF");

            List<PoslodavacPrikaz> poslodavci = new();

            foreach (var obj in data)
            {
                PoslodavacPrikaz poslodavacPrikaz = new()
                {
                    Adresa = obj.Adresa,
                    Naziv = obj.Naziv,
                    Grad = obj.Grad,
                    PIB = obj.PIB,
                };

                poslodavci.Add(poslodavacPrikaz);
            }


            return poslodavci;
        }

        public async Task<IEnumerable<PoslodavacPrikaz>> DajSvePoNazivu(object filter)
        {
            List<PoslodavacPrikaz> poslodavci = new();
            var data = await _poslodavacRepozitorijum.DajSvePoFilteru(filter);
            if (data.Count == 0)
                return poslodavci;

            foreach (var obj in data)
            {
                poslodavci.Add(new PoslodavacPrikaz
                {
                    Adresa = obj.Adresa,
                    Naziv = obj.Naziv,
                    Grad = obj.Grad,
                    PIB = obj.PIB,
                });

            }


            return poslodavci;
        }

        public async Task<PoslodavacPrikaz> DajSvePoPIB(object PK)
        {
            var data = await _poslodavacRepozitorijum.DajSvePoPrimarnomKljucu(PK);
            if (data == null)
                throw new ArgumentException("SF");

            PoslodavacPrikaz poslodavacPrikaz = new()
            {
                Adresa = data.Adresa,
                Naziv = data.Naziv,
                Grad = data.Grad,
                PIB = data.PIB,
            };

            return poslodavacPrikaz;
        }

        public async Task KreirajPoslodavca(PoslodavacUnos obj)
        {
            try
            {
                var data = await _poslodavacRepozitorijum.DajSvePoFilteru(obj.Naziv);
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        var dataById = await _poslodavacRepozitorijum.DajSvePoPrimarnomKljucu(item.PIB);
                        if (dataById != null)
                            throw new ArgumentException("Poslodavac postoji");

                    }
                }
            }
            catch (Exception ex) { }

            PoslodavacUnos poslodavacZaDodati = new()
            {
                Naziv = obj.Naziv,
                Adresa = obj.Adresa,
                Grad = obj.Grad,
                PIB = obj.PIB,
            };
            
            await _poslodavacRepozitorijum.DodajStorred(poslodavacZaDodati);
            _poslodavacRepozitorijum.Snimi();
        }

        public async Task Obrisi(object PK)
        {
            var pib = Int32.Parse(PK.ToString());
            await _poslodavacRepozitorijum.Obrisi(PK);
            _poslodavacRepozitorijum.Snimi();
        }
    }
}
