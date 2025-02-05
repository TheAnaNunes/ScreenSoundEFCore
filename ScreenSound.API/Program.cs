using Microsoft.EntityFrameworkCore;
using ScreenSound.API.EndPoints;
using ScreenSound.Shared.Dados.Repositorios;
using ScreenSound.Shared.Dados.Repositorios.Interfaces;
using ScreenSoundSQL.Banco;
using ScreenSoundSQL.Repositorios;
using ScreenSoundSQL.Repositorios.Interfaces;
using System.Text.Json.Serialization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<IArtistaRepositorio, ArtistaRepositorio>();
        builder.Services.AddScoped<IMusicaRepositorio, MusicaRepositorio>();
        builder.Services.AddScoped<IGeneroRepositorio, GeneroRepositorio>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ScreenSoundContext>(options =>
        {
            options.UseSqlServer(
                "Data Source=DESKTOP-NV3P7TJ;Initial Catalog=ScreenSoundV0;Integrated Security=True;Encrypt=False;",
                sqlOptions => sqlOptions.MigrationsAssembly("ScreenSound.Shared.Dados"));
        });

        builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(jsonOptions =>
        {
            jsonOptions.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
            jsonOptions.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

        var app = builder.Build();

        app.AdicionarEndPointsArtistas();
        app.AdicionarEndPointsMusicas();
        app.AdicionarEndPointsGeneros();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.Run();
    }
}