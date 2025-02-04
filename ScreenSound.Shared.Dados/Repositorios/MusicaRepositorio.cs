using Microsoft.EntityFrameworkCore;
using ScreenSound.Shared.Modelos.Modelos;
using ScreenSoundSQL.Banco;
using ScreenSoundSQL.Modelos;
using ScreenSoundSQL.Repositorios.Interfaces;

namespace ScreenSound.Shared.Dados.Repositorios;

public class MusicaRepositorio(ScreenSoundContext context) : IMusicaRepositorio
{
    private ScreenSoundContext _contexto { get; set; } = context;
    public async Task AdicionarAsync(Musica musica)
    {
        await _contexto.Musicas.AddAsync(musica);
        await _contexto.SaveChangesAsync();
    }

    public async Task AtualizarPorIdAsync(int id, MusicaAtualizacaoModel model) =>
            await _contexto.Musicas
            .Where(m => m.Id == id)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(prop => prop.Nome, model.Nome)
                .SetProperty(prop => prop.AnoLancamento, model.AnoLancamento)
                .SetProperty(prop => prop.ArtistaId, model.ArtistaId));

    public async Task<List<Musica>> ConsultarAsync() =>
        await _contexto.Musicas
            .ToListAsync();

    public async Task<List<Musica>> ConsultarPorAnoLancamentoAsync(int anoLancamento) =>
        await _contexto.Musicas
            .Where(m => m.AnoLancamento == anoLancamento)
            .ToListAsync();

    public async Task<Musica?> ConsultarPorIdAsync(int id) =>
        await _contexto.Musicas
            .FirstOrDefaultAsync(m => m.Id == id);

    public async Task<Musica?> ConsultarPorNomeAsync(string nome) =>
        await _contexto.Musicas
            .FirstOrDefaultAsync(m => m.Nome!.ToUpper().Equals(nome.ToUpper()));

    public async Task DeletarPorIdAsync(int id) =>
        await _contexto.Musicas
            .Where(m => m.Id == id)
            .ExecuteDeleteAsync();
}
