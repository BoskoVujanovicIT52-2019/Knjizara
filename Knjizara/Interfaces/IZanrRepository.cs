using Knjizara.Model;

namespace Knjizara.Interfaces
{
    public interface IZanrRepository
    {
        ICollection<Zanr> GetAllZanrs();
        Zanr GetZanrByID(int id);
        bool CreateZanr(Zanr zanr);
        bool UpdateZanr(Zanr zanr);
        bool Save();
        bool DeleteZanr(Zanr zanr);
    }
}
