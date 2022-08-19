using System.ComponentModel.DataAnnotations;

namespace todolist.DTOs;

#nullable disable

public class UserRegisterDTO
{
    [Required(ErrorMessage = "The email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    [Required(ErrorMessage = "The username is required")]
    [MinLength(3)]
    [MaxLength(20)]
    [StringLength(20)]
    public string Username { get; set; }

    [Required(ErrorMessage = "The password is required")]
    [MinLength(6)]
    [MaxLength(100)]
    [StringLength(100)]
    public string Password { get; set; }
}
