using Microsoft.EntityFrameworkCore;
using ScreenSoundSQL.Banco;
using ScreenSoundSQL.Modelos;
using ScreenSoundSQL.Repositorios.Interfaces;

namespace ScreenSoundSQL.Repositorios;

internal class ArtistaRepositorio(ScreenSoundContext contexto) : IArtistaRepositorio
{
    private ScreenSoundContext _contexto = contexto;
    public async Task AdicionarAsync(Artista artista)
    {
        await _contexto.AddAsync(artista);
        await _contexto.SaveChangesAsync();
    }

    public async Task AtualizarPorIdAsync(int id, ArtistaAtualizacaoModel model)
    {
        await _contexto.Artistas
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(prop => prop.FotoPerfil,model.FotoPerfil)
                .SetProperty(prop => prop.Bio, model.Bio)
                .SetProperty(prop => prop.Nome, model.Nome));
    }

    public async Task<List<Artista>> ConsultarAsync() =>
        await _contexto.Artistas.ToListAsync();

    public async Task<Artista?> ConsultarPorIdAsync(int id) =>
        await _contexto.Artistas.FirstOrDefaultAsync(artista => artista.Id == id);

    public async Task DeletarPorIdAsync(int id) =>
        await _contexto.Artistas.Where(artista => artista.Id == id).ExecuteDeleteAsync();
}
