namespace ScreenSoundSQL.Modelos;

internal class ArtistaAtualizacaoModel(string nome, string fotoPerfil, string bio)
{
    public string Nome { get; set; } = nome;
    public string FotoPerfil { get; set; } = fotoPerfil;
    public string Bio { get; set; } = bio;
}
