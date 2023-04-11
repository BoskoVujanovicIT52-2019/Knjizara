using Knjizara.Data;
using Knjizara.Interfaces;
using Knjizara.Model;

namespace Knjizara.Repository
{
    public class TipKorisnikaRepository : ITipKorisnika
    {
        private readonly DataContext _context;
        public TipKorisnikaRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateTipKorisnika(TipKorisnika tipKorisnika)
        {
            _context.Add(tipKorisnika);
            return Save();
        }

        public bool DeleteTipKorisnika(TipKorisnika tipKorisnika)
        {
            _context.Remove(tipKorisnika); 
            return Save();
        }

        public ICollection<TipKorisnika> GetallTipKorisnikas()
        {
            return _context.TipKorisnikas.OrderBy(p=>p.IdTipKorisnika).ToList();
        }

        public TipKorisnika GetTipKorisnikaByID(int id)
        {
            return _context.TipKorisnikas.Where(p => p.IdTipKorisnika == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateTipKorisnika(TipKorisnika tipKorisnika)
        {
            _context.Update(tipKorisnika);
            return Save();
        }
    }
}
