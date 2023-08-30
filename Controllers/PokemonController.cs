using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace NatberhTestTask.Controllers;

[ApiController, Route("[controller]")]
public class PokemonsController : ControllerBase
{
  readonly PokemonDbContext _pokemonDb;

  public PokemonsController(PokemonDbContext pokemonDb)
  {
    _pokemonDb = pokemonDb;
  }

  [HttpGet("get")]
  public async Task<IActionResult> Get([BindRequired] int id)
  {
    var pokemons = (IEnumerable<Pokemon>)_pokemonDb.Pokemons.Include(p => p.Types);

    var pokemon = pokemons.FirstOrDefault(p => p.Id == id);
    if (pokemon == null) return NotFound();

    return Ok(pokemon);
  }

  [HttpPost("create")]
  public async Task<IActionResult> Create([FromBody] Pokemon pokemon)
  {
    var pokemons = (IEnumerable<Pokemon>)_pokemonDb.Pokemons;

    if (pokemons.Any(p => p.Id == pokemon.Id))
    {
      return BadRequest("Pokemon with such id already exists.");
    }

    _pokemonDb.Pokemons.Add(pokemon);
    _pokemonDb.SaveChanges();

    return Ok();
  }

  [HttpPatch("update")]
  public async Task<IActionResult> Update([BindRequired] int id, [FromBody] Pokemon newPokemon)
  {
    var pokemons = (IEnumerable<Pokemon>)_pokemonDb.Pokemons;

    var oldPokemon = pokemons.FirstOrDefault(p => p.Id == id);
    if (oldPokemon == null) return BadRequest("Pokemon with such id does not exists.");

    _pokemonDb.Pokemons.Remove(oldPokemon);
    _pokemonDb.Pokemons.Add(newPokemon);
    _pokemonDb.SaveChanges();

    return Ok();
  }

  [HttpDelete("delete")]
  public async Task<IActionResult> Delete([BindRequired] int id)
  {
    var pokemons = (IEnumerable<Pokemon>)_pokemonDb.Pokemons;

    var pokemon = pokemons.FirstOrDefault(p => p.Id == id);
    if (pokemon == null) return BadRequest("Pokemon with such id does not exists.");

    _pokemonDb.Pokemons.Remove(pokemon);
    _pokemonDb.SaveChanges();

    return Ok();
  }

  [HttpGet("list")]
  public async Task<IActionResult> List([BindRequired, Range(1, double.PositiveInfinity)] int page,
                                        string? name, string? type)
  {
    var pokemons = (IEnumerable<Pokemon>)_pokemonDb.Pokemons.Include(p => p.Types);

    pokemons = pokemons.Skip(page * 10).Take(10);

    if (name != null) pokemons = pokemons.Where(p => p.Name == name);
    if (type != null) pokemons = pokemons.Where(p => p.Types.Any(t => t.Name == type));

    return Ok(pokemons);
  }
}