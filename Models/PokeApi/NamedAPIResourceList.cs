using System.Text.Json.Serialization;

namespace NatberhTestTask.Models.PokeApi;

/// <summary><see href="https://pokeapi.co/docs/v2#namedapiresourcelist"/></summary> 
struct NamedAPIResourceList
{
  [JsonPropertyName("results")]
  public IEnumerable<NamedApiResource> Results { get; set; }
}