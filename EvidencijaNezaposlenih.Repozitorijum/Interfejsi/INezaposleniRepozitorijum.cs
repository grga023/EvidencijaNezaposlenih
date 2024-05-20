using EvidencijaNezaposlenih.ModeliPodataka.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.Repozitorijum.Interfejsi
{
    public interface INezaposleniRepozitorijum : IRepozitorijum<Nezaposleni>
    {
        Task<Nezaposleni?> DajSvePoJMBG(object filter);
    }
}
