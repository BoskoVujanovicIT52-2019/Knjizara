using Knjizara.Data;
using Knjizara.Interfaces;
using Knjizara.Model;

namespace Knjizara.Repository
{
    public class PisacRepository : IPisacRepository
    {
        private readonly DataContext _context;
        public PisacRepository(DataContext context) {
            _context = context;
        }
        public bool CreatePisac(Pisac pisac)
        {
            _context.Add(pisac);
            return Save();
        }

        public bool DeletePisac(Pisac pisac)
        {
            _context.Remove(pisac);
            return Save();
        }

        public ICollection<Pisac> GetAllPisacs()
        {
            return _context.Pisacs.OrderBy(p=>p.IdPisca).ToList();
        }

        public Pisac GetPisacByID(int id)
        {
            return _context.Pisacs.Where(p => p.IdPisca == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved>0?true:false;
        }

        public bool UpdatePisac(Pisac pisac)
        {
            _context.Pisacs.Update(pisac); 
            return Save();
        }
    }
}
