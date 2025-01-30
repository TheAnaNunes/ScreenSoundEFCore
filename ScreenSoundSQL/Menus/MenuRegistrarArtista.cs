using ScreenSoundSQL.Modelos;
using ScreenSoundSQL.Repositorios.Interfaces;

namespace ScreenSoundSQL.Menus;

internal class MenuRegistrarArtista : Menu
{
    public override async Task ExecutarAsync(IArtistaRepositorio artistas, IMusicaRepositorio musicas)
    {
        Console.Clear();
        ExibirTituloDaOpcao("Registro dos Artistas");
        Console.Write("Digite o nome do artista que deseja registrar: ");
        string nomeDoArtista = Console.ReadLine()!;
        Console.Write("Digite a bio do artista que deseja registrar: ");
        string bioDoArtista = Console.ReadLine()!;
        Artista artista = new(nomeDoArtista, bioDoArtista);
        await artistas.AdicionarAsync(artista);
        Console.WriteLine($"O artista {nomeDoArtista} foi registrado com sucesso!");
        Thread.Sleep(4000);
        Console.Clear();
    }
}
