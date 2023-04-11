using Knjizara.Data;
using Knjizara.Interfaces;
using Knjizara.Model;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Knjizara.Repository
{
    public class KorisnikRepository : IKorisnikRepository
    {
        private readonly DataContext _context;
        public KorisnikRepository(DataContext context) {
            _context = context;    
        }
        public bool CreateKorisnik(Korisnik korisnik)
        {
            _context.Add(korisnik);
            return Save();
        }

        public bool DeleteKorisnik(Korisnik korisnik)
        {
            _context.Remove(korisnik);
            return Save();
        }

        public ICollection<Korisnik> GetAllKorisnik()
        {
            return _context.Korisniks.OrderBy(p=>p.IdClana).ToList();
            
        }

        public Korisnik GetKorisnikByID(int id)
        {
            return _context.Korisniks.Where(p => p.IdClana == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved>0 ? true : false;
        }

        public bool UpdateKorisnik(Korisnik korisnik)
        {
            _context.Update(korisnik);
            return Save();
        }
    }
}
