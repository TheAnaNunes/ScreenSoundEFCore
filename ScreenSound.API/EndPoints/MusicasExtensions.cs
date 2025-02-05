using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Reponses;
using ScreenSound.API.Requests;
using ScreenSound.Shared.Modelos.Modelos;
using ScreenSoundSQL.Modelos;
using ScreenSoundSQL.Repositorios.Interfaces;

namespace ScreenSound.API.EndPoints;

public static class MusicasExtensions
{
    private static MusicaReponse EntityToResponse(Musica musica) =>
    new MusicaReponse(musica.Id, musica.Nome, musica.ArtistaId, musica.Artista!.Nome);

    private static ICollection<MusicaReponse> EntityToResponseList(IEnumerable<Musica> listaDeMusicas) =>
        listaDeMusicas.Select(m => EntityToResponse(m)).ToList();
    public static void AdicionarEndPointsMusicas(this WebApplication app)
    {
        var endpoints = app.MapGroup("/musica").WithTags("Musica");

        endpoints.MapGet("", async ([FromServices] IMusicaRepositorio repositorio) =>
        {
            return Results.Ok(await repositorio.ConsultarAsync());
        });

        endpoints.MapGet("/{nome}", async (string nome, [FromServices] IMusicaRepositorio repositorio) =>
        {
            var musicaEscolhida = await repositorio.ConsultarPorNomeAsync(nome);
            if (musicaEscolhida is null) return Results.NotFound(new { Mensagem = "Musica não encontrada" });
            return Results.Ok(musicaEscolhida);
        });

        endpoints.MapPost("", async ([FromBody] MusicaRequest musicaRequest, [FromServices] IMusicaRepositorio repositorio) =>
        {
            var musica = new Musica(musicaRequest.Nome, musicaRequest.AnoLancamento, musicaRequest.ArtistaId);

            await repositorio.AdicionarAsync(musica);
            Results.Ok($"Musica do artista id {musica.ArtistaId} foi adicionada com sucesso!");
        });


        endpoints.MapDelete("/{id}", async ([FromServices] IMusicaRepositorio repositorio, int id) =>
        {
            Musica? musica = await repositorio.ConsultarPorIdAsync(id);

            if (musica is null) return Results.NotFound("Musica não encontrada");

            await repositorio.DeletarPorIdAsync(id);
            return Results.Ok();
        });

        endpoints.MapPut("/{id}", async ([FromServices] IMusicaRepositorio repositorio, int id, [FromBody] MusicaAtualizacaoModel musica) =>
        {
            await repositorio.AtualizarPorIdAsync(id, musica);
            return Results.Ok();
        });
    }
}
