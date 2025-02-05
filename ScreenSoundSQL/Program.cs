using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ScreenSound.Shared.Dados.Repositorios;
using ScreenSoundSQL.Banco;
using ScreenSoundSQL.Menus;
using ScreenSoundSQL.Repositorios;
using ScreenSoundSQL.Repositorios.Interfaces;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging( logging =>
        {
            logging.ClearProviders();
            logging.AddConsole();
            logging.SetMinimumLevel(LogLevel.Error);
        })
    .ConfigureServices((contexto, servicos) => 
    {
        servicos.AddDbContext<ScreenSoundContext>(options =>
        {
            options.UseSqlServer(
                "Data Source=DESKTOP-NV3P7TJ;Initial Catalog=ScreenSoundV0;Integrated Security=True;Encrypt=False;",
                sqlOptions => sqlOptions.MigrationsAssembly("ScreenSound.Shared.Dados"))
                .LogTo(Console.WriteLine, LogLevel.Error);
        });
        servicos.AddScoped<IArtistaRepositorio, ArtistaRepositorio>();
        servicos.AddScoped<IMusicaRepositorio, MusicaRepositorio>();
    })
    .Build();


var scope = host.Services.CreateScope();
var musicas = scope.ServiceProvider.GetRequiredService<IMusicaRepositorio>();
var artistas = scope.ServiceProvider.GetRequiredService<IArtistaRepositorio>();

Dictionary<int, Menu> opcoes = new()
{
    { 1, new MenuRegistrarArtista() },
    { 2, new MenuRegistrarMusica() },
    { 3, new MenuMostrarArtistas() },
    { 4, new MenuMostrarMusicas() },
    { 5, new MenuMostrarMusicasPorAno() },
    { -1, new MenuSair() }
};

void ExibirLogo()
{
    Console.WriteLine(@"

░██████╗░█████╗░██████╗░███████╗███████╗███╗░░██╗  ░██████╗░█████╗░██╗░░░██╗███╗░░██╗██████╗░
██╔════╝██╔══██╗██╔══██╗██╔════╝██╔════╝████╗░██║  ██╔════╝██╔══██╗██║░░░██║████╗░██║██╔══██╗
╚█████╗░██║░░╚═╝██████╔╝█████╗░░█████╗░░██╔██╗██║  ╚█████╗░██║░░██║██║░░░██║██╔██╗██║██║░░██║
░╚═══██╗██║░░██╗██╔══██╗██╔══╝░░██╔══╝░░██║╚████║  ░╚═══██╗██║░░██║██║░░░██║██║╚████║██║░░██║
██████╔╝╚█████╔╝██║░░██║███████╗███████╗██║░╚███║  ██████╔╝╚█████╔╝╚██████╔╝██║░╚███║██████╔╝
╚═════╝░░╚════╝░╚═╝░░╚═╝╚══════╝╚══════╝╚═╝░░╚══╝  ╚═════╝░░╚════╝░░╚═════╝░╚═╝░░╚══╝╚═════╝░
");
    Console.WriteLine("Boas vindas ao Screen Sound 3.0!");
}

async Task ExibirOpcoesDoMenu()
{
    ExibirLogo();
    Console.WriteLine("\nDigite 1 para registrar um artista");
    Console.WriteLine("Digite 2 para registrar a música de um artista");
    Console.WriteLine("Digite 3 para mostrar todos os artistas");
    Console.WriteLine("Digite 4 para exibir todas as músicas de um artista");
    Console.WriteLine("Digite 5 para exibir todas as músicas de um ano");
    Console.WriteLine("Digite -1 para sair");

    Console.Write("\nDigite a sua opção: ");
    string opcaoEscolhida = Console.ReadLine()!;
    int opcaoEscolhidaNumerica = int.Parse(opcaoEscolhida);

    if (opcoes.ContainsKey(opcaoEscolhidaNumerica))
    {
        Menu menuASerExibido = opcoes[opcaoEscolhidaNumerica];
        await menuASerExibido.ExecutarAsync(artistas, musicas);

        if (opcaoEscolhidaNumerica > 0) await ExibirOpcoesDoMenu();
    }
    else
    {
        Console.WriteLine("Opção inválida");
    }
}

await ExibirOpcoesDoMenu();