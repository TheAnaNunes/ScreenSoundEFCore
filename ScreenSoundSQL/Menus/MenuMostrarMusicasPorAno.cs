using ScreenSoundSQL.Banco;
using ScreenSoundSQL.Modelos;

namespace ScreenSoundSQL.Menus;

internal class MenuMostrarMusicasPorAno : Menu
{
    public override void Executar(DAL<Artista> artistaDal)
    {
        base.Executar(artistaDal);
        ExibirTituloDaOpcao("Exibir Músicas por Ano");
        Console.Write("Digite o ano de lançamento da(s) música(s): ");
        int anoLancamentoMusica = int.Parse(Console.ReadLine()!);
        var musicaDal = new DAL<Musica>(new ScreenSoundContext());
        var listaMusicas = musicaDal.ListarPor(musica => musica.AnoLancamento.Equals(anoLancamentoMusica));
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
