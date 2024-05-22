using Evidencijanezaposlenih.Interface.Migrations;
using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.Servisi.Interfejsi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Evidencijanezaposlenih.Interface.Pages
{
    [Authorize(Roles = "user,admin")]
    public class NezaposleniPrikazModel : PageModel
    {
        private readonly INezaposleniServis _nezaposleniService;
        private readonly IRadniOdnosServis _radniOdnosServis;

        public NezaposleniPrikazModel(INezaposleniServis nezaposleniService, IRadniOdnosServis radniOdnosServis)
        {
            _nezaposleniService = nezaposleniService;
            _radniOdnosServis = radniOdnosServis;
            NezaposleniList = new List<NezaposleniPrikaz>();
        }
        public List<NezaposleniPrikaz> NezaposleniList { get; set; }
        public async Task OnGetAsync()
        {
            var data = await _nezaposleniService.DajSve();

            foreach (var item in data)
            {
                NezaposleniPrikaz nezaposleni = new()
                {
                    Ime = item.Ime,
                    Prezime = item.Prezime,
                    Adresa = item.Adresa,
                    JMBG = item.JMBG,
                    BrojTelefona = item.BrojTelefona,
                    DatumRodjenja = item.DatumRodjenja,
                    RadniOdnosPrikaz = item.RadniOdnosPrikaz,
                };
                NezaposleniList.Add(nezaposleni);
            }

        }
        public async Task OnPostSearch()
        {
            var filter = Request.Form["filter"].ToString();

            var data = await _nezaposleniService.DajSvePoimenuIPrezimenu(filter);
            if (data != null)
            {
                foreach (var item in data)
                {
                    NezaposleniPrikaz nezaposleni = new()
                    {
                        Ime = item.Ime,
                        Prezime = item.Prezime,
                        Adresa = item.Adresa,
                        JMBG = item.JMBG,
                        BrojTelefona = item.BrojTelefona,
                        DatumRodjenja = item.DatumRodjenja,
                        RadniOdnosPrikaz = item.RadniOdnosPrikaz,
                    };
                    NezaposleniList.Add(nezaposleni);
                }
            }
            else NezaposleniList = new List<NezaposleniPrikaz>();

        }
        public async Task<RedirectToPageResult> OnPostObrisiAsync()
        {
            var data = Request.Form["nazivf"].ToString();

            var parts = data.ToString().Split(',');

            if (parts.Length == 2)
            {
                var nazivFirme = parts[0].ToString();
                var jmbg = parts[1].ToString();

                await _radniOdnosServis.Obrisi(nazivFirme, jmbg);
            }
            return RedirectToPage("/NezaposleniPikaz");
        }
    }
}

