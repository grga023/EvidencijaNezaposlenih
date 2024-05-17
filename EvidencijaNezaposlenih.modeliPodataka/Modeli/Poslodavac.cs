﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.ModeliPodataka.Modeli
{
    public class Poslodavac
    {
        [Key]
        public int ID { get; set; }
        public int PIB { get; set; }
        public string Naziv { get; set; }
        public string Grad { get; set; }
        public string Adresa { get; set; }
    }
}
