using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _repository;

    public TodoService(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TodoResponseDto>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();
        return items.Select(MapToDto);
    }

    public async Task<IEnumerable<TodoResponseDto>> SearchAsync(string searchTerm)
    {
        var items = await _repository.SearchAsync(searchTerm);
        return items.Select(MapToDto);
    }

    public async Task<TodoResponseDto?> GetByIdAsync(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        return item == null ? null : MapToDto(item);
    }

    public async Task<TodoResponseDto> CreateAsync(CreateTodoDto dto)
    {
        var item = new TodoItem
        {
            Title = dto.Title,
            Description = dto.Description,
            Priority = dto.Priority
        };

        var created = await _repository.AddAsync(item);
        return MapToDto(created);
    }

    public async Task<TodoResponseDto?> UpdateAsync(int id, UpdateTodoDto dto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null) return null;

        existing.Title = dto.Title;
        existing.Description = dto.Description;
        existing.Priority = dto.Priority;

        if (dto.IsCompleted && !existing.IsCompleted)
        {
            existing.IsCompleted = true;
            existing.CompletedAt = DateTime.UtcNow;
        }
        else if (!dto.IsCompleted && existing.IsCompleted)
        {
            existing.IsCompleted = false;
            existing.CompletedAt = null;
        }

        var updated = await _repository.UpdateAsync(existing);
        return MapToDto(updated);
    }

    public async Task<TodoResponseDto?> MarkCompleteAsync(int id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null) return null;

        existing.IsCompleted = true;
        existing.CompletedAt = DateTime.UtcNow;

        var updated = await _repository.UpdateAsync(existing);
        return MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<DashboardStatsDto> GetDashboardStatsAsync()
    {
        var all = await _repository.GetAllAsync();

        return new DashboardStatsDto
        {
            TotalPending = all.Count(t => !t.IsCompleted),
            CompletedToday = all.Count(t => t.IsCompleted && t.CompletedAt.HasValue && t.CompletedAt.Value.Date == DateTime.UtcNow.Date),
            HighPriorityItems = all.Count(t => t.Priority == Priority.High && !t.IsCompleted)
        };
    }

    private static TodoResponseDto MapToDto(TodoItem item)
    {
        return new TodoResponseDto
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            IsCompleted = item.IsCompleted,
            Priority = item.Priority,
            CreatedAt = item.CreatedAt,
            CompletedAt = item.CompletedAt
        };
    }
}
