namespace ScreenSoundSQL.Modelos;

public class Musica(string nome)
{
    public string Nome { get; set; } = nome;
    public int Id { get; set; }
    public int? AnoLancamento { get; set; }
    public virtual Artista? Artista { get; }

    public void ExibirFichaTecnica() => Console.WriteLine($"Nome: {Nome} - Ano Lançamento: {AnoLancamento}");

    public override string ToString() => 
        @$"Id: {Id}
        Nome: {Nome}";
}