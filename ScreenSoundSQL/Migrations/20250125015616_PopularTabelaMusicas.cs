using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSoundSQL.Migrations
{
    /// <inheritdoc />
    public partial class PopularTabelaMusicas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("Musicas", ["Nome", "AnoLancamento"], ["Hey Jude", "1968"]);
            migrationBuilder.InsertData("Musicas", ["Nome", "AnoLancamento"], ["Let it Be", "1970"]);
            migrationBuilder.InsertData("Musicas", ["Nome", "AnoLancamento"], ["Something", "1969"]);
            migrationBuilder.InsertData("Musicas", ["Nome", "AnoLancamento"], ["Yellow Submarine", "1966"]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Musicas");
        }
    }
}
