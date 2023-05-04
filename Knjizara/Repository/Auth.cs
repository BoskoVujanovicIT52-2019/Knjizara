using Knjizara.Data;
using Knjizara.Interfaces;
using Knjizara.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Knjizara.Repository
{
    public class Auth : IAuth
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;


        public Auth(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public bool AuthenticateUser(Korisnik korisnikA)
        {
            if (korisnikA.Email != null)
            {
                var korisnik= _context.Korisniks.Where(e=>e.Email == korisnikA.Email).FirstOrDefault();
                if(korisnik==null)
                {
                    return false;
                }
                if(VerifyPassword(korisnikA.Lozinka, korisnik.Lozinka, korisnik.Salt))
                {
                    return true;
                }
            }
            return false;
        }

        public string GenerateJWT(String? korisnikID)
        {
            string? key = _configuration["JWT:Key"];
            if (key!=null)
            {
                Claim[] claims = null;
                Korisnik? korisnik = null;
                if(korisnikID!=null) { 
                    korisnik=_context.Korisniks.FirstOrDefault(e=>e.Email ==korisnikID);
                }
                if(korisnik!=null)
                {
                    claims = new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, korisnik.IdClana.ToString()),
						new Claim(ClaimTypes.Role, korisnik.ulogaClana),
						new Claim(ClaimTypes.GivenName, korisnik.Ime),
						new Claim(ClaimTypes.Surname, korisnik.Prezime),
						new Claim(ClaimTypes.Email, korisnik.Email)
                    };
                }

                SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(key));
                SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

                JwtSecurityToken token = new(_configuration["JWT:Issuer"],
                                        _configuration["JWT:Audience"],
                                        claims,
                                        expires: DateTime.Now.AddMinutes(120),
                                        signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            return string.Empty;
        }

        private static bool VerifyPassword(string password, string pwdHash, string pwdSalt)
        {
            if (password != null && pwdHash != null && pwdSalt != null)
            {
                var saltBytes = Convert.FromBase64String(pwdSalt);
                var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 1000);
                if (Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == pwdHash)
                    return true;
            }
            return false;
        }
    }
}
