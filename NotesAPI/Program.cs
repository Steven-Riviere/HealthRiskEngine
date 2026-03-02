using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using NotesAPI.Data;
using NotesAPI.Services;

var builder = WebApplication.CreateBuilder(args);

//Connexion MongoDB
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDB");
mongoConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__MongoDb") ?? mongoConnectionString;


//Nom bdd
var databaseName = builder.Configuration["NotesDatabase:DatabaseName"];

var mongoClient = new MongoClient(mongoConnectionString);
var database = mongoClient.GetDatabase(databaseName);

NoteData.SeedNotes(database);

builder.Services.AddSingleton(database);
builder.Services.AddScoped<INoteService, NoteService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NotesAPI", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Entrez un token JWT valide (format : Bearer {token})",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Swagger en dev
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NotesAPI v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
