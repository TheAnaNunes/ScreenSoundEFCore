using Microsoft.AspNetCore.Mvc;
using ScreenSound.Shared.Modelos.Modelos;
using ScreenSoundSQL.Modelos;
using ScreenSoundSQL.Repositorios.Interfaces;

namespace ScreenSound.API.EndPoints;

public static class ArtistasExtensions
{
    public static void AdicionarEndPointsArtistas(this WebApplication app)
    {
        app.MapGet("/Artistas", async ([FromServices] IArtistaRepositorio repositorio) =>
        {
            return Results.Ok(await repositorio.ConsultarAsync());
        });

        app.MapGet("/Artistas/{nome}", async (string nome, [FromServices] IArtistaRepositorio repositorio) =>
        {
            var artistaEscolhido = await repositorio.ConsultarPorNomeAsync(nome);
            if (artistaEscolhido is null) return Results.NotFound(new { Mensagem = "Artista não encontrado" });
            return Results.Ok(artistaEscolhido);
        });

        app.MapPost("/Artistas", async ([FromBody] Artista artista, [FromServices] IArtistaRepositorio repositorio) =>
        {
            await repositorio.AdicionarAsync(artista);
            return Results.Created($"/Artistas/{artista.Nome}", artista);
        });

        app.MapDelete("/Artistas/{id}", async ([FromServices] IArtistaRepositorio repositorioArtista, [FromServices] IMusicaRepositorio repositorioMusica, int id) =>
        {
            Artista? artista = await repositorioArtista.ConsultarPorIdAsync(id);

            if (artista is null) return Results.NotFound("Artista não encontrado");

            await repositorioArtista.DeletarPorIdAsync(id);
            return Results.Ok();
        });


        app.MapPut("/Artistas/{id}", async ([FromServices] IArtistaRepositorio repositorio, int id, [FromBody] ArtistaAtualizacaoModel artista) =>
        {
            await repositorio.AtualizarPorIdAsync(id, artista);
            return Results.Ok();
        });
    }
}
