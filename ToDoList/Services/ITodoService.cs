using ToDoList.DTOs;

namespace ToDoList.Services;

public interface ITodoService
{
    Task<IEnumerable<TodoResponseDto>> GetAllAsync();
    Task<IEnumerable<TodoResponseDto>> SearchAsync(string searchTerm);
    Task<TodoResponseDto?> GetByIdAsync(int id);
    Task<TodoResponseDto> CreateAsync(CreateTodoDto dto);
    Task<TodoResponseDto?> UpdateAsync(int id, UpdateTodoDto dto);
    Task<TodoResponseDto?> MarkCompleteAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<DashboardStatsDto> GetDashboardStatsAsync();
}
