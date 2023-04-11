using Knjizara.Interfaces;
using Knjizara.Model;
using Knjizara.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Knjizara.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PovezController : Controller
    {
        private readonly IPovezRepository _povezRepository;
        public PovezController(IPovezRepository povezRepository)
        {
            _povezRepository = povezRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Izdavac>))]
        public IActionResult GetPovezi()
        {
            var povezi = _povezRepository.GetAllPovezs();

            if (povezi == null)
            {
                return NotFound(); // Proveriti sintaksu 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(povezi);
        }

        [HttpGet("{povezID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Povez>))]
        [ProducesResponseType(400)]
        public IActionResult GetPovezByID(int povezID)
        {

            var povez = _povezRepository.GetPovezByID(povezID);
            if (povez == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(povez);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePovez([FromBody] Povez povezCreate)
        {
            if (povezCreate == null) return BadRequest(ModelState);
            var povez = _povezRepository.GetAllPovezs().Where(c => c.NazivPoveza.Trim().ToUpper() == povezCreate.NazivPoveza.Trim().ToUpper()).FirstOrDefault();
            if (povez != null)
            {
                ModelState.AddModelError("", "Povez vec postoji");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_povezRepository.CreatePovez(povezCreate))
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
        public IActionResult UpdatePovez(int povezID, [FromBody] Povez povezUpdate)
        {
            if (povezUpdate == null) return BadRequest(ModelState);
            if (povezID != povezUpdate.IdPovez) return BadRequest(ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_povezRepository.UpdatePovez(povezUpdate))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu prilikom cuvanja.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{povezID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePovez(int povezID)
        {

            var povezToDelete = _povezRepository.GetPovezByID(povezID);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_povezRepository.GetPovezByID(povezID) == null) return StatusCode(500, ModelState);
            if (!_povezRepository.DeletePovez(povezToDelete))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu pri brisanju");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

    }
}
