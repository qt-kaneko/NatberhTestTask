using Microsoft.EntityFrameworkCore;

public class PokemonDbContext : DbContext
{
  public required DbSet<Pokemon> Pokemons { get; set; }

  public PokemonDbContext(DbContextOptions<PokemonDbContext> options) : base(options) 
  {
    Database.EnsureCreated();
  }
}