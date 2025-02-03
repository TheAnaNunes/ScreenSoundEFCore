using ScreenSound.Shared.Modelos.Modelos;
using ScreenSoundSQL.Modelos;

namespace ScreenSoundSQL.Repositorios.Interfaces;

public interface IArtistaRepositorio
{
    Task AdicionarAsync(Artista artista);
    Task DeletarPorIdAsync(int id);
    Task<Artista?> ConsultarPorIdAsync(int id);
    Task<List<Artista>> ConsultarAsync();
    Task AdicionarMusicaArtistaAsync(Artista artista);
    Task AtualizarPorIdAsync(int id, ArtistaAtualizacaoModel model);
    Task<Artista?> ConsultarPorNomeAsync(string nome);
}
