using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.Requests;

public record MusicaRequest(string Nome, int? AnoLancamento, int ArtistaId, ICollection<GeneroRequest>? Generos = null);
