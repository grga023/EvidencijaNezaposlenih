using Microsoft.AspNetCore.Identity;

namespace Evidencijanezaposlenih.Interface.Context.Modeli
{
    public class Korisnik : IdentityUser
    {
        public string Ime { get; set; } = "" ;
        public string Prezime { get; set; } = "";
        public string Adresa { get; set; } = "";
        public DateTime Kreiran { get; set; }   

    }
}
