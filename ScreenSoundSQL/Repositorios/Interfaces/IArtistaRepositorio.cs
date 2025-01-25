using ScreenSoundSQL.Modelos;

namespace ScreenSoundSQL.Repositorios.Interfaces;

internal interface IArtistaRepositorio
{
    Task AdicionarAsync(Artista artista);
    Task DeletarPorIdAsync(int id);
    Task<Artista?> ConsultarPorIdAsync(int id);
    Task<List<Artista>> ConsultarAsync();
    Task AtualizarPorIdAsync(int id, ArtistaAtualizacaoModel model);
}
