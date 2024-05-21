using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.ModeliPodataka.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.Servisi.Interfejsi
{
    public interface IRadniOdnosServis
    {
        Task<IEnumerable<RadniOdnos>> DajSve();
        Task<RadniOdnos> DajSvePoID(object PK_P, object PK_N);
        Task Obrisi(object ID_P, object JMBG);
        void Azuriraj(RadniOdnos obj);
    }
}
