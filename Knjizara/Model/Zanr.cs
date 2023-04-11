using System.ComponentModel.DataAnnotations;

namespace Knjizara.Model
{
    public class Zanr
    {
        [Key]
        public int IdZanr { get; set; }
        public String NazivZanr { get; set; }
    }
}
