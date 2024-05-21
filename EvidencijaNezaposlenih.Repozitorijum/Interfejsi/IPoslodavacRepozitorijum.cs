using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.ModeliPodataka.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.Repozitorijum.Interfejsi
{
    public interface IPoslodavacRepozitorijum : IRepozitorijum<Poslodavac>
    {
        Task<Poslodavac> PronadjiPoNazivu(object filter);
        Task<IEnumerable<PoslodavacPrikaz>> DajSvePogled(object pogled);
        Task DodajStorred(PoslodavacUnos obj);
    }
}
