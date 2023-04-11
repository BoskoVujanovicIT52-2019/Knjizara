using Knjizara.Data;
using Knjizara.Interfaces;
using Knjizara.Model;

namespace Knjizara.Repository
{
    public class KnjigaRepository : IKnjigaRepository
    {

        private readonly DataContext _context;
        public KnjigaRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateKnjiga(Knjiga knjiga)
        {
            _context.Add(knjiga);
            return Save();
        }

        public bool DeleteKnjiga(Knjiga knjiga)
        {
            _context.Remove(knjiga);
            return Save();
        }

        public ICollection<Knjiga> GetAllKnjigas()
        {
           return _context.Knjigas.OrderBy(p => p.IdKnjige).ToList();
            
        }

        public Knjiga GetKnjigaByID(int id)
        {
            return _context.Knjigas.Where(p=>p.IdKnjige == id).FirstOrDefault();

        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved>0?true:false;
        }

        public bool UpdateKnjiga(Knjiga knjiga)
        {
            _context.Update(knjiga);
            return Save();
        }
    }
}
