using System.Net.Http.Json;

namespace TodoBlazor.Services;

public class TodoApiClient
{
    private readonly HttpClient _http;

    public TodoApiClient(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<TodoItemDto>> GetAllAsync(string? search = null)
    {
        var url = search is not null ? $"api/todo?search={Uri.EscapeDataString(search)}" : "api/todo";
        return await _http.GetFromJsonAsync<List<TodoItemDto>>(url) ?? [];
    }

    public async Task<TodoItemDto?> GetByIdAsync(int id)
    {
        return await _http.GetFromJsonAsync<TodoItemDto>($"api/todo/{id}");
    }

    public async Task<TodoItemDto> CreateAsync(CreateTodoDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/todo", dto);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<TodoItemDto>())!;
    }

    public async Task<TodoItemDto> UpdateAsync(int id, UpdateTodoDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/todo/{id}", dto);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<TodoItemDto>())!;
    }

    public async Task<TodoItemDto> MarkCompleteAsync(int id)
    {
        var response = await _http.PatchAsync($"api/todo/{id}/complete", null);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<TodoItemDto>())!;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/todo/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<DashboardStatsDto> GetDashboardStatsAsync()
    {
        return (await _http.GetFromJsonAsync<DashboardStatsDto>("api/todo/dashboard"))!;
    }
}
