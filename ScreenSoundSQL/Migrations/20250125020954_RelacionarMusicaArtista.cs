using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSoundSQL.Migrations
{
    /// <inheritdoc />
    public partial class RelacionarMusicaArtista : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE Musicas SET ArtistaId = 2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
