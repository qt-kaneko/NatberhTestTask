using System.Text.Json;
using Microsoft.EntityFrameworkCore;

using NatberhTestTask.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient(nameof(HttpClient));
builder.Services.AddTransient<PokeApiDataSeeder>();
builder.Services.AddDbContext<PokemonDbContext>(options =>
  options.UseMySQL(builder.Configuration.GetConnectionString("PokemonDb")!)
);
builder.Services.AddControllers();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

await app.Services.GetRequiredService<IServiceScopeFactory>()
                  .CreateAsyncScope()
                  .ServiceProvider
                  .GetRequiredService<PokeApiDataSeeder>()!
                  .SeedIfNeeded();

app.Run();