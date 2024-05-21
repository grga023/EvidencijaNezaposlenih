using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.PoslovnaLogika.Interfejsi;
using EvidencijaNezaposlenih.PoslovnaLogika.Validacija;
using EvidencijaNezaposlenih.Servisi.Interfejsi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Evidencijanezaposlenih.Interface.Pages
{
    public class DodavanjeFirmeModel : PageModel
    {
        private readonly IPoslodavacServis _poslodavacServis;
        private readonly IPoslovnaLogika _poslovnaLogika;

        public DodavanjeFirmeModel(IPoslodavacServis poslodavacServis, IPoslovnaLogika poslovnaLogika)
        {
            _poslodavacServis = poslodavacServis;
            _poslovnaLogika = poslovnaLogika;
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
                CustomMessage = "Invalid PIB"; // Set the custom message
                ModelState.AddModelError("PIB", "Invalid PIB");
                return Page();
            }

            await _poslodavacServis.KreirajPoslodavca(obj);
            return Page();
        }
    }
}
