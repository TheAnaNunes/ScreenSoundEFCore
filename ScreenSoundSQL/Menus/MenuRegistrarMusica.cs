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
            var artistaComId = await artistas.ConsultarPorNomeAsync(artistaRecuperado.Nome!);

            musica.Artista!.Id = artistaComId!.Id;

            await musicas.AdicionarAsync(musica);
            await artistas.AdicionarMusicaArtistaAsync(artistaRecuperado);

            Console.WriteLine($"A música {tituloDaMusica} de {nomeDoArtista} foi registrada com sucesso!");

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
