using PokeApi = NatberhTestTask.Models.PokeApi;

namespace NatberhTestTask.Services;

public class PokeApiDataSeeder
{
  readonly PokemonDbContext _pokemonDb;
  readonly HttpClient _http;

  public PokeApiDataSeeder(PokemonDbContext pokemonDb, HttpClient http)
  {
    _pokemonDb = pokemonDb;
    _http = http;
  }

  public async Task SeedIfNeeded()
  {
    if (_pokemonDb.Pokemons.Any()) return;

    var pokemonsRequest = new HttpRequestMessage(HttpMethod.Get, "https://pokeapi.co/api/v2/pokemon?limit=50");
    var pokemonsResponse = await _http.SendAsync(pokemonsRequest);

    var resourceList = await pokemonsResponse.Content.ReadFromJsonAsync<PokeApi.NamedAPIResourceList>();

    var pokemons = await Task.WhenAll(resourceList.Results.Select(async (resoure) => {
      var pokemonRequest = new HttpRequestMessage(HttpMethod.Get, resoure.Url);
      var pokemonResponse = await _http.SendAsync(pokemonRequest);

      var pokemonResponseContent = await pokemonResponse.Content.ReadFromJsonAsync<PokeApi.Pokemon>();

      return new Pokemon() {
        Id = pokemonResponseContent.Id,
        Name = pokemonResponseContent.Name,
        Types = pokemonResponseContent.Type.Select(t => new PokemonType() {
          Slot = t.Slot,
          Name = t.Type.Name
        }).ToArray()
      };
    }));

    _pokemonDb.AddRange(pokemons);
    _pokemonDb.SaveChanges();
  }
}