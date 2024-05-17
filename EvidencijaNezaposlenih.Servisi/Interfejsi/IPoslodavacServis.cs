using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.Servisi.Interfejsi
{
    public interface IPoslodavacServis
    {
        Task<IEnumerable<PoslodavacPrikaz>> DajSve();
        Task<IEnumerable<PoslodavacPrikaz>> DajSvePoNazivu(object filter);
        Task<PoslodavacPrikaz> DajSvePoPIB(object PK);
        Task KreirajPoslodavca(PoslodavacUnos obj);
        Task Obrisi(object PK);
        Task Azuriraj(NezaposleniIzmena obj);
    }
}
