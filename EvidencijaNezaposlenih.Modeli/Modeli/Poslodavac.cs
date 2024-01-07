using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.Modeli.Modeli
{
    public class Poslodavac
    {
        [Key]
        public int PIB { get; set; }
        public required string Naziv { get; set; }
        public required string Adresa { get; set; }
    }
}
