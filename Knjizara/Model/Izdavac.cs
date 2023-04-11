using System.ComponentModel.DataAnnotations;

namespace Knjizara.Model
{
    public class Izdavac
    {
        [Key]public int IdIzdavac { get; set; }
        public String NazivIzdavaca { get; set; }
        public String Adresa { get; set; }
        public String Grad { get; set; }
        public String Drzava { get; set; }
        public String Telefon { get; set; }
    }
}
