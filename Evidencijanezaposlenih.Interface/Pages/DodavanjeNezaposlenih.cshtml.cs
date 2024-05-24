using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.PoslovnaLogika.Interfejsi;
using EvidencijaNezaposlenih.Repozitorijum.Interfejsi;
using EvidencijaNezaposlenih.Servisi.Interfejsi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.CompilerServices;

namespace Evidencijanezaposlenih.Interface.Pages
{
    [Authorize(Roles = "admin")]
    public class DodavanjeNezaposlenihModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly INezaposleniServis _nezaposleniServis;
        private readonly IPoslovnaLogika _poslovnaLogika;
        public DodavanjeNezaposlenihModel(HttpClient httpClient, INezaposleniServis nezaposleniServis, IPoslovnaLogika poslovnaLogika)
        {
            _httpClient = httpClient;
            _nezaposleniServis = nezaposleniServis;
            _poslovnaLogika = poslovnaLogika;
        }
        [BindProperty]
        public bool ShowPopup { get; set; }

        [BindProperty]
        public string CustomMessage { get; set; }

        private async Task ucitajFirmeAsync()
        {
            var firms = await _httpClient.GetFromJsonAsync<List<PoslodavacPrikaz>>("https://localhost:7240/api/FirmaKontroler");
            foreach (var firm in firms)
            {
                ViewData["Firms"] += $"<option >{firm.Naziv} | {firm.Grad}</option>"; // Adjust as per your Firma model properties
            }
        }
        public async Task OnGetAsync()
        {
            await ucitajFirmeAsync();
        }
        public async Task OnPost()
        {
            int cnt = 0;
            var nazivFirme = Request.Form["nazivFirme[]"];
            var datumPocetka = Request.Form["datumPocetka[]"];
            var datumZavrsetka = Request.Form["datumZavrsetka[]"];
            var pozicija = Request.Form["pozicija[]"];

            List<RadniOdnosPrikaz> radniOdnosi = new List<RadniOdnosPrikaz>();

            foreach (var item in nazivFirme)
            {
                RadniOdnosPrikaz radniOdnos = new()
                {
                    NazivFirme = nazivFirme[cnt].ToString(),
                    Pozicija = pozicija[cnt].ToString(),
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
                Zanimanje = Request.Form["zanimanje"].ToString(),
                RadniOdnosPrikaz = radniOdnosi
            };

            if (!_poslovnaLogika.ValidirajJMBG(nezaposleniUnos))
            {
                ShowPopup = true;
                CustomMessage = "Neispravan JMBG";
                ModelState.AddModelError("JMBG", "Neispravan JMBG");
                await ucitajFirmeAsync();
            }
            else if (!_poslovnaLogika.ValidirajTrajanjeRadnogOdnosa(nezaposleniUnos))
            {
                ShowPopup = true;
                CustomMessage = "Datum zavrsetka radnog odnosa mora biti veci od datuma pocetka radnog odnosa";
                ModelState.AddModelError("Datum zavrsetka radnog odnosa", "Datum zavrsetka radnog odnosa mora biti veci od datuma pocetka radnog odnosa");
                await ucitajFirmeAsync();
            }
            else
            {
                await _nezaposleniServis.KreirajNezaposlenog(nezaposleniUnos);
                await ucitajFirmeAsync();
            }

        }
    }
}
