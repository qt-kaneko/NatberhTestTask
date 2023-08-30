using System.Text.Json.Serialization;

namespace NatberhTestTask.Models.PokeApi;

/// <summary><see href="https://pokeapi.co/docs/v2#pokemontype"/></summary> 
struct PokemonType
{
  [JsonPropertyName("slot")]
  public int Slot { get; set; }

  [JsonPropertyName("type")]
  public NamedApiResource Type { get; set; }
}