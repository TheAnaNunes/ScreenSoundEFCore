using Microsoft.EntityFrameworkCore;

namespace ScreenSoundSQL.Banco;

internal class DAL<T> (DbContext context) where T : class
{
    private readonly DbContext context = context;
    protected DbContext Context => context;

    public IEnumerable<T> Listar() => [.. Context.Set<T>()];
    public void Adicionar(T objeto)
    {
        Context.Set<T>().Add(objeto);
        Context.SaveChanges();
    }
    public void Atualizar(T objeto)
    {
        Context.Set<T>().Update(objeto);
        Context.SaveChanges();
    }
    public void Deletar(T objeto)
    {
        Context.Set<T>().Remove(objeto);
        Context.SaveChanges();
    }

    public T? RecuperarPor(Func<T, bool> condicao) =>
        context
            .Set<T>()
            .FirstOrDefault(condicao);
}
