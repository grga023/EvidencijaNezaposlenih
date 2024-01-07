using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.Modeli.Modeli
{
    public class RadniOdnos
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Poslodavac")]
        public required int PIB { get; set; }
        public Poslodavac Poslodavac { get; set; }

        [ForeignKey("Nezaposleni")]
        public required string NezaposleniID { get; set; }
        public Nezaposleni Nezaposleni { get; set; }

        public required int Trajanje { get; set; }
    }
}
