using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PatientsAPI.Data;
using PatientsAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Patients
builder.Services.AddDbContext<PatientsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PatientsConnection")));

builder.Services.AddScoped<IPatientService, PatientService>();

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Veuillez entrer un token JWT valide (format : Bearer {token})",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    // Application du schéma à toutes les routes protégées
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Demo API v1");
        c.RoutePrefix = string.Empty; // Pour accéder à Swagger directement via https://localhost:5215/
    });
}

app.UseHttpsRedirection();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var patientsDbContext = services.GetRequiredService<PatientsDbContext>();
        await patientsDbContext.Database.MigrateAsync();

        Console.WriteLine("Migrations appliquées avec succès!");
        Console.WriteLine("Données initiales créées avec succès!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur lors de l'initialisation de la base de données: {ex.Message}");
        throw;
    }
}

app.Run();
