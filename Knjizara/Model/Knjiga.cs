using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Knjizara.Model
{
    public class Knjiga
    {
        [Key]
        public int IdKnjige { get; set; }
        public String NazivKnjige { get; set; }
        [ForeignKey("Izdavac")]
        public int IdIzdavac { get; set; }
        public int BrojStranica { get; set; }
        [ForeignKey("Povez")]
        public int IdPovez { get; set; }
        [ForeignKey("Zanr")]
        public int IdZanr { get; set; }
        [ForeignKey("Pisac")]
        public int IdPisca { get; set; }
        public String Opis { get; set; }
    }
}
