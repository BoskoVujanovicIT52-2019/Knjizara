using Knjizara.Data;
using Knjizara.Model;

namespace Knjizara.Repository
{
    public class IzdavacRepository : IIzdavacRepository
    {
        private readonly DataContext _context;

        public IzdavacRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateIzdavac(Izdavac izdavac)
        {
            _context.Add(izdavac);
            return Save();
        
        }

        public bool DeleteIzdavac(Izdavac izdavac)
        {
            _context.Remove(izdavac);
            return Save();
        
        }

        public ICollection<Izdavac> GetAllIzdavac()
        {
            return _context.Izdavacs.OrderBy(p => p.IdIzdavac).ToList();
        }

        public Izdavac GetIzdavacById(int id)
        {
            return _context.Izdavacs.Where(p => p.IdIzdavac == id).FirstOrDefault();    
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateIzdavac(Izdavac izdavac)
        {
            _context.Update(izdavac);
            return Save();
        }
    }
}
