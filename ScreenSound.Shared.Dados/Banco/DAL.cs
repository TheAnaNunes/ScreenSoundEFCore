using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace ScreenSoundSQL.Banco;

public class DAL<T> (DbContext context) where T : class
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
    
    public IEnumerable<T> ListarPor(Func<T, bool> condicao) => 
        [.. Context.Set<T>()
            .Where(condicao)];

    public void AtualizarCondicao(Func<T, bool> condicao, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> update)
    {
        Context.Set<T>()
            .Where(condicao)
            .AsQueryable()
            .ExecuteUpdateAsync(update);
    }
}
