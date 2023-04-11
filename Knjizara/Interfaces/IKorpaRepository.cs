using Knjizara.Model;

namespace Knjizara.Interfaces
{
    public interface IKorpaRepository
    {
        ICollection<Korpa> GetAllKorpas();
        Korpa GetKorpaByID(int id);
        bool CreateKorpa(Korpa korpa);
        bool UpdateKorpa(Korpa korpa);
        bool DeleteKorpa(Korpa korpa);
        bool Save();
    }
}
