using System.Text.Json.Serialization;

namespace NatberhTestTask.Models.PokeApi;

/// <summary><see href="https://pokeapi.co/docs/v2#pokemon"/></summary>
struct Pokemon
{
  [JsonPropertyName("id")]
  public int Id { get; set; }

  [JsonPropertyName("name")]
  public string Name { get; set; }

  [JsonPropertyName("types")]
  public IEnumerable<PokemonType> Type { get; set; }
}