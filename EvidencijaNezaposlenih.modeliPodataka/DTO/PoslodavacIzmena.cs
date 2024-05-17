using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.ModeliPodataka.DTO
{
    public class PoslodavacIzmena
    {
        public required int ID { get; set; }
        public required int PIB { get; set; }
        public required string Naziv { get; set; }
        public required string Grad { get; set; }
        public required string Adresa { get; set; }
    }
}
