using Microsoft.AspNetCore.Mvc;
using ScreenSound.Shared.Modelos.Modelos;
using ScreenSoundSQL.Modelos;
using ScreenSoundSQL.Repositorios.Interfaces;

namespace ScreenSound.API.EndPoints;

public static class MusicasExtensions
{
    public static void AdicionarEndPointsMusicas(this WebApplication app)
    {
        app.MapGet("/Musicas", async ([FromServices] IMusicaRepositorio repositorio) =>
        {
            return Results.Ok(await repositorio.ConsultarAsync());
        });

        app.MapGet("/Musicas/{nome}", async (string nome, [FromServices] IMusicaRepositorio repositorio) =>
        {
            var musicaEscolhida = await repositorio.ConsultarPorNomeAsync(nome);
            if (musicaEscolhida is null) return Results.NotFound(new { Mensagem = "Musica não encontrada" });
            return Results.Ok(musicaEscolhida);
        });

        app.MapPost("/Musicas", async ([FromBody] Musica musica, [FromServices] IMusicaRepositorio repositorio) =>
        {
            await repositorio.AdicionarAsync(musica);
            Results.Ok($"Musica do artista id {musica.ArtistaId} foi adicionada com sucesso!");
        });


        app.MapDelete("/Musicas/{id}", async ([FromServices] IMusicaRepositorio repositorio, int id) =>
        {
            Musica? musica = await repositorio.ConsultarPorIdAsync(id);

            if (musica is null) return Results.NotFound("Musica não encontrada");

            await repositorio.DeletarPorIdAsync(id);
            return Results.Ok();
        });

        app.MapPut("/Musicas/{id}", async ([FromServices] IMusicaRepositorio repositorio, int id, [FromBody] MusicaAtualizacaoModel musica) =>
        {
            await repositorio.AtualizarPorIdAsync(id, musica);
            return Results.Ok();
        });
    }
}
