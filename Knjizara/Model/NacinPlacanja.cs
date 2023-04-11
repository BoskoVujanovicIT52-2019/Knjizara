using System.ComponentModel.DataAnnotations;

namespace Knjizara.Model
{
    public class NacinPlacanja
    {
        [Key]
        public int IdNacinPlacanja { get; set; }
        public String NazivNacinPlacanja { get; set; }
    }
}
