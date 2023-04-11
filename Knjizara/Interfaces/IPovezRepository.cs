using Knjizara.Model;

namespace Knjizara.Interfaces
{
    public interface IPovezRepository
    {
        ICollection<Povez> GetAllPovezs();
        Povez GetPovezByID(int id);
        bool CreatePovez(Povez povez);
        bool DeletePovez(Povez povez);
        bool UpdatePovez(Povez povez);
        bool Save();
    }
}
