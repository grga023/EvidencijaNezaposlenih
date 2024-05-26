using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.PoslovnaLogika.Interfejsi
{
    public interface IRadUStruci
    {
        NezaposleniUnos DaLiJeRedioUStruci(NezaposleniUnos obj);
    }
}
