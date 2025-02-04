namespace ScreenSoundSQL.Modelos;

public class Musica
{
    public Musica(string nome, int? anoLancamento, int artistaId)
    {
        Nome = nome;
        AnoLancamento = anoLancamento;
        ArtistaId = artistaId;
    }

    public Musica(string nome)
    {
        Nome = nome;
    }
    public Musica() { }

    public string Nome { get; set; } = string.Empty;
    public int Id { get; set; }
    public int? AnoLancamento { get; set; }
    public virtual Artista? Artista { get; }
    public int ArtistaId { get; set; }

    public void ExibirFichaTecnica() => Console.WriteLine($"Nome: {Nome} - Ano Lançamento: {AnoLancamento}");

    public override string ToString() => 
        @$"Id: {Id}
        Nome: {Nome}";
}