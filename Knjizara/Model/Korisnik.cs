using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Knjizara.Model
{
    public class Korisnik
    {
        [Key]
        public int IdClana { get; set; }
        public String Ime { get; set; }
        public String Prezime { get; set; }
        public String JMBG { get; set; }
        public String Email { get; set; }
        public String Telefon { get; set; }
        public String Adresa { get; set; }
        public String Grad { get; set; }
        public String Drzava { get; set; }
        public String Lozinka { get; set; }
        public String Salt { get; set; } //dodato

        [DefaultValue("User")]
        public String ulogaClana { get; set; }

       

    }
}
