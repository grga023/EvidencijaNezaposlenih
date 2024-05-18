using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.Servisi.Interfejsi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Evidencijanezaposlenih.Interface.Pages
{
    public class NezaposleniPrikazModel : PageModel
    {
        private readonly INezaposleniServis _nezaposleniService;

        public NezaposleniPrikazModel(INezaposleniServis nezaposleniService)
        {
            _nezaposleniService = nezaposleniService;
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
                    BrojTelefona = item.BrojTelefona,
                    DatumRodjenja = item.DatumRodjenja,
                    RadniOdnosPrikaz = item.RadniOdnosPrikaz,
                };
                NezaposleniList.Add(nezaposleni);
            }

        }
    }
}
