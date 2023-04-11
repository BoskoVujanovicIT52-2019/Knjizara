using Knjizara.Model;

namespace Knjizara.Interfaces
{
    public interface IStavkaKorpe
    {
        ICollection<StavkaKorpe> GetAllStavkaKorpes();
        StavkaKorpe GetStavkaKorpeByID(int id);
        bool CreateStavkaKorpe(StavkaKorpe stavkaKorpe);
        bool UpdateStavkaKorpe(StavkaKorpe stavkaKorpe);
        bool DeleteStavkaKorpe(StavkaKorpe stavkaKorpe);
        bool Save();
    }
}
