using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScreenSound.API.EndPoints;
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

app.AdicionarEndPointsArtistas();
app.AdicionarEndPointsMusicas();

app.Run();
