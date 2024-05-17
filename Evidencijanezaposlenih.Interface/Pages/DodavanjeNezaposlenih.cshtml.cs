using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.Servisi.Interfejsi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Evidencijanezaposlenih.Interface.Pages
{
    public class DodavanjeNezaposlenihModel : PageModel
    {
        private readonly INezaposleniServis _nezaposleniServis;

        public DodavanjeNezaposlenihModel(INezaposleniServis nezaposleniServis)
        {
            _nezaposleniServis = nezaposleniServis;
        }
        public void OnPost()
        {
            int cnt = 0;
            //var name = Request.Form["name"];
            //var surname = Request.Form["surname"];
            //var dateOfBirth = Request.Form["dateOfBirth"];
            //var jmbg = Request.Form["jmbg"];
            //var phoneNumber = Request.Form["phoneNumber"];

            // Handling the list of work experiences
            var nazivFirme = Request.Form["nazivFirme[]"];
            var datumPocetka = Request.Form["datumPocetka[]"];
            var datumZavrsetka = Request.Form["datumZavrsetka[]"];

            List<RadniOdnosPrikaz> radniOdnosi = new List<RadniOdnosPrikaz>();

            foreach (var item in nazivFirme)
            {
                RadniOdnosPrikaz radniOdnos = new()
                {
                    NazivFirme = nazivFirme[cnt],
                    DatumPocetka = DateTime.Parse(datumPocetka[cnt]),
                    DatumZavrsetka = DateTime.Parse(datumZavrsetka[cnt])
                };
                radniOdnosi.Add(radniOdnos);
            }

            NezaposleniUnos nezaposleniUnos = new()
            { 
                Ime = Request.Form["name"],
                Prezime = Request.Form["surname"],
                DatumRodjenja = DateTime.Parse(Request.Form["dateOfBirth"]),
                JMBG = Request.Form["jmbg"],
                Adresa = "Adresa",
                BrojTelefona = Request.Form["phoneNumber"],
                RadniOdnosPrikaz = radniOdnosi
            };

            _nezaposleniServis.KreirajNezaposlenog(nezaposleniUnos);
        }
    }
}
