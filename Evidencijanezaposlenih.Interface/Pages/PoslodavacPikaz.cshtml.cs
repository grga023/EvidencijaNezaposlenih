using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.Servisi.Interfejsi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Evidencijanezaposlenih.Interface;

namespace Evidencijanezaposlenih.Interface.Pages
{
    [Authorize(Roles = "user,admin")]
    public class PoslodavacPikazModel : PageModel
    {
        private readonly HttpClient _httpClient;
        public PoslodavacPikazModel(IHttpClientFactory httpClientFactory)
        {
            PoslodavacPrikazLista = new List<PoslodavacPrikaz>();
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }
        public List<PoslodavacPrikaz> PoslodavacPrikazLista { get; set; }

        public async Task OnGetAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<List<PoslodavacPrikaz>>("/api/FirmaKontroler");

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
            List<PoslodavacPrikaz> data = new();
            if (filter == "")
            {
                data = await _httpClient.GetFromJsonAsync<List<PoslodavacPrikaz>>("/api/FirmaKontroler");
            }
            else
            {
                data = await _httpClient.GetFromJsonAsync<List<PoslodavacPrikaz>>("/api/FirmaKontroler/filter?naziv="+filter);
            }
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

