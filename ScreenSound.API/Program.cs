using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Shared.Dados.Repositorios;
using ScreenSoundSQL.Banco;
using ScreenSoundSQL.Modelos;
using ScreenSoundSQL.Repositorios;
using ScreenSoundSQL.Repositorios.Interfaces;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IArtistaRepositorio, ArtistaRepositorio>();
builder.Services.AddScoped<IMusicaRepositorio, MusicaRepositorio>();
builder.Services.AddDbContext<ScreenSoundContext>(options =>
{
    options.UseSqlServer("Data Source=DESKTOP-NV3P7TJ;Initial Catalog=ScreenSoundV0;Integrated Security=True;Encrypt=False;");
});

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(jsonOptions =>
{
    jsonOptions.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
    jsonOptions.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

app.MapGet("/Artistas", async ([FromServices] IArtistaRepositorio repositorio) =>
{
    return Results.Ok(await repositorio.ConsultarAsync());
});
 
app.MapGet("/Artistas/{nome}", async (string nome,[FromServices] IArtistaRepositorio repositorio) =>
{
    var artistaEscolhido = await repositorio.ConsultarPorNomeAsync(nome);
    if (artistaEscolhido is null) return Results.NotFound(new { Mensagem = "Aritsta n�o encontrado" });
    return Results.Ok(artistaEscolhido);
});

app.MapPost("/Artistas", async ([FromBody] Artista artista, [FromServices] IArtistaRepositorio repositorio) =>
{
    await repositorio.AdicionarAsync(artista);
    return Results.Created($"/Artistas/{artista.Nome}", artista);
});

app.MapDelete("/Artistas/{id}", async ([FromServices] IArtistaRepositorio repositorioArtista,[FromServices] IMusicaRepositorio repositorioMusica, int id) =>
{
    Artista? artista = await repositorioArtista.ConsultarPorIdAsync(id);

    if (artista is null) return Results.NotFound("Artista n�o encontrado");

    await repositorioArtista.DeletarPorIdAsync(id);
    return Results.Ok($"Artista: {artista.Nome} removido com sucesso!");
});

app.Run();
