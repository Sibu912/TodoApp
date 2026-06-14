using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models;

public enum Priority { Low, Medium, High }

public class TodoItem
{
    public int Id { get; set; }

    [Required]
    [StringLength(200, MinimumLength = 1)]
    public string Title { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }

    public bool IsCompleted { get; set; }

    public Priority Priority { get; set; } = Priority.Medium;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; set; }
}
