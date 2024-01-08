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
    public class NenzaposleniServis : INezaposleniServis
    {
        private readonly INezaposleniServis _nezaposleniServis;
        private readonly INezaposleniRepozitorijum _nezaposleniRepozitorijum;
        private readonly IRadniOdnosRepozitorijum _radniOdnosRepozitorijum;
        private readonly IPoslodavacRepozitorijum _poslodavacRepozitorijum;

        public NenzaposleniServis (INezaposleniServis nezaposleniServis, INezaposleniRepozitorijum nezaposleniRepozitorijum, IRadniOdnosRepozitorijum radniOdnosRepozitorijum, IPoslodavacRepozitorijum poslodavacRepozitorijum)
        {
            _nezaposleniServis = nezaposleniServis;
            _nezaposleniRepozitorijum = nezaposleniRepozitorijum;
            _radniOdnosRepozitorijum = radniOdnosRepozitorijum;
            _poslodavacRepozitorijum = poslodavacRepozitorijum;
        }

        public Task Azuriraj(NezaposleniUnos obj)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<NezaposleniPrikaz>> DajSve()
        {
            var data = await _nezaposleniRepozitorijum.DajSve();
            if(data == null) { throw new ArgumentNullException("Nema unetih podataka"); }

            List<NezaposleniPrikaz> rezultat = new();
            NezaposleniPrikaz nezaposleni;
            RadniOdnosPrikaz radniOdnos;


            foreach(var item in data)
            {
                nezaposleni = new NezaposleniPrikaz
                { 
                    Adresa = item.Adresa,
                    BrojTelefona = item.BrojTelefona,
                    Ime = item.Ime,
                    Prezime = item.Prezime,
                    DatumRodjenja = item.DatumRodjenja,
                    RadniOdnosPrikaz = null
                };

                List<RadniOdnosPrikaz> radniOdnosList = new();

                foreach(var iskustvo in item.RadniOdnos)
                {
                    radniOdnos = new RadniOdnosPrikaz
                    {
                        NazivFirme = iskustvo.Poslodavac.Naziv,
                        Staz = iskustvo.Trajanje
                    };
                    radniOdnosList.Add(radniOdnos);
                }
                nezaposleni.RadniOdnosPrikaz = radniOdnosList;
                rezultat.Add(nezaposleni);   
            }

            return rezultat;
        }

        public async Task<NezaposleniPrikaz> DajSvePoID(object PK)
        {
            var data = await _nezaposleniRepozitorijum.DajSvePoPrimarnomKljucu(PK);
            if (data == null) { throw new ArgumentNullException("Nema unetih podataka"); }

            NezaposleniPrikaz nezaposleni;
            RadniOdnosPrikaz radniOdnos;

            nezaposleni = new NezaposleniPrikaz
            {
                Adresa = data.Adresa,
                BrojTelefona = data.BrojTelefona,
                Ime = data.Ime,
                Prezime = data.Prezime,
                DatumRodjenja = data.DatumRodjenja,
                RadniOdnosPrikaz = null
            };

            List<RadniOdnosPrikaz> radniOdnosList = new();

            foreach (var iskustvo in data.RadniOdnos)
            {
                radniOdnos = new RadniOdnosPrikaz
                {
                    NazivFirme = iskustvo.Poslodavac.Naziv,
                    Staz = iskustvo.Trajanje
                };
                radniOdnosList.Add(radniOdnos);
            }

            nezaposleni.RadniOdnosPrikaz = radniOdnosList;

            return nezaposleni;
        }

        public async Task<IEnumerable<NezaposleniPrikaz>> DajSvePoimenuIPrezimenu(object filter)
        {
            var data = await _nezaposleniRepozitorijum.DajSvePoFilteru(filter);
            if (data == null) { throw new ArgumentNullException("Nema unetih podataka"); }

            List<NezaposleniPrikaz> rezultat = new();
            NezaposleniPrikaz nezaposleni;
            RadniOdnosPrikaz radniOdnos;


            foreach (var item in data)
            {
                nezaposleni = new NezaposleniPrikaz
                {
                    Adresa = item.Adresa,
                    BrojTelefona = item.BrojTelefona,
                    Ime = item.Ime,
                    Prezime = item.Prezime,
                    DatumRodjenja = item.DatumRodjenja,
                    RadniOdnosPrikaz = null
                };

                List<RadniOdnosPrikaz> radniOdnosList = new();

                foreach (var iskustvo in item.RadniOdnos)
                {
                    radniOdnos = new RadniOdnosPrikaz
                    {
                        NazivFirme = iskustvo.Poslodavac.Naziv,
                        Staz = iskustvo.Trajanje
                    };
                    radniOdnosList.Add(radniOdnos);
                }
                nezaposleni.RadniOdnosPrikaz = radniOdnosList;
                rezultat.Add(nezaposleni);
            }

            return rezultat;
        }

        private string GenerisiRandomID()
        {
            string IDzaKontrolu;
            string ID;

            Random generator = new Random();
            int dig = generator.Next(1, 1000);
            //lending zero
            string Prvi = dig.ToString("000");
            int deo1 = generator.Next(1, 1000000000);
            int deo2 = generator.Next(1, 10000);
            string Drugi = deo1.ToString("000000000") + deo2.ToString("0000");

            IDzaKontrolu = Prvi + Drugi;
            long TeloBroja = long.Parse(IDzaKontrolu);

            long KontrolniBroj = 98 - ((TeloBroja * 100) % 97);
            string kontrolniString = KontrolniBroj.ToString("00");

            ID = Prvi + "-" + Drugi + "-" + kontrolniString;

            //if (!_validationService.IsValidBillNumber(billNumber))
            //{
            //    GenerateRandomBillNumber();
            //}

            return ID;
        }

        public async Task KreirajNezaposlenog(NezaposleniUnos obj)
        {
            string ID_N = GenerisiRandomID();
            var postoji = await _nezaposleniRepozitorijum.DajSvePoPrimarnomKljucu(ID_N);
            if (postoji == null)
                await KreirajNezaposlenog(obj);

            Nezaposleni nezaposleniZaDodavanje = new Nezaposleni
            {
                ID = ID_N,
                Ime = obj.Ime,
                Prezime = obj.Prezime,
                BrojTelefona = obj.BrojTelefona,
                DatumRodjenja = obj.DatumRodjenja,
                Adresa = obj.Adresa,
                JMBG = obj.JMBG
            };

            List<RadniOdnos> radniOdnosList = new List<RadniOdnos>();

            foreach (var item in obj.RadniOdnosPrikaz)
            {
                var poslodavac = await _poslodavacRepozitorijum.PronadjiPoNazivu(item);
                if (poslodavac == null)
                    throw new ArgumentException("SF");

                radniOdnosList.Add(new RadniOdnos
                {
                    NezaposleniID = ID_N,
                    PIB = poslodavac.PIB
                });
            }
            nezaposleniZaDodavanje.RadniOdnos = radniOdnosList;
            _nezaposleniRepozitorijum.Dodaj(nezaposleniZaDodavanje);
            _nezaposleniRepozitorijum.Snimi();
        }

        public Task Obrisi(object PK)
        {
            _nezaposleniRepozitorijum.Obrisi(PK);
            _nezaposleniRepozitorijum.Snimi();
            return Task.CompletedTask;
        }
    }
}
