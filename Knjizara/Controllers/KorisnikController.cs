using Knjizara.Interfaces;
using Knjizara.Model;
using Microsoft.AspNetCore.Mvc;

namespace Knjizara.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class KorisnikController : Controller
    {
        private readonly IKorisnikRepository _korisnikRepository;
        public KorisnikController(IKorisnikRepository korisnikRepository)
        {
            _korisnikRepository = korisnikRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Korisnik>))]
        public IActionResult GetKorisnici()
        {
            var korisnici = _korisnikRepository.GetAllKorisnik();

            if (korisnici == null)
            {
                return NotFound(); // Proveriti sintaksu 
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(korisnici);
        }

        [HttpGet("{korisnikID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Korisnik>))]
        [ProducesResponseType(400)]
        public IActionResult GetKorisnikByID(int korisnikID)
        {

            var korisnik = _korisnikRepository.GetKorisnikByID(korisnikID);
            if (korisnik == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(korisnik);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateKnjiga([FromBody] Korisnik korisnikCreate)
        {
            if (korisnikCreate == null) return BadRequest(ModelState);
            var korisnik = _korisnikRepository.GetAllKorisnik().Where(c => c.Telefon.Trim().ToUpper() == korisnikCreate.Telefon.Trim().ToUpper()).FirstOrDefault();
            if (korisnik != null)
            {
                ModelState.AddModelError("", "Korisnik vec postoji");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_korisnikRepository.CreateKorisnik(korisnikCreate))
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
        public IActionResult UpdateKorisnik(int korisnikID, [FromBody] Korisnik korisnikUpdate)
        {
            if (korisnikUpdate == null) return BadRequest(ModelState);
            if (korisnikID != korisnikUpdate.IdClana) return BadRequest(ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_korisnikRepository.UpdateKorisnik(korisnikUpdate))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu prilikom cuvanja.");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{korisnikID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteKorisnik(int korisnikID)
        {

            var korisnikToDelete = _korisnikRepository.GetKorisnikByID(korisnikID);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (_korisnikRepository.GetKorisnikByID(korisnikID) == null) return StatusCode(500, ModelState);
            if (!_korisnikRepository.DeleteKorisnik(korisnikToDelete))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu pri brisanju");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

    }
}
