using Knjizara.Interfaces;
using Knjizara.Model;
using Microsoft.AspNetCore.Mvc;

namespace Knjizara.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RacunController : Controller
    {
        private readonly IRacunRepository _racunRepository;
        public RacunController(IRacunRepository racunRepository)
        {
            _racunRepository = racunRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Racun>))]
        public IActionResult GetRacuni()
        {
            var racuni = _racunRepository.GetAllRacuns();

            if (racuni == null)
            {
                return NotFound(); // Proveriti sintaksu 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(racuni);
        }

        [HttpGet("{racunID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Racun>))]
        [ProducesResponseType(400)]
        public IActionResult GetRacunByID(int racunID)
        {

            var racun = _racunRepository.GetRacunByID(racunID);
            if (racun == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(racun);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRacun([FromBody] Racun racunCreate)
        {
            if (racunCreate == null) return BadRequest(ModelState);
            var racun = _racunRepository.GetAllRacuns().Where(c => c.IdKorisnik == racunCreate.IdKorisnik &&
                                                                    c.Datum==racunCreate.Datum &&
                                                                    c.Vreme== racunCreate.Vreme).FirstOrDefault();
            if (racun != null)
            {
                ModelState.AddModelError("", "Racun vec postoji");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_racunRepository.CreateRacun(racunCreate))
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
        public IActionResult UpdateRacun(int racunID, [FromBody] Racun racunUpdate)
        {
            if (racunUpdate == null) return BadRequest(ModelState);
            if (racunID != racunUpdate.IdRacun) return BadRequest(ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_racunRepository.UpdateRacun(racunUpdate))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu prilikom cuvanja.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{racunID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteRacun(int racunID)
        {

            var racunToDelete = _racunRepository.GetRacunByID(racunID);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_racunRepository.GetRacunByID(racunID) == null) return StatusCode(500, ModelState);
            if (!_racunRepository.DeleteRacun(racunToDelete))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu pri brisanju");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

    }
}
