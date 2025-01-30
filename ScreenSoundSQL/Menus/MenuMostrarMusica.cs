using ScreenSoundSQL.Repositorios.Interfaces;

namespace ScreenSoundSQL.Menus;

internal class MenuMostrarMusicas : Menu
{
    public override async Task ExecutarAsync(IArtistaRepositorio artistas, IMusicaRepositorio musicas)
    {
        Console.Clear();
        ExibirTituloDaOpcao("Exibir detalhes do artista");
        Console.Write("Digite o nome do artista que deseja conhecer melhor: ");
        string nomeDoArtista = Console.ReadLine()!;
        var artistaRecuperado = await artistas.ConsultarPorNomeAsync(nomeDoArtista);
        if (artistaRecuperado is not null)
        {
            Console.WriteLine("\nDiscografia:");
            artistaRecuperado.ExibirDiscografia();
            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
        else
        {
            Console.WriteLine($"\nO artista {nomeDoArtista} não foi encontrado!");
            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
