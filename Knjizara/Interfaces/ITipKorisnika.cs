using Knjizara.Model;

namespace Knjizara.Interfaces
{
    public interface ITipKorisnika
    {
        ICollection<TipKorisnika> GetallTipKorisnikas();
        TipKorisnika GetTipKorisnikaByID(int id);
        bool CreateTipKorisnika(TipKorisnika tipKorisnika);
        bool UpdateTipKorisnika(TipKorisnika tipKorisnika);
        bool DeleteTipKorisnika(TipKorisnika tipKorisnika);
        bool Save();
    }
}
