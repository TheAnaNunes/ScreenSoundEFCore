using ScreenSoundSQL.Repositorios.Interfaces;

namespace ScreenSoundSQL.Menus;

internal class MenuMostrarMusicasPorAno : Menu
{
    public override async Task ExecutarAsync(IArtistaRepositorio artistas, IMusicaRepositorio musicas)
    {
        Console.Clear();
        ExibirTituloDaOpcao("Exibir Músicas por Ano");
        Console.Write("Digite o ano de lançamento da(s) música(s): ");
        int anoLancamentoMusica = int.Parse(Console.ReadLine()!);

        var listaMusicas = await musicas.ConsultarPorAnoLancamentoAsync(anoLancamentoMusica);

        if (listaMusicas.Any())
        {
            foreach (var musica in listaMusicas)
                musica.ExibirFichaTecnica();

            Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
        else
        {
            Console.WriteLine($"\nNão temos registro de músicas lançadas no ano:{anoLancamentoMusica}");
            Console.WriteLine("Digite uma tecla para voltar ao menu principal");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
