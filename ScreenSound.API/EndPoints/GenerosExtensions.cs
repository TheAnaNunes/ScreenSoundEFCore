using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Reponses;
using ScreenSound.API.Requests;
using ScreenSound.Shared.Dados.Repositorios.Interfaces;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.EndPoints;

public static class GenerosExtensions
{
    private static GeneroResponse EntityToResponse(Genero genero) =>
    new(genero.Nome, genero.Descricao);

    private static ICollection<GeneroResponse> EntityToResponseList(IEnumerable<Genero> listaDeGeneros) =>
        listaDeGeneros.Select(g => EntityToResponse(g)).ToList();

    public static void AdicionarEndPointsGeneros(this WebApplication app)
    {
        var endpoints = app.MapGroup("/generos").WithTags("Generos");

        endpoints.MapGet("", async ([FromServices] IGeneroRepositorio repositorio) =>
        {
            var generos = EntityToResponseList(await repositorio.ConsultarAsync());

            return Results.Ok(generos);
        });

        endpoints.MapPost("", async (
            [FromServices] IGeneroRepositorio repositorio, 
            [FromBody] GeneroRequest request) =>
        {
            var genero = new Genero
            {
                Nome = request.Nome,
                Descricao = request.Descricao
            };

            await repositorio.AdicionarAsync(genero);

            return Results.Created($"/Generos/{genero.Nome}", genero);
        });

        endpoints.MapPut("/{id}", async (
            [FromServices] IGeneroRepositorio repositorio, 
            [FromBody] GeneroModel model, 
            int id) =>
        {
            await repositorio.AtualizarPorIdAsync(id, model);

            return Results.NoContent();
        });

        endpoints.MapDelete("/{id}", async (
            [FromServices] IGeneroRepositorio repositorio, 
            int id) =>
        {
            await repositorio.DeletarPorIdAsync(id);

            return Results.NoContent();
        });

    }
}
