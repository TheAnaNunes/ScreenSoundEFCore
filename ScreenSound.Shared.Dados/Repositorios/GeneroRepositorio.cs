using Microsoft.EntityFrameworkCore;
using ScreenSound.Shared.Dados.Repositorios.Interfaces;
using ScreenSound.Shared.Modelos.Modelos;
using ScreenSoundSQL.Banco;
using ScreenSoundSQL.Modelos;

namespace ScreenSound.Shared.Dados.Repositorios;

public class GeneroRepositorio(ScreenSoundContext context) : IGeneroRepositorio
{
    private ScreenSoundContext _contexto { get; set; } = context;
    public async Task AdicionarAsync(Genero genero)
    {
        await _contexto.Generos.AddAsync(genero);
        await _contexto.SaveChangesAsync();
    }

    public async Task AtualizarPorIdAsync(int id, GeneroModel model) =>
            await _contexto.Generos
                .Where(g => g.Id == id)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(prop => prop.Nome, model.Nome)
                    .SetProperty(prop => prop.Descricao, model.Descricao));

    public async Task<List<Genero>> ConsultarAsync() =>
        await _contexto.Generos
            .ToListAsync();

    public async Task<Genero?> ConsultarPorIdAsync(int id) =>
        await _contexto.Generos
            .FirstOrDefaultAsync(g => g.Id == id);

    public async Task<Genero?> ConsultarPorNomeAsync(string nome) =>
        await _contexto.Generos
            .FirstOrDefaultAsync(g => g.Nome.ToUpper() == nome.ToUpper());

    public async Task DeletarPorIdAsync(int id) =>
        await _contexto.Generos
            .Where(g => g.Id == id)
            .ExecuteDeleteAsync();
        
}
