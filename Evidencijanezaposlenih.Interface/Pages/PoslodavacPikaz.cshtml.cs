using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.Servisi.Interfejsi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Evidencijanezaposlenih.Interface.Pages
{
    public class PoslodavacPikazModel : PageModel
    {
        private readonly IPoslodavacServis _poslodavacServis;

        public PoslodavacPikazModel(IPoslodavacServis poslodavacServis)
        {
            _poslodavacServis = poslodavacServis;
            PoslodavacPrikazLista = new List<PoslodavacPrikaz>();
        }
        public List<PoslodavacPrikaz> PoslodavacPrikazLista { get; set; }
        public async Task OnGetAsync()
        {
            var data = await _poslodavacServis.DajSve();

            foreach (var item in data)
            {
                PoslodavacPrikazLista.Add(new PoslodavacPrikaz 
                {
                    Adresa = item.Adresa,
                    Grad = item.Grad,
                    Naziv = item.Naziv,
                    PIB = item.PIB
                });
            }

        }
        public async Task OnPostAsync()
        {
            var filter = Request.Form["filter"].ToString();

            var data = await _poslodavacServis.DajSvePoNazivu(filter);
            if (data != null)
            {
                foreach (var item in data)
                {
                    PoslodavacPrikazLista.Add(new PoslodavacPrikaz
                    {
                        Adresa = item.Adresa,
                        Grad = item.Grad,
                        Naziv = item.Naziv,
                        PIB = item.PIB
                    });
                }
            }
            else PoslodavacPrikazLista = new List<PoslodavacPrikaz>();

        }
    }
}

