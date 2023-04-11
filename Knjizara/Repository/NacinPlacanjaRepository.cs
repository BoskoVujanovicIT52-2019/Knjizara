using Knjizara.Data;
using Knjizara.Interfaces;
using Knjizara.Model;

namespace Knjizara.Repository
{
    public class NacinPlacanjaRepository : INacinPlacanja
    {
        private readonly DataContext _context;
        public NacinPlacanjaRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateNacinPlacanja(NacinPlacanja nacinPlacanja)
        {
            _context.Add(nacinPlacanja);
            return Save();
        }

        public bool DeleteNacinPlacanja(NacinPlacanja nacinPlacanja)
        {
            _context.Remove(nacinPlacanja);
            return Save();
        }

        public ICollection<NacinPlacanja> GetAllnacinPlacanjas()
        {
            return _context.NacinPlacanjas.OrderBy(p=>p.IdNacinPlacanja).ToList();
        }

        public NacinPlacanja GetNacinPlacanjaByID(int id)
        {
            return _context.NacinPlacanjas.Where(p=>p.IdNacinPlacanja==id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved>0?true:false;
        }

        public bool UpdateNacinPlacanja(NacinPlacanja nacinPlacanja)
        {
            _context.Update(nacinPlacanja);
            return Save();
        }
    }
}
