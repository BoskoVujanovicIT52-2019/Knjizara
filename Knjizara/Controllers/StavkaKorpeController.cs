using Knjizara.Interfaces;
using Knjizara.Model;
using Knjizara.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Knjizara.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StavkaKorpreController : Controller
    {
        private readonly IStavkaKorpe _stavkaKorpeRepository;
        public StavkaKorpreController(IStavkaKorpe stavkaKorpeRepository)
        {
            _stavkaKorpeRepository = stavkaKorpeRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StavkaKorpe>))]
        public IActionResult GetStavkaKorpes()
        {
            var stavke = _stavkaKorpeRepository.GetAllStavkaKorpes();

            if (stavke == null)
            {
                return NotFound(); // Proveriti sintaksu 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(stavke);
        }

        [HttpGet("{stavkaKorpeID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StavkaKorpe>))]
        [ProducesResponseType(400)]
        public IActionResult GetSravkaKorpeByID(int stavkaKorpeID)
        {

            var stavkaKorpe = _stavkaKorpeRepository.GetStavkaKorpeByID(stavkaKorpeID);
            if (stavkaKorpe == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(stavkaKorpe);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateStavkaKorpe([FromBody] StavkaKorpe stavkaCreate)
        {
            if (stavkaCreate == null) return BadRequest(ModelState);
            var stavka = _stavkaKorpeRepository.GetAllStavkaKorpes().Where(c => c.IdKnjige == stavkaCreate.IdKnjige
                                                                                        && c.IdKorpa == stavkaCreate.IdKorpa).FirstOrDefault();
            if (stavka != null)
            {
                ModelState.AddModelError("", "Stavka vec postoji");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_stavkaKorpeRepository.CreateStavkaKorpe(stavkaCreate))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu pri cuvanju");
                return StatusCode(500, ModelState);
            }
            return Ok("Uspesno kreirano!");
        }



        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateStavka(int stavkaID, [FromBody] StavkaKorpe stavkaUpdate)
        {
            if (stavkaUpdate == null) return BadRequest(ModelState);
            if (stavkaID != stavkaUpdate.IdStavkaKorpe) return BadRequest(ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_stavkaKorpeRepository.UpdateStavkaKorpe(stavkaUpdate))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu prilikom cuvanja.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{stavkaID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteStavka(int stavkaID)
        {

            var stavkaToDelete = _stavkaKorpeRepository.GetStavkaKorpeByID(stavkaID);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_stavkaKorpeRepository.GetStavkaKorpeByID(stavkaID) == null) return StatusCode(500, ModelState);
            if (!_stavkaKorpeRepository.DeleteStavkaKorpe(stavkaToDelete))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu pri brisanju");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

    }
}

