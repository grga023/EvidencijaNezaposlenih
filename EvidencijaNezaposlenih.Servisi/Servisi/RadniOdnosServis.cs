using EvidencijaNezaposlenih.ModeliPodataka.Modeli;
using EvidencijaNezaposlenih.Repozitorijum.Interfejsi;
using EvidencijaNezaposlenih.Servisi.Interfejsi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.Servisi.Servisi
{
    public class RadniOdnosServis : IRadniOdnosServis
    {
        private readonly IRadniOdnosRepozitorijum _radniOdnosRepozitorijum;
        private readonly IPoslodavacRepozitorijum _poslodavacRepozitorijum;
        private readonly INezaposleniRepozitorijum _nezaposleniRepozitorijum;
        public RadniOdnosServis(IRadniOdnosRepozitorijum radniOdnosRepozitorijum, IPoslodavacRepozitorijum poslodavacRepozitorijum, INezaposleniRepozitorijum nezaposleniRepozitorijum)
        {
            _radniOdnosRepozitorijum = radniOdnosRepozitorijum;
            _nezaposleniRepozitorijum = nezaposleniRepozitorijum ;
            _poslodavacRepozitorijum = poslodavacRepozitorijum ;
        }
        public void Azuriraj(RadniOdnos obj)
        {
            _radniOdnosRepozitorijum.Izmeni(obj);
            _radniOdnosRepozitorijum.Snimi();
        }

        public Task<IEnumerable<RadniOdnos>> DajSve()
        {
            throw new NotImplementedException();
        }

        public Task<RadniOdnos> DajSvePoID(object PK_P, object PK_N)
        {
            throw new NotImplementedException();
        }

        public async Task Obrisi(object nazivP, object JMBG)
        {
            var poslodavac = await _poslodavacRepozitorijum.PronadjiPoNazivu(nazivP);
            var nezaposleni = await _nezaposleniRepozitorijum.DajSvePoJMBG(JMBG);

            var PK = nezaposleni.ID + " " + poslodavac.ID;
            await _radniOdnosRepozitorijum.Obrisi(PK);
            _radniOdnosRepozitorijum.Snimi();
        }
    }
}
