using GameLibraryAPI.Dtos;
namespace GameLibraryAPI.Services;
public interface IGameService
{
    Task<GameDto> CreateAsync(GameCreateDto dto);
    Task<bool> UpdateAsync(int id, GameUpdateDto dto);
    Task<bool> SoftDeleteAsync(int id);
    Task<GameDto?> GetByIdAsync(int id);
    Task<IEnumerable<GameDto>> GetAllAsync();
}
