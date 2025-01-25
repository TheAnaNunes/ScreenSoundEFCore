namespace ScreenSoundSQL.Modelos;

internal class Musica(string nome)
{
    public string Nome { get; set; } = nome;
    public int Id { get; set; }

    public void ExibirFichaTecnica()
    {
        Console.WriteLine($"Nome: {Nome}");
    }

    public override string ToString() => 
        @$"Id: {Id}
        Nome: {Nome}";
}