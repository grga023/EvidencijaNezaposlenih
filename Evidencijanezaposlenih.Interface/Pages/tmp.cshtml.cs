using EvidencijaNezaposlenih.ModeliPodataka.DTO;
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


        //public async Task OnPostAsync()
        //{
        //    List<RadniOdnosPrikaz> RadniOdnosLista = new();

        //    RadniOdnosLista.Add(new RadniOdnosPrikaz
        //    {
        //        NazivFirme = "Bomist",
        //        Staz = 50,
        //    });

        //    RadniOdnosLista.Add(new RadniOdnosPrikaz
        //    {
        //        NazivFirme = "Ogrev",
        //        Staz = 20,
        //    });


        //    NezaposleniUnos obj = new NezaposleniUnos
        //    {
        //        Adresa = "Zrenjanin",
        //        BrojTelefona = "063637108",
        //        Ime = "Milan",
        //        Prezime = "Buric",
        //        DatumRodjenja = DateTime.Parse("4/6/2001"),
        //        RadniOdnosPrikaz = RadniOdnosLista
        //    };

        //    //await _nezaposleniServis.KreirajNezaposlenog(obj);

        //    var data  = await _nezaposleniServis.DajSvePoID(Name);
        //    var tmp = "Grgur";

        //    var data2 = await _nezaposleniServis.DajSvePoimenuIPrezimenu(tmp);

        //    tmp = "Ognjen";
        //    var data3 = await _nezaposleniServis.DajSvePoimenuIPrezimenu(tmp);

        //    tmp = "Ognjen Grgur";
        //    var data4 = await _nezaposleniServis.DajSvePoimenuIPrezimenu(tmp);

        //    //await _nezaposleniServis.Obrisi(Name);

        //    data = null;
        //}
    }
}
