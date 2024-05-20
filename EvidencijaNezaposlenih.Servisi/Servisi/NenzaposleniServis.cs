using Azure.Core;
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
        private readonly INezaposleniRepozitorijum _nezaposleniRepozitorijum;
        private readonly IPoslodavacRepozitorijum _poslodavacRepozitorijum;

        public NenzaposleniServis (INezaposleniRepozitorijum nezaposleniRepozitorijum, IPoslodavacRepozitorijum poslodavacRepozitorijum)
        {
            _nezaposleniRepozitorijum = nezaposleniRepozitorijum;
            _poslodavacRepozitorijum = poslodavacRepozitorijum;
        }

        public async Task Azuriraj(NezaposleniIzmena obj)
        {
            var data = await _nezaposleniRepozitorijum.DajSvePoPrimarnomKljucu(obj.ID);
            if (data != null)
                throw new ArgumentException("Pogresan ID");

            Nezaposleni nezaposleni = new Nezaposleni
            {
                ID = data.ID,
                Adresa = obj.Adresa,
                BrojTelefona = obj.BrojTelefona,
                Ime = obj.Ime,
                Prezime = obj.Prezime,
                DatumRodjenja = obj.DatumRodjenja,
                JMBG = obj.JMBG,
            };

            _nezaposleniRepozitorijum.Izmeni(nezaposleni);
            _nezaposleniRepozitorijum.Snimi();
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
                        DatumPocetka = iskustvo.DatumPocetka,
                        DatumZavrsetka = iskustvo.DatumZavrsetka,
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
                    DatumPocetka = iskustvo.DatumPocetka,
                    DatumZavrsetka = iskustvo.DatumZavrsetka,
                };
                radniOdnosList.Add(radniOdnos);
            }

            nezaposleni.RadniOdnosPrikaz = radniOdnosList;

            return nezaposleni;
        }

        public async Task<IEnumerable<NezaposleniPrikaz>> DajSvePoimenuIPrezimenu(object filter)
        {
            var data = await _nezaposleniRepozitorijum.DajSvePoFilteru((string)filter);
            if (data == null) { return null; }


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
                    JMBG = item.JMBG,
                    DatumRodjenja = item.DatumRodjenja,
                    RadniOdnosPrikaz = null
                };

                List<RadniOdnosPrikaz> radniOdnosList = new();

                foreach (var iskustvo in item.RadniOdnos)
                {
                    radniOdnos = new RadniOdnosPrikaz
                    {
                        NazivFirme = iskustvo.Poslodavac.Naziv,
                        DatumPocetka = iskustvo.DatumPocetka,
                        DatumZavrsetka = iskustvo.DatumZavrsetka,
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
        private string FormatirajFirmu(string nazivFirme)
        {
            string input = nazivFirme;
            int index = input.IndexOf('|'); 

            string result = index >= 0 ? input.Substring(0, index).Trim() : input;

            result = result.TrimEnd();
            return result;
        }
        public async Task KreirajNezaposlenog(NezaposleniUnos obj)
        {
            string ID_N = GenerisiRandomID();
            var postoji = await _nezaposleniRepozitorijum.DajSvePoPrimarnomKljucu(ID_N);
            if (postoji != null)
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
                var poslodavac = await _poslodavacRepozitorijum.PronadjiPoNazivu(FormatirajFirmu(item.NazivFirme));
                if (poslodavac == null)
                    throw new ArgumentException("SF");

                radniOdnosList.Add(new RadniOdnos
                {
                    NezaposleniID = ID_N,
                    ID = poslodavac.ID,
                    DatumPocetka = item.DatumPocetka,
                    DatumZavrsetka = item.DatumZavrsetka,
                });
            }
            nezaposleniZaDodavanje.RadniOdnos = radniOdnosList;
            _nezaposleniRepozitorijum.Dodaj(nezaposleniZaDodavanje);
            _nezaposleniRepozitorijum.Snimi();
        }

        public async Task Obrisi(object PK)
        {
            await _nezaposleniRepozitorijum.Obrisi(PK);
            _nezaposleniRepozitorijum.Snimi();
            
        }
    }
}
