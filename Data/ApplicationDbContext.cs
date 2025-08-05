using Microsoft.EntityFrameworkCore;
namespace GameLibraryAPI.Data;
using GameLibraryAPI.Models;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Game> Games { get; set; }
}