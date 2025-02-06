using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Reponses;
using ScreenSound.API.Requests;
using ScreenSound.Shared.Modelos.Modelos;
using ScreenSoundSQL.Modelos;
using ScreenSoundSQL.Repositorios.Interfaces;

namespace ScreenSound.API.EndPoints;

public static class ArtistasExtensions
{
    private static ArtistaResponse EntityToResponse(Artista artista) =>
        new(artista.Id, artista.Nome, artista.Bio!, artista.FotoPerfil);

    private static ICollection<ArtistaResponse> EntityToResponseList(IEnumerable<Artista> listaDeArtistas) =>
        listaDeArtistas.Select(a => EntityToResponse(a)).ToList();
    public static void AdicionarEndPointsArtistas(this WebApplication app)
    {
        var endpoints = app.MapGroup("/artista").WithTags("Artista");

        endpoints.MapGet("", async ([FromServices] IArtistaRepositorio repositorio) =>
        {
            var artistas = EntityToResponseList(await repositorio.ConsultarAsync());

            return Results.Ok(artistas);
        });

        endpoints.MapGet("/{nome}", async (
            string nome, 
            [FromServices] IArtistaRepositorio repositorio) =>
        {
            var artistaEscolhido = await repositorio.ConsultarPorNomeAsync(nome);

            if (artistaEscolhido is null) 
                return Results.NotFound(new { Mensagem = "Artista não encontrado" });

            return Results.Ok(artistaEscolhido);
        });

        endpoints.MapPost("", async (
            [FromBody] ArtistaRequest artistaRequest, 
            [FromServices] IArtistaRepositorio repositorio) =>
        {
            var artista = new Artista(artistaRequest.Nome, artistaRequest.Bio);

            await repositorio.AdicionarAsync(artista);

            return Results.Created($"/Artistas/{artistaRequest.Nome}", artista);
        });

        endpoints.MapDelete("/{id}", async (
            [FromServices] IArtistaRepositorio repositorioArtista, 
            [FromServices] IMusicaRepositorio repositorioMusica, 
            int id) =>
        {
            Artista? artista = await repositorioArtista.ConsultarPorIdAsync(id);

            if (artista is null) 
                return Results.NotFound("Artista não encontrado");

            await repositorioArtista.DeletarPorIdAsync(id);

            return Results.NoContent();
        });


        endpoints.MapPut("/{id}", async (
            [FromServices] IArtistaRepositorio repositorio, 
            int id, 
            [FromBody] ArtistaAtualizacaoModel artista) =>
        {
            await repositorio.AtualizarPorIdAsync(id, artista);

            return Results.NoContent();
        });
    }
}
