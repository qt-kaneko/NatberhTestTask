using System.Text.Json.Serialization;

public class PokemonType
{
  [JsonIgnore] public int Id { get; set; }
  
  [JsonIgnore] public int PokemonId { get; set; }
  [JsonIgnore] public Pokemon? Pokemon { get; set; }

  public required int Slot { get; set; }
  public required string Name { get; set; }
}