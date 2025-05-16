using EvidencijaNezaposlenih.ModeliPodataka.DTO;
using EvidencijaNezaposlenih.Servisi.Interfejsi;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Evidencijanezaposlenih.Interface.Controlers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NezaposleniKontroler : ControllerBase
    {
        private readonly INezaposleniServis _nezaposleniServis;

        public NezaposleniKontroler(INezaposleniServis nezaposleniServis)
        {
            _nezaposleniServis = nezaposleniServis;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NezaposleniPrikaz>>> GetAll()
        {
            var result = await _nezaposleniServis.DajSve();
            return Ok(result);
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<NezaposleniPrikaz>>> GetByName([FromQuery] string filter)
        {
            var result = await _nezaposleniServis.DajSvePoimenuIPrezimenu(filter);
            return Ok(result);
        }

        [HttpGet("{jmbg}")]
        public async Task<ActionResult<NezaposleniPrikaz>> GetByJMBG(string jmbg)
        {
            var result = await _nezaposleniServis.DajSvePoJMBGU(jmbg);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Create(NezaposleniUnos nezaposleniUnos)
        {
            await _nezaposleniServis.KreirajNezaposlenog(nezaposleniUnos);
            return CreatedAtAction(nameof(GetByJMBG), new { jmbg = nezaposleniUnos.JMBG }, nezaposleniUnos);
        }

        [HttpPut]
        public async Task<ActionResult> Update(NezaposleniUnos nezaposleniUnos)
        {
            await _nezaposleniServis.Azuriraj(nezaposleniUnos);
            return NoContent();
        }

        [HttpDelete("{jmbg}")]
        public async Task<ActionResult> Delete(string jmbg)
        {
            await _nezaposleniServis.Obrisi(jmbg);
            return NoContent();
        }
    }
}
