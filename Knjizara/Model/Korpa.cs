using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knjizara.Model
{
    public class Korpa
    {
        [Key]
        public int IdKorpa { get; set; }
        [ForeignKey("Korisnik")]
        public int IdKorisnik { get; set; }
        [ForeignKey("StavkaKorpe")]
        public int IdStavkaKorpe { get; set; }
    }
}
