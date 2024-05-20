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
                    JMBG = item.JMBG,
                    BrojTelefona = item.BrojTelefona,
                    DatumRodjenja = item.DatumRodjenja,
                    RadniOdnosPrikaz = item.RadniOdnosPrikaz,
                };
                NezaposleniList.Add(nezaposleni);
            }

        }
        public async Task OnPostAsync()
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
    }
}

