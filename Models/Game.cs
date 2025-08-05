using System.Text.Json.Serialization;
using GameLibraryAPI.Models;

namespace GameLibraryAPI.Models;

public class Game
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    public string Developer { get; set; }

    public int GenreId { get; set; }

   [JsonIgnore]
    public Genre Genre { get; set; }
}