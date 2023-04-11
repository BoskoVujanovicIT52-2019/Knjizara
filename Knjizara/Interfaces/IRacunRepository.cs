using Knjizara.Model;

namespace Knjizara.Interfaces
{
    public interface IRacunRepository
    {
        ICollection<Racun> GetAllRacuns();
        Racun GetRacunByID(int id);
        bool CreateRacun(Racun racun);
        bool Save();
        bool DeleteRacun(Racun racun);
        bool UpdateRacun(Racun racun);
    }
}
