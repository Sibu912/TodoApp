using System.ComponentModel.DataAnnotations;
using TodoBlazor.Models;

namespace TodoBlazor.DTOs;

public class CreateTodoDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000, ErrorMessage = "Description must not exceed 1000 characters")]
    public string? Description { get; set; }

    public Priority Priority { get; set; } = Priority.Medium;
}
