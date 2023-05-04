using Knjizara.Model;

namespace Knjizara.Interfaces
{
    public interface IAuth
    {

        public bool AuthenticateUser(Korisnik korisnik);


        public string GenerateJWT(String? userIdentity);
    }
}
