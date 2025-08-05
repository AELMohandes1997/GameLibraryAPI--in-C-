using System.Text.Json.Serialization;

namespace GameLibraryAPI.Models; 
public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public List<Game> Games { get; set; } = new List<Game>();
}