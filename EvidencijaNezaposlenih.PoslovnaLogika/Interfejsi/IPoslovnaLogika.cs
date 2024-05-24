using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.PoslovnaLogika.Interfejsi
{
    public interface IPoslovnaLogika
    {
        bool ValidirajIdNezaposlenog(string IdNezaposlenog);
        bool ValidirajTrajanjeRadnogOdnosa(NezaposleniUnos obj);
        bool ValidirajJMBG(NezaposleniUnos obj);
        bool ValidirajPIB(PoslodavacUnos obj);
        NezaposleniUnos DaLiJeRedioUStruci(NezaposleniUnos obj);
    }
}
