using Microsoft.EntityFrameworkCore;
using PlayersAPI.Data;
using PlayersAPI.Models;
using PlayersAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PlayerContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IPlayerService, PlayerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<PlayerContext>();
    context.Database.EnsureCreated();

    // Add initial data
    if (!context.Players.Any())
    {
        context.Players.AddRange(
            new Player { Id = 1, Name = "Cristiano Ronaldo", Age = 38, BirthPlace = "Europe" },
            new Player { Id = 2, Name = "Lionel Messi", Age = 36, BirthPlace = "South America" },
            new Player { Id = 3, Name = "Karim Benzema", Age = 35, BirthPlace = "Europe" },
            new Player { Id = 4, Name = "Erling Haaland", Age = 23, BirthPlace = "Europe" },
            new Player { Id = 5, Name = "Kylian Mbappe", Age = 24, BirthPlace = "Europe" }
        );
        context.SaveChanges();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/api/settings", (IConfiguration config) =>
{
    var connectionString = config.GetConnectionString("DefaultConnection");
    return Results.Ok(new { ConnectionString = connectionString });
});
app.Run();
