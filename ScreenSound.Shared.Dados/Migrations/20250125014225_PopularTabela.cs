using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSoundSQL.Migrations
{
    /// <inheritdoc />
    public partial class PopularTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Artistas", ["Nome", "Bio", "FotoPerfil"], ["Djavan", "Um cantor famoso de Rap", "FotoPadrao"]);
            migrationBuilder.InsertData("Artistas", ["Nome", "Bio", "FotoPerfil"], ["The Beatles", "A maior banda de Rock de todos os tempos", "FotoPadrao"]);
            migrationBuilder.InsertData("Artistas", ["Nome", "Bio", "FotoPerfil"], ["Tim Maia", "Um cantor brasileiro de MPB super popular nos anos 90", "FotoPadrao"]);
            migrationBuilder.InsertData("Artistas", ["Nome", "Bio", "FotoPerfil"], ["Alcione", "Cantora de 'Não deixe o samba morrer!'", "FotoPadrao"]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE * FROM Artistas");
        }
    }
}
