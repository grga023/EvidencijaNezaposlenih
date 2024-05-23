


    using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.Servisi.Interfejsi;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EvidencijaNezaposlenih.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FirmaKontroler : ControllerBase
    {
        private readonly IPoslodavacServis _poslodavacServis;

        public FirmaKontroler(IPoslodavacServis poslodavacServis)
        {
            _poslodavacServis = poslodavacServis;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PoslodavacPrikaz>>> GetAll()
        {
            var result = await _poslodavacServis.DajSve();
            return Ok(result);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<PoslodavacPrikaz>>> GetByName([FromQuery] string naziv)
        {
            var result = await _poslodavacServis.DajSvePoNazivu(naziv);
            return Ok(result);
        }

        [HttpGet("{pib}")]
        public async Task<ActionResult<PoslodavacPrikaz>> GetByPIB(string pib)
        {
            var result = await _poslodavacServis.DajSvePoPIB(pib);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(PoslodavacUnos poslodavacUnos)
        {
            await _poslodavacServis.KreirajPoslodavca(poslodavacUnos);
            return CreatedAtAction(nameof(GetByPIB), new { pib = poslodavacUnos.PIB }, poslodavacUnos);
        }

        [HttpPut]
        public async Task<ActionResult> Update(NezaposleniIzmena nezaposleniIzmena)
        {
            await _poslodavacServis.Azuriraj(nezaposleniIzmena);
            return NoContent();
        }

        [HttpDelete("{pib}")]
        public async Task<ActionResult> Delete(string pib)
        {
            await _poslodavacServis.Obrisi(pib);
            return NoContent();
        }
    }
}
