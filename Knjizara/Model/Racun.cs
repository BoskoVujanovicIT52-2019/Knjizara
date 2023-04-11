using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knjizara.Model
{
    public class Racun
    {
        [Key]
        public int IdRacun { get; set; }
        [ForeignKey("Korisnik")]
        public int IdKorisnik { get; set; }
        [ForeignKey("Korpa")]
        public int IdKorpa { get; set; }
        public String Datum { get; set; }
        public String Vreme { get; set; }
        [ForeignKey("NacinPlacanja")]
        public int IdNacinPlacanja { get; set; }
    }
}
