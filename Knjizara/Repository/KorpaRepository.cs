using Knjizara.Data;
using Knjizara.Interfaces;
using Knjizara.Model;

namespace Knjizara.Repository
{
    public class KorpaRepository : IKorpaRepository
    {
        private readonly DataContext _context;
            public KorpaRepository(DataContext context)
        {
            _context= context;
        }
        public bool CreateKorpa(Korpa korpa)
        {
            _context.Add(korpa);
            return Save();
        }

        public bool DeleteKorpa(Korpa korpa)
        {
           _context.Remove(korpa);
            return Save();
        }

        public ICollection<Korpa> GetAllKorpas()
        {
            return _context.Korpaks.OrderBy(p=>p.IdKorpa).ToList();
        }

        public Korpa GetKorpaByID(int id)
        {
            return _context.Korpaks.Where(p => p.IdKorpa == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved>0?true:false;
        }

        public bool UpdateKorpa(Korpa korpa)
        {
            _context.Update(korpa);
            return Save();
        }
    }
}
