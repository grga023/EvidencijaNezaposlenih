﻿using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.PoslovnaLogika.Interfejsi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EvidencijaNezaposlenih.PoslovnaLogika.Sifarnik;
using Newtonsoft.Json;

namespace EvidencijaNezaposlenih.PoslovnaLogika.Validacija
{
    public class PoslovnaLogika : IPoslovnaLogika
    {
        public bool ValidirajIdNezaposlenog(string IdNezaposlenog)
        { 
            IdNezaposlenog = IdNezaposlenog.Replace("-", "");

            if (IdNezaposlenog.Length < 18)
            {
                return false;
            }
            int controlNumber = Convert.ToInt32(IdNezaposlenog.Substring(IdNezaposlenog.Length - 2));
            string billSubstring = IdNezaposlenog.Substring(0, 16);
            long numberBody = long.Parse(billSubstring);
            if (98 - ((numberBody * 100) % 97) == controlNumber)
            {
                return true;
            }
            return false;
        }

        public bool ValidirajJMBG(NezaposleniUnos obj)
        {
            var JMBG = obj.JMBG;

            if (JMBG.Length != 13)
            {
                return false; 
            }

            var datumRodjenja = obj.DatumRodjenja;

            var dan = datumRodjenja.Day.ToString("D2");
            var mesec = datumRodjenja.Month.ToString("D2");
            var godina = datumRodjenja.Year.ToString().Substring(1,3);

            var jmbgDan = JMBG.Substring(0, 2);
            var jmbgMesec = JMBG.Substring(2, 2);
            var jmbgGodina = JMBG.Substring(4, 3);

            return jmbgDan == dan && jmbgMesec == mesec && jmbgGodina == godina;
        }

        public bool ValidirajPIB(PoslodavacUnos obj)
        {
            var len = obj.PIB.ToString().Length;
            if (len != 8) return false;
            else return true;
        }

        public bool ValidirajTrajanjeRadnogOdnosa(NezaposleniUnos obj)
        {
            foreach(var item in obj.RadniOdnosPrikaz)
            {
                if (item.DatumZavrsetka < item.DatumPocetka) return false;
            }

            return true;
        }
    }
}
