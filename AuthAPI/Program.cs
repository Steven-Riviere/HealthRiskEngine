using AuthAPI.Data;
using AuthAPI.Data.SeedData;
using AuthAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ----------------------
// Controllers + Swagger
// ----------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "AuthAPI", Version = "v1" });
});

// ----------------------
// Database + Identity
// ----------------------
builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AuthConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();

// ----------------------
// Middleware
// ----------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthAPI v1");
        c.RoutePrefix = string.Empty;
    });
}

// ----------------------
// Seed Admin
// ----------------------
async Task WaitForDatabase()
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();

    const int maxRetries = 30;
    var delay = TimeSpan.FromSeconds(2);

    for (var i = 0; i < maxRetries; i++)
    {
        try
        {
            await dbContext.Database.CanConnectAsync();
            Console.WriteLine("Base de données connectée avec succès!");
            break;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Tentative {i + 1}/{maxRetries} - Connexion à la base de données échouée: {ex.Message}");
            if (i == maxRetries - 1)
                throw;
            await Task.Delay(delay);
        }
    }
}

// Attendre que la base de données soit disponible
await WaitForDatabase();

// Appliquer les migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var authDbContext = services.GetRequiredService<AuthDbContext>();
        await authDbContext.Database.MigrateAsync();

        Console.WriteLine("Migrations appliquées avec succès!");

        await IdentityData.SeedAdmin(services);
        Console.WriteLine("Données initiales créées avec succès!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur lors de l'initialisation de la base de données: {ex.Message}");
        throw;
    }
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();