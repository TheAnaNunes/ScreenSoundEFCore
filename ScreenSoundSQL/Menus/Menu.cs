using ScreenSoundSQL.Repositorios.Interfaces;

namespace ScreenSoundSQL.Menus;

public abstract class Menu
{
    public void ExibirTituloDaOpcao(string titulo)
    {
        int quantidadeDeLetras = titulo.Length;
        string asteriscos = string.Empty.PadLeft(quantidadeDeLetras, '*');
        Console.WriteLine(asteriscos);
        Console.WriteLine(titulo);
        Console.WriteLine(asteriscos + "\n");
    }
    public abstract Task ExecutarAsync(IArtistaRepositorio artistas, IMusicaRepositorio musicas);
}
