using Knjizara.Interfaces;
using Knjizara.Model;
using Microsoft.AspNetCore.Mvc;

namespace Knjizara.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PisacController : Controller
    {
        private readonly IPisacRepository _pisacRepository;
        public PisacController(IPisacRepository pisacRepository)
        {
            _pisacRepository = pisacRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pisac>))]
        public IActionResult GetPisci()
        {
            var pisci = _pisacRepository.GetAllPisacs();

            if (pisci == null)
            {
                return NotFound(); // Proveriti sintaksu 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(pisci);
        }

        [HttpGet("{pisacID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pisac>))]
        [ProducesResponseType(400)]
        public IActionResult GetPisacByID(int pisacID)
        {

            var pisac = _pisacRepository.GetPisacByID(pisacID);
            if (pisac == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(pisac);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CratePisac([FromBody] Pisac pisacCreate)
        {
            if (pisacCreate == null) return BadRequest(ModelState);
            var pisac = _pisacRepository.GetAllPisacs().Where(c => c.Ime.Trim().ToUpper() == pisacCreate.Ime.Trim().ToUpper() &&
                                                                c.Prezime.Trim().ToUpper() == pisacCreate.Prezime.Trim().ToUpper()).FirstOrDefault();
            if (pisac != null)
            {
                ModelState.AddModelError("", "Pisac vec postoji");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_pisacRepository.CreatePisac(pisacCreate))
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
        public IActionResult UpdatePisac(int pisacID, [FromBody] Pisac pisacUpdate)
        {
            if (pisacUpdate == null) return BadRequest(ModelState);
            if (pisacID != pisacUpdate.IdPisca) return BadRequest(ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_pisacRepository.UpdatePisac(pisacUpdate))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu prilikom cuvanja.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{pisacID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePisac(int pisacID)
        {

            var pisacToDelete = _pisacRepository.GetPisacByID(pisacID);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_pisacRepository.GetPisacByID(pisacID) == null) return StatusCode(500, ModelState);
            if (!_pisacRepository.DeletePisac(pisacToDelete))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu pri brisanju");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

    }
}