using Knjizara.Interfaces;
using Knjizara.Model;
using Knjizara.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Knjizara.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipKorisnikaController : Controller
    {
        private readonly ITipKorisnika _tipKorisnikaRepository;
        public TipKorisnikaController(ITipKorisnika tipKorisnika)
        {
                _tipKorisnikaRepository= tipKorisnika;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TipKorisnika>))]
        public IActionResult GetTipKorisnika()
        {
            var tipovi = _tipKorisnikaRepository.GetallTipKorisnikas();

            if (tipovi == null)
            {
                return NotFound(); // Proveriti sintaksu 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(tipovi);
        }

        [HttpGet("{tipID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TipKorisnika>))]
        [ProducesResponseType(400)]
        public IActionResult GetTipByID(int tipID)
        {

            var tip = _tipKorisnikaRepository.GetTipKorisnikaByID(tipID);
            if (tip == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(tip);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateTip([FromBody] TipKorisnika tipCreate)
        {
            if (tipCreate == null) return BadRequest(ModelState);
            var tip = _tipKorisnikaRepository.GetallTipKorisnikas().Where(c => c.NazivTipaKorisnika.Trim().ToUpper() == tipCreate.NazivTipaKorisnika.Trim().ToUpper()).FirstOrDefault();
            if (tip != null)
            {
                ModelState.AddModelError("", "Tip korisnika vec postoji");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_tipKorisnikaRepository.CreateTipKorisnika(tip))
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
        public IActionResult UpdateTip(int tipID, [FromBody] TipKorisnika tipUpdate)
        {
            if (tipUpdate == null) return BadRequest(ModelState);
            if (tipID != tipUpdate.IdTipKorisnika) return BadRequest(ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_tipKorisnikaRepository.UpdateTipKorisnika(tipUpdate))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu prilikom cuvanja.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{tipID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteTipKorisnika(int tipID)
        {

            var tipToDelete = _tipKorisnikaRepository.GetTipKorisnikaByID(tipID);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_tipKorisnikaRepository.GetTipKorisnikaByID(tipID) == null) return StatusCode(500, ModelState);
            if (!_tipKorisnikaRepository.DeleteTipKorisnika(tipToDelete))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu pri brisanju");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

    }
}
