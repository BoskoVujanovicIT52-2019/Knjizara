using Knjizara.Data;
using Knjizara.Interfaces;
using Knjizara.Model;

namespace Knjizara.Repository
{
    public class StavkaKorpeRepository : IStavkaKorpe
    {
        private readonly DataContext _context;
        public StavkaKorpeRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateStavkaKorpe(StavkaKorpe stavkaKorpe)
        {
            _context.Add(stavkaKorpe);
            return Save();

        }

        public bool DeleteStavkaKorpe(StavkaKorpe stavkaKorpe)
        {
            _context.Remove(stavkaKorpe);
            return Save();
        }

        public ICollection<StavkaKorpe> GetAllStavkaKorpes()
        {
            return _context.StavkaKorpes.OrderBy(p => p.IdStavkaKorpe).ToList();
        }

        public StavkaKorpe GetStavkaKorpeByID(int id)
        {
            return _context.StavkaKorpes.Where(p => p.IdKorpa == id).FirstOrDefault();

        }

        public bool Save()
        {
               var saved = _context.SaveChanges();
            return saved>0?true:false;
        }

        public bool UpdateStavkaKorpe(StavkaKorpe stavkaKorpe)
        {
            _context.Update(stavkaKorpe);
            return Save();
        }
    }
}
