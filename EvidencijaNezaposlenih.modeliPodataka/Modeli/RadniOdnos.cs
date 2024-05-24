using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.ModeliPodataka.Modeli
{
    public class RadniOdnos
    {
        public DateTime DatumPocetka { get; set; }
        public DateTime DatumZavrsetka { get; set; }
        public string Pozicija { get; set; }
        public bool Struka {  get; set; }

        [ForeignKey("Poslodavac")]
        public int ID { get; set; }
        public Poslodavac Poslodavac { get; set; }

        [ForeignKey("Nezaposleni")]
        public string NezaposleniID { get; set; }
        public Nezaposleni Nezaposleni { get; set; }

    }
}
