using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.Servisi.Interfejsi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Evidencijanezaposlenih.Interface.Pages
{
    [Authorize(Roles = "user,admin")]
    public class PoslodavacPikazModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public PoslodavacPikazModel(HttpClient httpClient)
        {
            PoslodavacPrikazLista = new List<PoslodavacPrikaz>();
            _httpClient = httpClient;
        }
        public List<PoslodavacPrikaz> PoslodavacPrikazLista { get; set; }

        public async Task OnGetAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<List<PoslodavacPrikaz>>("http://localhost:8080/api/FirmaKontroler");

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
                data = await _httpClient.GetFromJsonAsync<List<PoslodavacPrikaz>>("http://localhost:8080/api/FirmaKontroler");
            }
            else
            {
                data = await _httpClient.GetFromJsonAsync<List<PoslodavacPrikaz>>("http://localhost:8080/api/FirmaKontroler/filter?naziv="+filter);
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

