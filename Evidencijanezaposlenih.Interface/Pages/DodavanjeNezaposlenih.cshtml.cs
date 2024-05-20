using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.Repozitorijum.Interfejsi;
using EvidencijaNezaposlenih.Servisi.Interfejsi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Evidencijanezaposlenih.Interface.Pages
{
    public class DodavanjeNezaposlenihModel : PageModel
    {
        private readonly INezaposleniServis _nezaposleniServis;
        private readonly IPoslodavacRepozitorijum _poslodavacRepozitorijum;

        public DodavanjeNezaposlenihModel(IPoslodavacRepozitorijum poslodavacRepozitorijum, INezaposleniServis nezaposleniServis)
        {
            _poslodavacRepozitorijum = poslodavacRepozitorijum;
            _nezaposleniServis = nezaposleniServis;
        }

        public async Task OnGetAsync()
        {
            // Populate the combo boxes with data from _firmaServis.DajSve() method
            var firms = await _poslodavacRepozitorijum.DajSve();
            foreach (var firm in firms)
            {
                ViewData["Firms"] += $"<option >{firm.Naziv} | {firm.Grad}</option>"; // Adjust as per your Firma model properties
            }
        }
        public async Task OnPost()
        {
            int cnt = 0;
            var nazivFirme = Request.Form["nazivFirme[]"];
            var datumPocetka = Request.Form["datumPocetka[]"];
            var datumZavrsetka = Request.Form["datumZavrsetka[]"];

            List<RadniOdnosPrikaz> radniOdnosi = new List<RadniOdnosPrikaz>();

            foreach (var item in nazivFirme)
            {
                RadniOdnosPrikaz radniOdnos = new()
                {
                    NazivFirme = nazivFirme[cnt].ToString(),
                    DatumPocetka = DateTime.Parse(datumPocetka[cnt].ToString()),
                    DatumZavrsetka = DateTime.Parse(datumZavrsetka[cnt].ToString())
                };
                radniOdnosi.Add(radniOdnos);
                cnt++;

            }

            NezaposleniUnos nezaposleniUnos = new()
            { 
                Ime = Request.Form["name"].ToString(),
                Prezime = Request.Form["surname"].ToString(),
                DatumRodjenja = DateTime.Parse(Request.Form["dateOfBirth"].ToString()),
                JMBG = Request.Form["jmbg"].ToString(),
                Adresa = Request.Form["adresa"].ToString(),
                BrojTelefona = Request.Form["phoneNumber"].ToString(),
                RadniOdnosPrikaz = radniOdnosi
            };
            
            await _nezaposleniServis.KreirajNezaposlenog(nezaposleniUnos);

            cnt++;
        }
    }
}
