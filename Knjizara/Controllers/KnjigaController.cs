using Knjizara.Interfaces;
using Knjizara.Model;
using Knjizara.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Knjizara.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KnjigaController : Controller
    {
        private readonly IKnjigaRepository _knjigaRepository;
        public KnjigaController(IKnjigaRepository knjigaRepository)
        {
            _knjigaRepository = knjigaRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Knjiga>))]
        public IActionResult GetKnjige()
        {
            var knjige = _knjigaRepository.GetAllKnjigas();

            if (knjige == null)
            {
                return NotFound(); // Proveriti sintaksu 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(knjige);
        }

        [HttpGet("{knjigaID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Knjiga>))]
        [ProducesResponseType(400)]
        public IActionResult GetKnjigaByID(int knjigaID)
        {

            var knjiga = _knjigaRepository.GetKnjigaByID(knjigaID);
            if (knjiga == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(knjiga);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateKnjiga([FromBody] Knjiga knjigaCreate)
        {
            if (knjigaCreate == null) return BadRequest(ModelState);
            var knjiga = _knjigaRepository.GetAllKnjigas().Where(c => c.NazivKnjige.Trim().ToUpper() == knjigaCreate.NazivKnjige.Trim().ToUpper()).FirstOrDefault();
            if (knjiga != null)
            {
                ModelState.AddModelError("", "Knjiga vec postoji");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_knjigaRepository.CreateKnjiga(knjigaCreate))
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
        public IActionResult UpdateKnjiga(int knjigaID, [FromBody] Knjiga knjigaUpdate)
        {
            if (knjigaUpdate == null) return BadRequest(ModelState);
            if (knjigaID != knjigaUpdate.IdKnjige) return BadRequest(ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_knjigaRepository.UpdateKnjiga(knjigaUpdate))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu prilikom cuvanja.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{knjigaID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteKnjiga(int knjigaID)
        {

            var knjigaToDelete = _knjigaRepository.GetKnjigaByID(knjigaID);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_knjigaRepository.GetKnjigaByID(knjigaID) == null) return StatusCode(500, ModelState);
            if (!_knjigaRepository.DeleteKnjiga(knjigaToDelete))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu pri brisanju");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

    }
}
