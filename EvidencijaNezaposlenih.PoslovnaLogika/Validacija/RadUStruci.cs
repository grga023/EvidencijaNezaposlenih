using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.PoslovnaLogika.Interfejsi;
using EvidencijaNezaposlenih.PoslovnaLogika.Sifarnik;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.PoslovnaLogika.Validacija
{
    public class RadUStruci : IRadUStruci
    {
        public NezaposleniUnos DaLiJeRedioUStruci(NezaposleniUnos obj)
        {
            string json = File.ReadAllText("zanimanja.json");
            //Ucitavanje podataka iz JSON fajla
            var data = JsonConvert.DeserializeObject<Dictionary<string, List<Zanimanje>>>(json);

            // Pozicija koju želimo da pretražujemo
            string trazenaPozicija = "";

            List<RadniOdnosPrikaz> odnosi = new();

            //Prolazak kroz listu radnih odnosa
            foreach (var kvp in obj.RadniOdnosPrikaz)
            {
                trazenaPozicija = kvp.Pozicija.ToString().ToUpper();

                List<string> zanimanjaKojaSadrzePoziciju = new List<string>();
                foreach (var zanimanje in data["zanimanja"])
                {
                    if (zanimanje.Pozicije.Contains(trazenaPozicija))
                    {
                        zanimanjaKojaSadrzePoziciju.Add(zanimanje.Naziv);
                    }
                }

                foreach (var poz in zanimanjaKojaSadrzePoziciju)
                {
                    //Radi lakse pretrage slova se pretvaraju u velika
                    if (poz == obj.Zanimanje.ToUpper())
                    {
                        kvp.Struka = true;
                    }
                }
                odnosi.Add(kvp);
            };

            obj.RadniOdnosPrikaz = null;
            obj.RadniOdnosPrikaz = odnosi;

            //Vracanje izmenjenog objekta
            return obj;
        }
    }
}
