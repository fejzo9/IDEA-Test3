using IDEA_Holding_Test3.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;
using IDEA_Holding_Test3.Data;

var builder = WebApplication.CreateBuilder(args);

// Konekcija sa bazom podataka
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Konfiguracija Identity korisničkog sistema
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddRoles<IdentityRole>()  // Dodaj podršku za role (Admin, User)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// AuthenticationStateProvider - omogućava Blazor-u da koristi login sesije
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider>();

// Podrška za Blazor Server i Razor komponente
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Konfiguracija middleware-a
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();  // Omogućili autentifikaciju
app.UseAuthorization();   // Omogućili autorizaciju

// Omogućili Blazor da koristi Authentication & Authorization
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
