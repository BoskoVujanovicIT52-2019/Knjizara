using Knjizara.Model;
using Microsoft.EntityFrameworkCore;

namespace Knjizara.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { 
        
        }
        public DbSet<TipKorisnika> TipKorisnikas { get; set; }
        public DbSet<Izdavac> Izdavacs { get; set; }
        public DbSet<Knjiga> Knjigas { get; set; }
        public DbSet<Korisnik> Korisniks { get; set; }
        public DbSet<Korpa> Korpaks { get; set; }
        public DbSet<NacinPlacanja> NacinPlacanjas { get; set; }
        public DbSet<Pisac> Pisacs { get; set; }
        public DbSet<Povez> Povezs { get; set; }
        public DbSet<Racun> Racuns { get; set; }
        public DbSet<StavkaKorpe> StavkaKorpes { get; set; }
        public DbSet<Zanr> Zanrs { get; set; }
    }
}
