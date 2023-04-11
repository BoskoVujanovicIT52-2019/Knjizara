using System.ComponentModel.DataAnnotations;

namespace Knjizara.Model
{
    public class Povez
    {
        [Key]
        public int IdPovez { get; set; }
        public String NazivPoveza { get; set; }
    }
}
