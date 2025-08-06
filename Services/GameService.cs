using GameLibraryAPI.Data;
using GameLibraryAPI.Models;
using GameLibraryAPI.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace GameLibraryAPI.Services;
public class GameService : IGameService
{
    private readonly ApplicationDbContext _context;

    public GameService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GameDto> CreateGameAsync(GameCreateDto dto)
    {
        // Manual mapping from DTO to Model
        var game = new Game
        {
            Title = dto.Title,
            ReleaseYear = dto.ReleaseYear,
            Developer = dto.Developer,
            GenreId = dto.GenreId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Games.Add(game);
        await _context.SaveChangesAsync().ConfigureAwait(false);

        // Manual mapping from Model to DTO
        return new GameDto
        {
            Id = game.Id,
            Title = game.Title,
            ReleaseYear = game.ReleaseYear,
            Developer = game.Developer,
            GenreName = (await _context.Genres.FindAsync(game.GenreId))?.Name,
            CreatedAt = game.CreatedAt
        };
    }

    public async Task<GameDto?> GetGameAsync(int id)
    {
        var game = await _context.Games
            .Include(g => g.Genre)
            .FirstOrDefaultAsync(g => g.Id == id)
            .ConfigureAwait(false);

        if (game == null) return null;

        return new GameDto
        {
            Id = game.Id,
            Title = game.Title,
            ReleaseYear = game.ReleaseYear,
            Developer = game.Developer,
            GenreName = game.Genre?.Name,
            CreatedAt = game.CreatedAt
        };
    }

    // Implementing missing interface methods

    public async Task<GameDto> CreateAsync(GameCreateDto dto)
    {
        // You can call your existing CreateGameAsync or implement logic here
        return await CreateGameAsync(dto);
    }

    public async Task<bool> UpdateAsync(int id, GameUpdateDto dto)
    {
        var game = await _context.Games.FindAsync(id);
        if (game == null) return false;
    
        game.Title = dto.Title;
        game.ReleaseYear = dto.ReleaseYear;
        game.Developer = dto.Developer;
        game.GenreId = dto.GenreId;
        await _context.SaveChangesAsync().ConfigureAwait(false);
    
        return true;
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var game = await _context.Games.FindAsync(id);
        if (game == null) return false;

        // Assuming you have a property like IsDeleted for soft delete
        game.IsDeleted = true;
        await _context.SaveChangesAsync().ConfigureAwait(false);
        return true;
    }

    public async Task<GameDto?> GetByIdAsync(int id)
    {
        return await GetGameAsync(id);
    }

    public async Task<IEnumerable<GameDto>> GetAllAsync()
    {
        var games = await _context.Games
            .Include(g => g.Genre)
            .Where(g => !g.IsDeleted)
            .ToListAsync()
            .ConfigureAwait(false);

        return games.Select(game => new GameDto
        {
            Id = game.Id,
            Title = game.Title,
            ReleaseYear = game.ReleaseYear,
            Developer = game.Developer,
            GenreName = game.Genre?.Name,
            CreatedAt = game.CreatedAt
        });
    }
}