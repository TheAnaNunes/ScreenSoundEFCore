using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScreenSound.Shared.Dados.Repositorios;
using ScreenSoundSQL.Banco;
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
    return await repositorio.ConsultarAsync();
});

app.MapGet("/Artistas/{nome}", async (string nome,[FromServices] IArtistaRepositorio repositorio) =>
{
    var artistaEscolhido = await repositorio.ConsultarPorNomeAsync(nome);
    if (artistaEscolhido is null)
        return Results.NotFound(new { Mensagem = "Aritsta não encontrado" });
    return Results.Ok(artistaEscolhido);
});

app.Run();
