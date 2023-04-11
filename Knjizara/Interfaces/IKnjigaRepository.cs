using Knjizara.Model;

namespace Knjizara.Interfaces
{
    public interface IKnjigaRepository
    {
        ICollection<Knjiga> GetAllKnjigas();
        Knjiga GetKnjigaByID(int id);
        bool CreateKnjiga(Knjiga knjiga);
        bool Save();
        bool UpdateKnjiga(Knjiga knjiga);
        bool DeleteKnjiga(Knjiga knjiga);
    }
}
