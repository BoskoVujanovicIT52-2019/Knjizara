using Knjizara.Data;
using Knjizara.Interfaces;
using Knjizara.Model;

namespace Knjizara.Repository
{
    public class ZanrRepository : IZanrRepository
    {
        private readonly DataContext _context;
        public ZanrRepository(DataContext context)
        {
            _context = context;

        }
        public bool CreateZanr(Zanr zanr)
        {
            _context.Add(zanr);
            return Save();
        }

        public bool DeleteZanr(Zanr zanr)
        {
            _context.Remove(zanr);
            return Save();
        }

        public ICollection<Zanr> GetAllZanrs()
        {
            return _context.Zanrs.OrderBy(p=>p.IdZanr).ToList();
        }

        public Zanr GetZanrByID(int id)
        {
            return _context.Zanrs.Where(p => p.IdZanr == id).FirstOrDefault();
        }

        public bool Save()
        {
           var saved = _context.SaveChanges();
            return saved>0?true:false;
        }

        public bool UpdateZanr(Zanr zanr)
        {
            _context.Update(zanr);
            return Save();
        }
    }
}
