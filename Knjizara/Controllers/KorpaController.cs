using Knjizara.Interfaces;
using Knjizara.Model;
using Microsoft.AspNetCore.Mvc;

namespace Knjizara.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorpaController : Controller
    {
        private readonly IKorpaRepository _korpaRepository;
        public KorpaController(IKorpaRepository korpaRepository)
        {
            _korpaRepository = korpaRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Korpa>))]
        public IActionResult GetKorpas()
        {
            var korpe = _korpaRepository.GetAllKorpas();

            if (korpe == null)
            {
                return NotFound(); // Proveriti sintaksu 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(korpe);
        }

        [HttpGet("{korpaID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Korisnik>))]
        [ProducesResponseType(400)]
        public IActionResult GetKorpaByID(int korpaID)
        {

            var korpa = _korpaRepository.GetKorpaByID(korpaID);
            if (korpa == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(korpa);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateKorpa([FromBody] Korpa korpaCreate)
        {
            if (korpaCreate == null) return BadRequest(ModelState);
            var korpa = _korpaRepository.GetAllKorpas().Where(c => c.IdKorisnik == korpaCreate.IdKorisnik).FirstOrDefault();
            if (korpa != null)
            {
                ModelState.AddModelError("", "Korpa vec postoji");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_korpaRepository.CreateKorpa(korpaCreate))
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
        public IActionResult UpdateKorpa(int korpaID, [FromBody] Korpa korpaUpdate)
        {
            if (korpaUpdate == null) return BadRequest(ModelState);
            if (korpaID != korpaUpdate.IdKorpa) return BadRequest(ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_korpaRepository.UpdateKorpa(korpaUpdate))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu prilikom cuvanja.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{korpaID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteKorpa(int korpaID)
        {

            var korpaToDelete = _korpaRepository.GetKorpaByID(korpaID);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_korpaRepository.GetKorpaByID(korpaID) == null) return StatusCode(500, ModelState);
            if (!_korpaRepository.DeleteKorpa(korpaToDelete))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu pri brisanju");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

    }
}
