using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Knjizara.Model
{
    public class TipKorisnika
    {
        [Key]
        public int IdTipKorisnika  { get; set; }
        public String NazivTipaKorisnika { get; set; }

    }
}
