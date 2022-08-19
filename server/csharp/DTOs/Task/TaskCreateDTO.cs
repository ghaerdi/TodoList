using System.ComponentModel.DataAnnotations;

namespace todolist.DTOs;

#nullable disable

public class TaskCreateDTO
{
    [Required(ErrorMessage = "The title is required")]
    public string Title { get; set; }

    public string Description { get; set; }
}
