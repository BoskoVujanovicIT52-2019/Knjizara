using System.ComponentModel.DataAnnotations;

namespace Knjizara.Model
{
    public class Pisac
    {
        [Key]
        public int IdPisca { get; set; }
        public String Ime { get; set; }
        public String Prezime { get; set; }
        public String Opis { get; set; }
    }
}
