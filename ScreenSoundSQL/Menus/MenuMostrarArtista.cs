using ScreenSoundSQL.Repositorios.Interfaces;

namespace ScreenSoundSQL.Menus;

public class MenuMostrarArtistas : Menu
{
    public override async Task ExecutarAsync(IArtistaRepositorio artistas, IMusicaRepositorio musicas)
    {
        Console.Clear();
        ExibirTituloDaOpcao("Exibindo todos os artistas registradas na nossa aplicação");

        var listaArtistas = await artistas.ConsultarAsync();

        foreach (var artista in listaArtistas)
            Console.WriteLine($"Artista: {artista}");

        Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
    }
}
