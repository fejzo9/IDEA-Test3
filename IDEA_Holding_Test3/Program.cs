using IDEA_Holding_Test3.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IDEA_Holding_Test3.Data;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Konekcija sa bazom podataka
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Konfiguracija Identity korisničkog sistema
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
})
    .AddRoles<IdentityRole>()  // Dodaj podršku za role (Admin, User)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Registracija NotificationService-a
builder.Services.AddScoped<NotificationService>();

builder.Services.AddScoped<UserManager<IdentityUser>>();
builder.Services.AddScoped<SignInManager<IdentityUser>>();
builder.Services.AddScoped<RoleManager<IdentityRole>>();

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

async Task SeedRolesAndAdminUser(IServiceProvider serviceProvider)
{
    using var scope = serviceProvider.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    string[] roleNames = { "Admin", "User", "Anonymous" };

    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Dodavanje početnog admin korisnika 
    string adminEmail = "admin@admin.com";
    string adminPassword = "Admin123!";

    if (await userManager.FindByEmailAsync(adminEmail) == null)
    {
        var adminUser = new IdentityUser
        {
            UserName = "admin",
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}

// Poziv metode nakon što je aplikacija pokrenuta
await SeedRolesAndAdminUser(app.Services);

app.Run();
