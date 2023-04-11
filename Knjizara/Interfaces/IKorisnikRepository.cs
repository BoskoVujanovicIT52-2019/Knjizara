using Knjizara.Model;

namespace Knjizara.Interfaces
{
    public interface IKorisnikRepository
    {
        ICollection<Korisnik> GetAllKorisnik();
        Korisnik GetKorisnikByID(int id);
        bool CreateKorisnik(Korisnik korisnik);
        bool UpdateKorisnik(Korisnik korisnik);
        bool DeleteKorisnik(Korisnik korisnik);
        bool Save();

    }
}
