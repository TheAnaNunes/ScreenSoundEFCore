namespace ScreenSound.API.Reponses;

public record MusicaReponse(int Id, string Nome, int ArtistaId, string NomeArtista, ICollection<GeneroResponse> Generos);
