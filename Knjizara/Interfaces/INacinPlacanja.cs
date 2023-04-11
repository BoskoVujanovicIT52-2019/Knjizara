using Knjizara.Model;

namespace Knjizara.Interfaces
{
    public interface INacinPlacanja
    {
        ICollection<NacinPlacanja> GetAllnacinPlacanjas();
        NacinPlacanja GetNacinPlacanjaByID(int id);
        bool CreateNacinPlacanja(NacinPlacanja nacinPlacanja);
        bool Save();
        bool UpdateNacinPlacanja(NacinPlacanja nacinPlacanja);
        bool DeleteNacinPlacanja(NacinPlacanja nacinPlacanja);
    }
}
