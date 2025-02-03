namespace ScreenSoundSQL.Modelos;

public class Artista
{
    public virtual ICollection<Musica> Musicas { get; set; } = [];
    public string Nome { get; set; } = string.Empty;
    public string FotoPerfil { get; set; } = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
    public string? Bio { get; set; }
    public int Id { get; set; }

    public Artista(string nome, string bio)
    {
        Nome = nome;
        Bio = bio;
    }
    public Artista() { }
    public void AdicionarMusica(Musica musica) => Musicas.Add(musica);

    public void ExibirDiscografia()
    {
        Console.WriteLine($"Discografia do artista {Nome}");
        foreach (var musica in Musicas)
            Console.WriteLine($"Música: {musica.Nome} - Ano Lançamento: {musica.AnoLancamento}");
    }
    public override string ToString() => 
        @$"Id: {Id}
        Nome: {Nome}
        Foto de Perfil: {FotoPerfil}
        Bio: {Bio}";
}