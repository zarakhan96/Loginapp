using Loginapp.Components;
using Loginapp.Data;
using Loginapp.Services;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✔ Register custom services
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnectionString")));

builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<UserSessionService>();

// ✔ Register EmailSystem with proper constructor
builder.Services.AddScoped(sp =>
    new EmailSystem(
        "smtp.gmail.com",
        587,
        "ajwanadeem14@gmail.com",      // Replace with your sender email
        "uqvkqkptpiebbpjo"             // App password (keep this safe)
    )
);

builder.Services.AddScoped<ToastService>();


// ✔ Razor + Blazor services
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddServerSideBlazor().AddCircuitOptions(options =>
{
    options.DetailedErrors = true;
});

var app = builder.Build();

// ✔ Configure middleware pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
