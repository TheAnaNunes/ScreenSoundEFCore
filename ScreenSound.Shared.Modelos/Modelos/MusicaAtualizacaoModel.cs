namespace ScreenSound.Shared.Modelos.Modelos;

public class MusicaAtualizacaoModel(string nome, int? anoLancamento,int artistaId)
{
    public string Nome { get; set; } = nome;
    public int? AnoLancamento { get; set; } = anoLancamento;
    public int ArtistaId { get; set; } = artistaId;
}
