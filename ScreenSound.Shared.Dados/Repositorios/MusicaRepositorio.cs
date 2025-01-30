using Microsoft.EntityFrameworkCore;
using ScreenSoundSQL.Banco;
using ScreenSoundSQL.Modelos;
using ScreenSoundSQL.Repositorios.Interfaces;

namespace ScreenSound.Shared.Dados.Repositorios;

public class MusicaRepositorio(ScreenSoundContext context) : IMusicaRepositorio
{
    private ScreenSoundContext _context { get; set; } = context;
    public async Task AdicionarAsync(Musica artista)
    {
        await _context.Musicas.AddAsync(artista);
        await _context.SaveChangesAsync();
    }
    public async Task<List<Musica>> ConsultarAsync() =>
        await _context.Musicas
            .ToListAsync();

    public async Task<Musica?> ConsultarPorIdAsync(int id) => 
        await _context.Musicas
            .FirstOrDefaultAsync(m => m.Id == id);

    public async Task<Musica?> ConsultarPorNomeAsync(string nome) =>
        await _context.Musicas
            .FirstOrDefaultAsync(m => m.Nome.Equals(nome));

    public async Task DeletarPorIdAsync(int id) =>
        await _context.Musicas
            .Where(m => m.Id == id)
            .ExecuteDeleteAsync();
}
