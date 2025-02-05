using ScreenSound.Shared.Modelos.Modelos;
using ScreenSoundSQL.Modelos;

namespace ScreenSound.Shared.Dados.Repositorios.Interfaces;

public interface IGeneroRepositorio
{
    Task AdicionarAsync(Genero genero);
    Task DeletarPorIdAsync(int id);
    Task<Genero?> ConsultarPorIdAsync(int id);
    Task<List<Genero>> ConsultarAsync();
    Task AtualizarPorIdAsync(int id, GeneroModel model);
    Task<Genero?> ConsultarPorNomeAsync(string nome);
}
