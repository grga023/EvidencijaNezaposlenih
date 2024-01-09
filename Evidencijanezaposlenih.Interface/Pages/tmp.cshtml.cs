using EvidencijaNezaposlenih.Servisi.Interfejsi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Evidencijanezaposlenih.Interface.Pages
{
    public class tmpModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }
        private readonly INezaposleniServis _nezaposleniServis;

        public tmpModel (INezaposleniServis nezaposleniServis)
        {
            _nezaposleniServis = nezaposleniServis;
        }


        public async Task OnPostAsync()
        {
            var tmp = "Grgur";

            var data  = await _nezaposleniServis.DajSvePoID(Name);

            var data2 = await _nezaposleniServis.DajSvePoimenuIPrezimenu(tmp);
            _ = await _nezaposleniServis.Obrisi(Name);

            data = null;
        }
    }
}
