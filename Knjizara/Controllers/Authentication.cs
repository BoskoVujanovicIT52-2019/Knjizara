using AutoMapper;
using Knjizara.Interfaces;
using Knjizara.Model;
using Knjizara.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace Knjizara.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authentication : Controller

    {
        public static Korisnik korisnik = new Korisnik();

        IKorisnikRepository _korisnikRepository;
        private readonly IMapper _mapper;

        public Authentication(IKorisnikRepository korisnikRepository, IMapper mapper)
        {
            _korisnikRepository = korisnikRepository;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        public IActionResult Registration([FromBody] KorisnikRegDTO korisnikReg)
        {
            var korisnik = _mapper.Map<Korisnik>(_korisnikRepository.GetAllKorisnik().Where(e => e.Email == korisnikReg.Email).FirstOrDefault());
          // var korisnik= _korisnikRepository.GetAllKorisnik().Where(e=>e.Email==korisnikReg.Email).FirstOrDefault();
            if (korisnik != null) {
                ModelState.AddModelError("", "Email je vec u upotrebi.");
                return StatusCode(409, ModelState);
            }

            var korisnikMap = _mapper.Map<Korisnik>(korisnikReg);

            var temp = korisnikMap;
            temp.Lozinka = BCrypt.Net.BCrypt.HashPassword(korisnikMap.Lozinka);

            if(!_korisnikRepository.CreateKorisnik(temp))
            {
                ModelState.AddModelError("", "Nesto je poslo po zlu pri cuvanju.");
                return StatusCode(500, ModelState);
            }

            return Ok("Uspesno registrovan korisnik.");
        }


        [HttpPost("Login")]
        public IActionResult Login([FromBody] KorisnikLogDTO korisnikLogin)
        {
            var korisnik = _mapper.Map<Korisnik>(_korisnikRepository.GetAllKorisnik().Where(e => e.Email == korisnikLogin.Email).FirstOrDefault());
            Console.WriteLine(korisnik);
            if(korisnik != null)
            {
                ModelState.AddModelError("", "Ne postoji korisnik sa unetom email adresom");
                return StatusCode(409, ModelState);
            }

            var korisnikMap = _mapper.Map<Korisnik>(korisnikLogin);
            //var temp = BCrypt.Net.BCrypt.HashPassword(korisnikLogin.Lozinka);
            var temp = korisnikLogin.Lozinka;

            if(!BCrypt.Net.BCrypt.Verify(korisnikMap.Lozinka,temp)) {
                return BadRequest("Pogresna Lozinka!");
            }

            return Ok(korisnik);
        }


    }
}
