using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Reponses;
using ScreenSound.API.Requests;
using ScreenSound.Shared.Dados.Repositorios.Interfaces;
using ScreenSound.Shared.Modelos.Modelos;
using ScreenSoundSQL.Modelos;
using ScreenSoundSQL.Repositorios.Interfaces;

namespace ScreenSound.API.EndPoints;

public static class MusicasExtensions
{
    private static MusicaReponse EntityToResponse(Musica musica) =>
    new(musica.Id, musica.Nome, musica.ArtistaId, musica.Artista!.Nome);

    private static ICollection<MusicaReponse> EntityToResponseList(IEnumerable<Musica> listaDeMusicas) =>
        listaDeMusicas.Select(EntityToResponse).ToList();
    public static void AdicionarEndPointsMusicas(this WebApplication app)
    {
        var endpoints = app.MapGroup("/musica").WithTags("Musica");

        endpoints.MapGet("", async ([FromServices] IMusicaRepositorio repositorio) =>
        {
            var musicas = EntityToResponseList(await repositorio.ConsultarAsync());
            return Results.Ok(musicas);
        });

        endpoints.MapGet("/{nome}", async (string nome, [FromServices] IMusicaRepositorio repositorio) =>
        {
            var musicaEscolhida = await repositorio.ConsultarPorNomeAsync(nome);
            if (musicaEscolhida is null) return Results.NotFound(new { Mensagem = "Musica não encontrada" });
            return Results.Ok(musicaEscolhida);
        });

        endpoints.MapPost("", async (
            [FromBody] MusicaRequest musicaRequest, 
            [FromServices] IMusicaRepositorio repositorioMusica, 
            [FromServices] IGeneroRepositorio repositorioGenero) =>
        {
            var musica = new Musica
            {
                Nome = musicaRequest.Nome,
                AnoLancamento = musicaRequest.AnoLancamento,
                ArtistaId = musicaRequest.ArtistaId,
                Generos = await GeneroRequestConverter(musicaRequest.Generos!, repositorioGenero)
            };

            await repositorioMusica.AdicionarAsync(musica);
            return Results.Ok($"Musica do artista id {musica.ArtistaId} foi adicionada com sucesso!");
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

    private static async Task<ICollection<Genero>> GeneroRequestConverter(ICollection<GeneroRequest> generos, IGeneroRepositorio repositorio)
    {
        var listaDeGeneros = new List<Genero>();
        foreach (var genero in generos)
        {
            var generoConvertido = RequestToEntity(genero);
            Genero? generoRetornadoBanco = await repositorio.ConsultarPorNomeAsync(genero.Nome);

            if (generoRetornadoBanco is not null)
                listaDeGeneros.Add(generoRetornadoBanco!);
            else
                listaDeGeneros.Add(generoConvertido);
        }
        return listaDeGeneros;
    }

    private static Genero RequestToEntity(GeneroRequest generoRequest) =>
        new()
        {
            Nome = generoRequest.Nome,
            Descricao = generoRequest.Descricao,
        };
}
