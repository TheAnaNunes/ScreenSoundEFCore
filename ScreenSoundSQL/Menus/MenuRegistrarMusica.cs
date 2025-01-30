using ScreenSoundSQL.Banco;
using ScreenSoundSQL.Modelos;
using ScreenSoundSQL.Repositorios.Interfaces;

namespace ScreenSoundSQL.Menus;

internal class MenuRegistrarMusica : Menu
{
    public override async Task ExecutarAsync(IArtistaRepositorio artistas, IMusicaRepositorio musicas)
    {
        Console.Clear();
        ExibirTituloDaOpcao("Registro de músicas");
        Console.Write("Digite o artista cuja música deseja registrar: ");
        string nomeDoArtista = Console.ReadLine()!;
        var artistaRecuperado = await artistas.ConsultarPorNomeAsync(nomeDoArtista);
        if (artistaRecuperado is not null)
        {
            Console.Write("Agora digite o título da música: ");
            string tituloDaMusica = Console.ReadLine()!;
            Console.Write("Agora digite o ano da música: ");
            int anoLancamentoDaMusica = int.Parse(Console.ReadLine()!);
            var musica = new Musica(tituloDaMusica, anoLancamentoDaMusica, artistaRecuperado);
            artistaRecuperado.AdicionarMusica(musica);
            await musicas.AdicionarAsync(artistaRecuperado, musica);
            Console.WriteLine($"A música {tituloDaMusica} de {nomeDoArtista} foi registrada com sucesso!");
            await artistas.AdicionarMusicaArtistaAsync(artistaRecuperado);
            Thread.Sleep(4000);
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
