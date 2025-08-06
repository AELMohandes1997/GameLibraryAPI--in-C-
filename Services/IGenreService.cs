namespace GameLibraryAPI.Services;
public interface IGenreService
{
    Task<GenreDto> CreateGenreAsync(GenreCreateDto dto);
    Task<bool> UpdateGenreAsync(int id, GenreUpdateDto dto);
    Task<bool> DeleteGenreAsync(int id);
    Task<GenreDto?> GetGenreAsync(int id);
    Task<IEnumerable<GenreDto>> GetAllGenresAsync();
}