using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knjizara.Model
{
    public class StavkaKorpe
    {
        [Key]
        public int IdStavkaKorpe { get; set; }
        [ForeignKey("Knjiga")]
        public int IdKnjige { get; set; }
        public int Kolicina { get; set; }
        public Double Cena { get; set; }
        [ForeignKey("Korpa")]
        public int IdKorpa { get; set; }
    }
}
