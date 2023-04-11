using Knjizara.Data;
using Knjizara.Interfaces;
using Knjizara.Model;
using System.Runtime.CompilerServices;

namespace Knjizara.Repository
{
    public class PovezRepository : IPovezRepository
    {
        private readonly DataContext _context;
        public PovezRepository(DataContext context) {
        _context= context;
        }
        public bool CreatePovez(Povez povez)
        {
            _context.Add(povez);
            return Save();
        }

        public bool DeletePovez(Povez povez)
        {
            _context.Remove(povez);
            return Save();
        }

        public ICollection<Povez> GetAllPovezs()
        {
            return _context.Povezs.OrderBy(p=>p.IdPovez).ToList();
        }

        public Povez GetPovezByID(int id)
        {
            return _context.Povezs.Where(p => p.IdPovez == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePovez(Povez povez)
        {
            _context.Update(povez);
            return Save();
        }
    }
}
