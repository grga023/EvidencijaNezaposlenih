using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.Modeli.Modeli
{
    public class Nezaposleni
    {
        [Required]
        public required string ID { get; set; }
        public required string Ime { get; set; }
        public required string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string? BrojTelefona { get; set; }
        public required string Adresa { get; set; }
    }

}
