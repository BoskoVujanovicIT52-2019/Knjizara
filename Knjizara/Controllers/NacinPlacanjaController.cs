using Knjizara.Interfaces;
using Knjizara.Model;
using Microsoft.AspNetCore.Mvc;

namespace Knjizara.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NacinPlacanjaController : Controller
    {
        private readonly INacinPlacanja _nacinPlacanja;
        public NacinPlacanjaController(INacinPlacanja nacinPlacanja)
        {
            _nacinPlacanja = nacinPlacanja;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<NacinPlacanja>))]
        public IActionResult GetNaciniPlacanja()
        {
            var nacini = _nacinPlacanja.GetAllnacinPlacanjas();

            if (nacini == null)
            {
                return NotFound(); // Proveriti sintaksu 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(nacini);
        }

        [HttpGet("{nacinID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<NacinPlacanja>))]
        [ProducesResponseType(400)]
        public IActionResult GetNacinPlacanjaByID(int nacinID)
        {

            var nacin = _nacinPlacanja.GetNacinPlacanjaByID(nacinID);
            if (nacin == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(nacin);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateNacin([FromBody] NacinPlacanja nacinCreate)
        {
            if (nacinCreate == null) return BadRequest(ModelState);
            var nacin = _nacinPlacanja.GetAllnacinPlacanjas().Where(c => c.NazivNacinPlacanja.Trim().ToUpper() == nacinCreate.NazivNacinPlacanja.Trim().ToUpper()).FirstOrDefault();
            if (nacin != null)
            {
                ModelState.AddModelError("", "Nacin vec postoji");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_nacinPlacanja.CreateNacinPlacanja(nacinCreate))
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
        public IActionResult UpdateNacinPlacanja(int nacinID, [FromBody] NacinPlacanja nacinUpdate)
        {
            if (nacinUpdate == null) return BadRequest(ModelState);
            if (nacinID != nacinUpdate.IdNacinPlacanja) return BadRequest(ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_nacinPlacanja.UpdateNacinPlacanja(nacinUpdate))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu prilikom cuvanja.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{nacinID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteNacinPlacanja(int nacinID)
        {

            var nacinToDelete = _nacinPlacanja.GetNacinPlacanjaByID(nacinID);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_nacinPlacanja.GetNacinPlacanjaByID(nacinID) == null) return StatusCode(500, ModelState);
            if (!_nacinPlacanja.DeleteNacinPlacanja(nacinToDelete))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu pri brisanju");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

    }
}