﻿using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.ModeliPodataka.Modeli;
using EvidencijaNezaposlenih.Repozitorijum.Interfejsi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.Servisi.Interfejsi
{
    public interface INezaposleniServis
    {
        Task<IEnumerable<NezaposleniPrikaz>> DajSve();
        Task<IEnumerable<NezaposleniPrikaz>> DajSvePoimenuIPrezimenu(object filter);
        Task<NezaposleniPrikaz> DajSvePoID(object PK);
        Task KreirajNezaposlenog(NezaposleniUnos obj);
        Task Obrisi(object PK);
        Task Azuriraj(NezaposleniUnos obj);
    }
}
