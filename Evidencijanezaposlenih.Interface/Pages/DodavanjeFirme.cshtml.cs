using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.Servisi.Interfejsi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Evidencijanezaposlenih.Interface.Pages
{
    public class DodavanjeFirmeModel : PageModel
    {
        private readonly IPoslodavacServis _poslodavacServis;

        public DodavanjeFirmeModel(IPoslodavacServis poslodavacServis)
        {
            _poslodavacServis = poslodavacServis;
        }

        public void OnGet()
        {
        }
        public async Task OnPostAsync()
        {
            //var dateOfBirth = Request.Form["dateOfBirth"];
            //var jmbg = Request.Form["jmbg"];
            //var phoneNumber = Request.Form["phoneNumber"];
            PoslodavacUnos obj = new()
            {
                PIB = Int32.Parse(Request.Form["pib"]),
                Adresa = Request.Form["adresa"],
                Grad = Request.Form["grad"],
                Naziv = Request.Form["naziv"]
            };

            await _poslodavacServis.KreirajPoslodavca(obj);
        }
    }
}
