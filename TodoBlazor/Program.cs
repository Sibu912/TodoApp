using TodoBlazor.Components;
using TodoBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var apiUrl = Environment.GetEnvironmentVariable("API_BASE_URL")
             ?? builder.Configuration.GetValue<string>("ApiBaseUrl")
             ?? "http://localhost:5000/";
builder.Services.AddHttpClient<TodoApiClient>(client =>
{
    client.BaseAddress = new Uri(apiUrl);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
    app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
