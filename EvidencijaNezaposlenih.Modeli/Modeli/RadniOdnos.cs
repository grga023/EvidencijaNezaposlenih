using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.Modeli.Modeli
{
    public class RadniOdnos
    {
        [Required]
        public int ID { get; set; }
        public int PIB { get; set; }
        public string NezaposleniID { get; set; }
        public int trajanje { get; set; }
    }
}
