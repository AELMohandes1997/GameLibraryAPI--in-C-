using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameLibraryAPI.Data;
using GameLibraryAPI.Models;
using GameLibraryAPI.Dtos;
using Microsoft.EntityFrameworkCore;
namespace GameLibraryAPI.Services;

using GameLibraryAPI.Services;
using AutoMapper;
public class GenreService : IGenreService
{
    private readonly ApplicationDbContext _context;

    public GenreService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<GenreDto> CreateGenreAsync(GenreCreateDto dto)
    {
        var genre = new Genre
        {
            Name = dto.Name,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow
        };

        _context.Genres.Add(genre);
        await _context.SaveChangesAsync().ConfigureAwait(false);

        return new GenreDto
        {
            Id = genre.Id,
            Name = genre.Name,
            Description = genre.Description,
            CreatedAt = genre.CreatedAt
        };
    }

    public async Task<GenreDto?> GetGenreAsync(int id)
    {
        var genre = await _context.Genres.FindAsync(id).ConfigureAwait(false);
        return genre == null ? null : new GenreDto
        {
            Id = genre.Id,
            Name = genre.Name,
            Description = genre.Description,
            CreatedAt = genre.CreatedAt
        };
    }

    public async Task<IEnumerable<GenreDto>> GetAllGenresAsync()
    {
        return await _context.Genres
            .Where(g => !g.IsDeleted)
            .Select(g => new GenreDto
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                CreatedAt = g.CreatedAt
            })
            .ToListAsync()
            .ConfigureAwait(false);
    }

    public async Task<bool> UpdateGenreAsync(int id, GenreUpdateDto dto)
    {
        var genre = await _context.Genres.FindAsync(id).ConfigureAwait(false);
        if (genre == null || genre.IsDeleted)
        {
            return false;
        }
    
        genre.Name = dto.Name;
        genre.Description = dto.Description;
        await _context.SaveChangesAsync().ConfigureAwait(false);
    
        return true;
    }

    public async Task<bool> DeleteGenreAsync(int id)
    {
        var genre = await _context.Genres.FindAsync(id).ConfigureAwait(false);
        if (genre == null || genre.IsDeleted)
        {
            return false;
        }

        genre.IsDeleted = true;
        await _context.SaveChangesAsync().ConfigureAwait(false);
        return true;
    }
}