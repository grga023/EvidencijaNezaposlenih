﻿using EvidencijaNezaposlenih.ModeliPodataka.Modeli;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.ModeliPodataka.DTO
{
    public class NezaposleniUnos
    {
        public required string Ime { get; set; }    
        public required string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public required string BrojTelefona { get; set; }
        public required string Adresa { get; set; }
        public required string JMBG { get; set; }
        public string Zanimanje { get; set; }
        public required List<RadniOdnosPrikaz> RadniOdnosPrikaz { get; set; }
    }
}
