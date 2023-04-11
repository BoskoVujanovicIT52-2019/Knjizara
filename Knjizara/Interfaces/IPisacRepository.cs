using Knjizara.Model;

namespace Knjizara.Interfaces
{
    public interface IPisacRepository
    {
        ICollection<Pisac> GetAllPisacs();
        Pisac GetPisacByID(int id);
        bool CreatePisac (Pisac pisac);
        bool UpdatePisac (Pisac pisac);
        bool DeletePisac (Pisac pisac);
        bool Save();
    }
}
