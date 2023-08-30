using System.Text.Json.Serialization;

namespace NatberhTestTask.Models.PokeApi;

/// <summary><see href="https://pokeapi.co/docs/v2#namedapiresource"/></summary> 
struct NamedApiResource
{
  [JsonPropertyName("name")]
  public string Name { get; set; }

  [JsonPropertyName("url")]
  public string Url { get; set; }
}