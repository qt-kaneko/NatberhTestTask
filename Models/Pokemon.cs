public class Pokemon
{
  public required string Name { get; set; }
  public required ICollection<PokemonType> Types { get; set; }
  public required int Id { get; set; }
}