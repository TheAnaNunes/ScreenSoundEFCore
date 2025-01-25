using ScreenSoundSQL.Banco;
using ScreenSoundSQL.Modelos;

namespace ScreenSoundSQL.Menus;

internal class MenuSair : Menu
{
    public override void Executar(DAL<Artista> artistaDal)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
