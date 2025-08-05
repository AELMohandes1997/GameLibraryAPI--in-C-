using GameLibraryAPI.Data;
using GameLibraryAPI.Models;
using GameLibraryAPI.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
namespace GameLibraryAPI.Services;
public class GameService : IGameService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GameService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GameDto> CreateAsync(GameCreateDto dto)
    {
        var game = _mapper.Map<Game>(dto);
        _context.Games.Add(game);
        await _context.SaveChangesAsync().ConfigureAwait(false);
        return _mapper.Map<GameDto>(game);
    }

    public async Task<GameDto?> GetByIdAsync(int id)
    {
        var game = await _context.Games
            .Include(g => g.Genre)
            .FirstOrDefaultAsync(g => g.Id == id && !g.IsDeleted)
            .ConfigureAwait(false);
        
        return _mapper.Map<GameDto>(game);
    }


    public async Task<IEnumerable<GameDto>> GetAllAsync()
    {
        var games = await _context.Games
            .Include(g => g.Genre)
            .Where(g => !g.IsDeleted)
            .ToListAsync()
            .ConfigureAwait(false);
            
        return _mapper.Map<List<GameDto>>(games);
    }

  
    public async Task<bool> UpdateAsync(int id, GameUpdateDto dto)
    {
        var game = await _context.Games.FindAsync(id).ConfigureAwait(false);
        if (game == null || game.IsDeleted) return false;

        _mapper.Map(dto, game);
        game.UpdatedAt = DateTime.UtcNow; // Track update timestamp
        await _context.SaveChangesAsync().ConfigureAwait(false);
        return true;
    }

    public async Task<bool> SoftDeleteAsync(int id)
    {
        var game = await _context.Games.FindAsync(id).ConfigureAwait(false);
        if (game == null || game.IsDeleted) return false;

        game.IsDeleted = true;
        game.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync().ConfigureAwait(false);
        return true;
    }


    public async Task<bool> HardDeleteAsync(int id)
    {
        var game = await _context.Games.FindAsync(id).ConfigureAwait(false);
        if (game == null) return false;

        _context.Games.Remove(game);
        await _context.SaveChangesAsync().ConfigureAwait(false);
        return true;
    }
}