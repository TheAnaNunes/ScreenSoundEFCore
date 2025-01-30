using ScreenSoundSQL.Modelos;

namespace ScreenSoundSQL.Repositorios.Interfaces;

public interface IMusicaRepositorio
{
    Task AdicionarAsync(Musica artista);
    Task DeletarPorIdAsync(int id);
    Task<Musica?> ConsultarPorIdAsync(int id);
    Task<List<Musica>> ConsultarAsync();
    Task<Musica?> ConsultarPorNomeAsync(string nome);
}
