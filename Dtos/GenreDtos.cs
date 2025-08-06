using System.ComponentModel.DataAnnotations;

public class GenreCreateDto
{
    [Required, StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }
}

public class GenreUpdateDto : GenreCreateDto { }

public class GenreDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}