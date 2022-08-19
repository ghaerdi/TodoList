using System.ComponentModel.DataAnnotations;

namespace todolist.Entities;

#nullable disable

public class TaskEntity
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "The title is required")]
    public string Title { get; set; }

    public string Description { get; set; }

    public bool Done { get; set; } = false;

    public UserEntity Owner { get; set; }

    public int OwnerId { get; set; }
}
