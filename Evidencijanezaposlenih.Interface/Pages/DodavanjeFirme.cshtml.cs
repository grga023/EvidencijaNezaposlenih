using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.PoslovnaLogika.Interfejsi;
using EvidencijaNezaposlenih.PoslovnaLogika.Validacija;
using EvidencijaNezaposlenih.Servisi.Interfejsi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http.Json;

namespace Evidencijanezaposlenih.Interface.Pages
{
    [Authorize]
    public class DodavanjeFirmeModel : PageModel
    {
        private readonly IPoslovnaLogika _poslovnaLogika;
        private readonly HttpClient _httpClient;

        public DodavanjeFirmeModel(IPoslovnaLogika poslovnaLogika, HttpClient httpClient)
        {
            _poslovnaLogika = poslovnaLogika;
            _httpClient = httpClient;
        }
        [BindProperty]
        public bool ShowPopup { get; set; }

        [BindProperty]
        public string CustomMessage { get; set; }

        public void OnGet()
        {
        }
        public async Task<PageResult> OnPostAsync()
        {
            PoslodavacUnos obj = new()
            {
                PIB = Int32.Parse(Request.Form["pib"]),
                Adresa = Request.Form["adresa"],
                Grad = Request.Form["grad"],
                Naziv = Request.Form["naziv"]
            };

            if (!_poslovnaLogika.ValidirajPIB(obj))
            { 
                ShowPopup = true; // Set to true to show the popup
                CustomMessage = "PIB mora biti 8 cifata"; // Set the custom message
                ModelState.AddModelError("PIB", "PIB mora biti 8 cifata");
                return Page();
            }

            await _httpClient.PostAsJsonAsync("http://localhost:8080/api/FirmaKontroler", obj);

            //await _poslodavacServis.KreirajPoslodavca(obj);
            return Page();
        }
    }
}
