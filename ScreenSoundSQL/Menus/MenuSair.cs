using ScreenSoundSQL.Repositorios.Interfaces;

namespace ScreenSoundSQL.Menus;

internal class MenuSair : Menu
{
    public override async Task ExecutarAsync(IArtistaRepositorio artistas, IMusicaRepositorio musicas)
    {
        await Task.Delay(1);
        Console.WriteLine("Tchau tchau :)");
    }
}
