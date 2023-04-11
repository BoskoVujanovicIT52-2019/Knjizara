using Knjizara.Model;
using Knjizara.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Knjizara.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IzdavacController : Controller
    {
        private readonly IIzdavacRepository _izdavacRepository;
        public IzdavacController(IIzdavacRepository izdavacRepository)
        {
           _izdavacRepository= izdavacRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Izdavac>))]
        public IActionResult GetIzdavaci()
        {
            var izdavaci = _izdavacRepository.GetAllIzdavac();

            if(izdavaci == null)
            {
                return NotFound(); // Proveriti sintaksu 
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            return Ok(izdavaci);
        }

        [HttpGet("{izdavacID}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Izdavac>))]
        [ProducesResponseType(400)]
        public IActionResult GetIzdavacByID(int izdavacID) {

            var izdavac = _izdavacRepository.GetIzdavacById(izdavacID);
            if (izdavac == null) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(izdavac);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateIzdavac([FromBody] Izdavac izdavacCreate)
        {
            if(izdavacCreate==null) return BadRequest(ModelState);
            var izdavac = _izdavacRepository.GetAllIzdavac().Where(c => c.NazivIzdavaca.Trim().ToUpper() == izdavacCreate.NazivIzdavaca.Trim().ToUpper()).FirstOrDefault();
            if(izdavac!=null) {
                ModelState.AddModelError("", "Izdavac vec postoji");
                return StatusCode(422, ModelState);
            }
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);

            if(!_izdavacRepository.CreateIzdavac(izdavacCreate))
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
        public IActionResult UpdateIzdavac(int izdavacID, [FromBody] Izdavac izdavacUpdate)
        {
            if(izdavacUpdate==null) return BadRequest(ModelState);
            if(izdavacID!=izdavacUpdate.IdIzdavac) return BadRequest(ModelState);
            if(!ModelState.IsValid) { 
                return BadRequest(ModelState);
            }
            if (!_izdavacRepository.UpdateIzdavac(izdavacUpdate))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu prilikom cuvanja.");
                return StatusCode(500, ModelState) ;
            }
            return NoContent();
        }

        [HttpDelete("{izdavacID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteIzdavac(int izdavacID) {
        
            var izdavacToDelete= _izdavacRepository.GetIzdavacById(izdavacID);
            if(!ModelState.IsValid) return BadRequest(ModelState);
            if(_izdavacRepository.GetIzdavacById(izdavacID)==null) return StatusCode(500, ModelState);
            if(!_izdavacRepository.DeleteIzdavac(izdavacToDelete))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu pri brisanju");
                return StatusCode(500, ModelState) ;
            }
            return NoContent();
        
        }

    }
}
