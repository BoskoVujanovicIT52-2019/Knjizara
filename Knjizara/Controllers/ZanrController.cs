using Knjizara.Interfaces;
using Knjizara.Model;
using Knjizara.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Knjizara.Controllers
{ 

    [Route("api/[controller]")]
    [ApiController]
public class ZanrController : Controller
{
    private readonly IZanrRepository _zanrRepository;
    public ZanrController(IZanrRepository zanrRepository)
    {
        _zanrRepository = zanrRepository;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Zanr>))]
    public IActionResult GetZanrs()
    {
        var zanrovi = _zanrRepository.GetAllZanrs();

        if (zanrovi == null)
        {
            return NotFound(); // Proveriti sintaksu 
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(zanrovi);
    }

    [HttpGet("{zanrID}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Zanr>))]
    [ProducesResponseType(400)]
    public IActionResult GetZanrByID(int zanrID)
    {

        var zanr = _zanrRepository.GetZanrByID(zanrID);
        if (zanr == null) return NotFound();
        if (!ModelState.IsValid) return BadRequest(ModelState);
        return Ok(zanr);
    }

    [HttpPost]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CreateZanr([FromBody] Zanr zanrCreate)
    {
        if (zanrCreate == null) return BadRequest(ModelState);
        var zanr = _zanrRepository.GetAllZanrs().Where(c => c.NazivZanr.Trim().ToUpper() == zanrCreate.NazivZanr.Trim().ToUpper()).FirstOrDefault();
        if (zanr != null)
        {
            ModelState.AddModelError("", "Zanr vec postoji");
            return StatusCode(422, ModelState);
        }
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!_zanrRepository.CreateZanr(zanrCreate))
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
    public IActionResult UpdateZanr(int zanrID, [FromBody] Zanr zanrUpdate)
    {
        if (zanrUpdate == null) return BadRequest(ModelState);
        if (zanrID != zanrUpdate.IdZanr) return BadRequest(ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!_zanrRepository.UpdateZanr(zanrUpdate))
        {
            ModelState.AddModelError("", "Nesto je poslo po zlu prilikom cuvanja.");
            return StatusCode(500, ModelState);
        }
        return NoContent();
    }

    [HttpDelete("{zanrID}")]
    [ProducesResponseType(400)]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public IActionResult DeleteZanr(int zanrID)
    {

        var zanrToDelete = _zanrRepository.GetZanrByID(zanrID);
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (_zanrRepository.GetZanrByID(zanrID) == null) return StatusCode(500, ModelState);
        if (!_zanrRepository.DeleteZanr(zanrToDelete))
        {
            ModelState.AddModelError("", "Nesto je poslo po zlu pri brisanju");
            return StatusCode(500, ModelState);
        }
        return NoContent();

    }

}
}
