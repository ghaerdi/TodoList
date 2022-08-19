namespace todolist.DTOs;

#nullable disable

public class TaskInfoDTO
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public bool Done { get; set; }

    public int OwnerId { get; set; }
}
