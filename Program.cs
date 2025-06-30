using Loginapp.Components;
using Loginapp.Data;
using Loginapp.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ Register EmailSystem directly with Gmail credentials
builder.Services.AddScoped(sp =>
    new EmailSystem(
        "smtp.gmail.com",
        587,
        "izarakhan18@gmail.com",           // Your Gmail
        "uqvkqkptpiebbpjo"                 // Your app-specific password
    )
);

//  Add Razor Components with Interactive Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//  Add EF Core DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnectionString")));

var app = builder.Build();

// ✅ Configure pipeline
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
