using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.ModeliPodataka.Modeli;
using EvidencijaNezaposlenih.Servisi.Interfejsi;
using EvidencijaNezaposlenih.Servisi.Servisi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Evidencijanezaposlenih.Interface.Pages
{
    public class NezaposleniIzmeniModel : PageModel
    {
        private readonly INezaposleniServis _nezaposleniServis;
        private readonly IPoslodavacServis _poslodavacServis;
        public string Jmbg { get; set; }
        public NezaposleniPrikaz nezaposleni { get; set; }
        public NezaposleniIzmeniModel(INezaposleniServis nezaposleniServis, IPoslodavacServis poslodavacServis)
        {
            _nezaposleniServis = nezaposleniServis;
            _poslodavacServis = poslodavacServis;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Jmbg = Request.Query["jmbg"];

            if (string.IsNullOrEmpty(Jmbg))
            {
                return RedirectToPage("/Error");
            }
            var firms = await _poslodavacServis.DajSve();
            foreach (var firm in firms)
            {
                ViewData["Firms"] += $"<option >{firm.Naziv} | {firm.Grad}</option>"; // Adjust as per your Firma model properties
            }
            nezaposleni = await _nezaposleniServis.DajSvePoJMBGU(Jmbg);

            if (nezaposleni == null)
            {
                return NotFound();
            }
            return Page();
        }
        public async Task<RedirectToPageResult> OnPostAsync()
        {
            int cnt = 0;
            var nazivFirme = Request.Form["nazivFirme[]"];
            var datumPocetka = Request.Form["datumPocetka[]"];
            var datumZavrsetka = Request.Form["datumZavrsetka[]"];

            var firms = await _poslodavacServis.DajSve();
            foreach (var firm in firms)
            {
                ViewData["Firms"] += $"<option >{firm.Naziv} | {firm.Grad}</option>"; // Adjust as per your Firma model properties
            }

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

            NezaposleniPrikaz nezaposleniUnos = new()
            {
                Ime = Request.Form["name"].ToString(),
                Prezime = Request.Form["surname"].ToString(),
                DatumRodjenja = DateTime.Parse(Request.Form["dateOfBirth"].ToString()),
                JMBG = Request.Form["jmbg"].ToString(),
                Adresa = Request.Form["adresa"].ToString(),
                BrojTelefona = Request.Form["phoneNumber"].ToString(),
                RadniOdnosPrikaz = radniOdnosi
            };

            await _nezaposleniServis.Azuriraj(nezaposleniUnos);
            return RedirectToPage("/NezaposleniPikaz");
        }
    }
}
