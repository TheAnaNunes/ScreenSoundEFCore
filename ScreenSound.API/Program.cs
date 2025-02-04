using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Shared.Dados.Repositorios;
using ScreenSound.Shared.Modelos.Modelos;
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

app.MapGet("/Musicas", async ([FromServices] IMusicaRepositorio repositorio) =>
{
    return Results.Ok(await repositorio.ConsultarAsync());
});
 
app.MapGet("/Artistas/{nome}", async (string nome,[FromServices] IArtistaRepositorio repositorio) =>
{
    var artistaEscolhido = await repositorio.ConsultarPorNomeAsync(nome);
    if (artistaEscolhido is null) return Results.NotFound(new { Mensagem = "Artista não encontrado" });
    return Results.Ok(artistaEscolhido);
});

app.MapGet("/Musicas/{nome}", async (string nome, [FromServices] IMusicaRepositorio repositorio) =>
{
    var musicaEscolhida = await repositorio.ConsultarPorNomeAsync(nome);
    if (musicaEscolhida is null) return Results.NotFound(new { Mensagem = "Musica não encontrada" });
    return Results.Ok(musicaEscolhida);
});

app.MapPost("/Artistas", async ([FromBody] Artista artista, [FromServices] IArtistaRepositorio repositorio) =>
{
    await repositorio.AdicionarAsync(artista);
    return Results.Created($"/Artistas/{artista.Nome}", artista);
});

app.MapPost("/Musicas", async ([FromBody] Musica musica, [FromServices] IMusicaRepositorio repositorio) =>
{
    await repositorio.AdicionarAsync(musica);
    Results.Ok($"Musica do artista id {musica.ArtistaId} foi adicionada com sucesso!");
});

app.MapDelete("/Artistas/{id}", async ([FromServices] IArtistaRepositorio repositorioArtista,[FromServices] IMusicaRepositorio repositorioMusica, int id) =>
{
    Artista? artista = await repositorioArtista.ConsultarPorIdAsync(id);

    if (artista is null) return Results.NotFound("Artista não encontrado");

    await repositorioArtista.DeletarPorIdAsync(id);
    return Results.Ok($"Artista: {artista.Nome} removido com sucesso!");
});

app.MapDelete("/Musicas/{id}", async ([FromServices] IMusicaRepositorio repositorio, int id) =>
{
    Musica? musica = await repositorio.ConsultarPorIdAsync(id);

    if (musica is null) return Results.NotFound("Musica não encontrada");

    await repositorio.DeletarPorIdAsync(id);
    return Results.Ok($"Musica: {musica.Nome} removida com sucesso!");
});

app.MapPut("/Artistas/{id}", async ([FromServices] IArtistaRepositorio repositorio, int id, [FromBody] ArtistaAtualizacaoModel artista) =>
{
    await repositorio.AtualizarPorIdAsync(id, artista);
    return Results.Ok($"Artista {artista.Nome} foi atualizado com sucesso!");
});

app.MapPut("/Musicas/{id}", async ([FromServices] IMusicaRepositorio repositorio, int id, [FromBody] MusicaAtualizacaoModel musica) =>
{
    await repositorio.AtualizarPorIdAsync(id, musica);
    return Results.Ok($"Musica {musica.Nome} foi atualizado com sucesso!");
});

app.Run();
