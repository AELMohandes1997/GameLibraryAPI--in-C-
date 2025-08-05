namespace GameLibraryAPI.Dtos;
using System.ComponentModel.DataAnnotations;

public class GameCreateDto
{
    [Required]
    public string Title { get; set; }
    
    [Range(1950, 2100)]
    public int ReleaseYear { get; set; }
    
    [Required]
    public string Developer { get; set; }
    
    [Required]
    public int GenreId { get; set; }
}


public class GameDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int ReleaseYear { get; set; }
    public string Developer { get; set; }
    public GenreDto Genre { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class GameUpdateDto : GameCreateDto { }
public class GenreDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}