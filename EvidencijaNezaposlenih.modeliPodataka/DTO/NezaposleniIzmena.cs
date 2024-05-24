using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.ModeliPodataka.DTO
{
    public class NezaposleniIzmena
    {
        public required string ID { get; set; }
        public required string Ime { get; set; }
        public required string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public required string BrojTelefona { get; set; }
        public required string Adresa { get; set; }
        public string Zanimanje { get; set; }
        public required string JMBG { get; set; }
    }
}
