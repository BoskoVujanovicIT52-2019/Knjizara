using Knjizara.Data;
using Knjizara.Interfaces;
using Knjizara.Model;

namespace Knjizara.Repository
{
    public class RacunRepository : IRacunRepository
    {
        private readonly DataContext _context;
        public RacunRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateRacun(Racun racun)
        {
            _context.Add(racun);
            return Save();
        }

        public bool DeleteRacun(Racun racun)
        {
            _context.Remove(racun);
            return Save();
        }

        public ICollection<Racun> GetAllRacuns()
        {
            return _context.Racuns.OrderBy(p=>p.IdRacun).ToList();
        }

        public Racun GetRacunByID(int id)
        {
            return _context.Racuns.Where(p => p.IdRacun == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateRacun(Racun racun)
        {
            _context.Update(racun);
            return Save();
        }
    }
}
