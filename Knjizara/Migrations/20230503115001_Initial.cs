using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Knjizara.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Izdavacs",
                columns: table => new
                {
                    IdIzdavac = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivIzdavaca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Drzava = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Izdavacs", x => x.IdIzdavac);
                });

            migrationBuilder.CreateTable(
                name: "Knjigas",
                columns: table => new
                {
                    IdKnjige = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivKnjige = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdIzdavac = table.Column<int>(type: "int", nullable: false),
                    BrojStranica = table.Column<int>(type: "int", nullable: false),
                    IdPovez = table.Column<int>(type: "int", nullable: false),
                    IdZanr = table.Column<int>(type: "int", nullable: false),
                    IdPisca = table.Column<int>(type: "int", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knjigas", x => x.IdKnjige);
                });

            migrationBuilder.CreateTable(
                name: "Korisniks",
                columns: table => new
                {
                    IdClana = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Drzava = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ulogaClana = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisniks", x => x.IdClana);
                });

            migrationBuilder.CreateTable(
                name: "Korpaks",
                columns: table => new
                {
                    IdKorpa = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdKorisnik = table.Column<int>(type: "int", nullable: false),
                    IdStavkaKorpe = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korpaks", x => x.IdKorpa);
                });

            migrationBuilder.CreateTable(
                name: "NacinPlacanjas",
                columns: table => new
                {
                    IdNacinPlacanja = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivNacinPlacanja = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NacinPlacanjas", x => x.IdNacinPlacanja);
                });

            migrationBuilder.CreateTable(
                name: "Pisacs",
                columns: table => new
                {
                    IdPisca = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pisacs", x => x.IdPisca);
                });

            migrationBuilder.CreateTable(
                name: "Povezs",
                columns: table => new
                {
                    IdPovez = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivPoveza = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Povezs", x => x.IdPovez);
                });

            migrationBuilder.CreateTable(
                name: "Racuns",
                columns: table => new
                {
                    IdRacun = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdKorisnik = table.Column<int>(type: "int", nullable: false),
                    IdKorpa = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vreme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdNacinPlacanja = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racuns", x => x.IdRacun);
                });

            migrationBuilder.CreateTable(
                name: "StavkaKorpes",
                columns: table => new
                {
                    IdStavkaKorpe = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdKnjige = table.Column<int>(type: "int", nullable: false),
                    Kolicina = table.Column<int>(type: "int", nullable: false),
                    Cena = table.Column<double>(type: "float", nullable: false),
                    IdKorpa = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StavkaKorpes", x => x.IdStavkaKorpe);
                });

            migrationBuilder.CreateTable(
                name: "TipKorisnikas",
                columns: table => new
                {
                    IdTipKorisnika = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivTipaKorisnika = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipKorisnikas", x => x.IdTipKorisnika);
                });

            migrationBuilder.CreateTable(
                name: "Zanrs",
                columns: table => new
                {
                    IdZanr = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivZanr = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zanrs", x => x.IdZanr);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Izdavacs");

            migrationBuilder.DropTable(
                name: "Knjigas");

            migrationBuilder.DropTable(
                name: "Korisniks");

            migrationBuilder.DropTable(
                name: "Korpaks");

            migrationBuilder.DropTable(
                name: "NacinPlacanjas");

            migrationBuilder.DropTable(
                name: "Pisacs");

            migrationBuilder.DropTable(
                name: "Povezs");

            migrationBuilder.DropTable(
                name: "Racuns");

            migrationBuilder.DropTable(
                name: "StavkaKorpes");

            migrationBuilder.DropTable(
                name: "TipKorisnikas");

            migrationBuilder.DropTable(
                name: "Zanrs");
        }
    }
}
