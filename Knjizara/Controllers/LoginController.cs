using AutoMapper;
using Knjizara.Data;
using Knjizara.Model;
using Knjizara.Model.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Knjizara.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public LoginController(IConfiguration config, DataContext context, IMapper mapper)
        {
            _config = config;
            _context = context;
            _mapper = mapper;

        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] KorisnikLogDTO korisnikLog)
        {

            var korisnik = Autheniticate(korisnikLog);
                if(korisnik != null) {

                var token = Generate(korisnik);
                return Ok(token);
            }
                return NotFound("User not found");
        }

        private Korisnik Autheniticate(KorisnikLogDTO korisnikLog)
        {
            Korisnik korisnik = _context.Korisniks.FirstOrDefault(e=>e.Email==korisnikLog.Email &&
                                                              e.Lozinka==korisnikLog.Lozinka);

            if(korisnik!= null)
            {
                return korisnik;
            }
            return null;
        }

        private object Generate(Korisnik korisnik)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, korisnik.Email),
                new Claim(ClaimTypes.GivenName, korisnik.Ime),
                new Claim(ClaimTypes.Surname, korisnik.Prezime),
                new Claim(ClaimTypes.Role, korisnik.ulogaClana)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
    }
}
