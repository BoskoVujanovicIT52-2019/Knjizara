using Knjizara.Model;

namespace Knjizara.Repository
{
    public interface IIzdavacRepository
    {
        ICollection<Izdavac> GetAllIzdavac();
        Izdavac GetIzdavacById(int id);
        bool CreateIzdavac(Izdavac izdavac);
        bool Save();
        bool UpdateIzdavac(Izdavac izdavac);
        bool DeleteIzdavac(Izdavac izdavac);
    }
}


