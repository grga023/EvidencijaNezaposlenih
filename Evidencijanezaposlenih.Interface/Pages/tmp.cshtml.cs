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


        public void OnPost()
        {
            var data  = _nezaposleniServis.DajSvePoID(Name);

            while (true)
            {

            }
        }
    }
}
