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
        public required int Trajanje { get; set; }

        [ForeignKey("Poslodavac")]
        public required Guid PIB { get; set; }
        public Poslodavac Poslodavac { get; set; }

        [ForeignKey("Nezaposleni")]
        public required string NezaposleniID { get; set; }
        public Nezaposleni Nezaposleni { get; set; }

    }
}
