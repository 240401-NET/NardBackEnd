using Data;
using Models;
using Service;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Repository;

public class TypeRepository : ITypeRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IPokeAPIService _pokeAPIService;

    public TypeRepository(ApplicationDbContext context, IPokeAPIService pokeAPIService)
    {
        _context = context;
        _pokeAPIService = pokeAPIService;
    }

    public async Task<List<Models.Type>> MakeTypeDBTable()
    {
        List<Models.Type> types = new List<Models.Type>();
        await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE Types");

        for (int i = 1; i <= 18; i++)
        {
            Models.Type dbType = new Models.Type();
            JsonDocument type = await _pokeAPIService.GetPokeType(i);

            dbType.Name = type.RootElement.GetProperty("name").GetString();
            dbType.Fire = 1;
            dbType.Water = 1;
            dbType.Electric = 1;
            dbType.Grass = 1;
            dbType.Ice = 1;
            dbType.Fighting = 1;
            dbType.Poison = 1;
            dbType.Ground = 1;
            dbType.Flying = 1;
            dbType.Psychic = 1;
            dbType.Bug = 1;
            dbType.Rock = 1;
            dbType.Ghost = 1;
            dbType.Dragon = 1;
            dbType.Dark = 1;
            dbType.Steel = 1;
            dbType.Fairy = 1;
            dbType.Normal = 1;
            dbType.Unknown = 1;
            dbType.Shadow = 1;

            var damageRelations = type.RootElement.GetProperty("damage_relations");
            // if the type attribute is in one of the damage relationships assign an appropriate damage modifier.
            for (int j = 0; j < damageRelations.GetProperty("double_damage_to").GetArrayLength(); j++)
            {
                string typeString = damageRelations.GetProperty("double_damage_to")[j].GetProperty("name").GetString();
                switch (typeString)
                {
                    case "fire":
                        dbType.Fire = 2;
                        break;
                    case "water":
                        dbType.Water = 2;
                        break;
                    case "electric":
                        dbType.Electric = 2;
                        break;
                    case "grass":
                        dbType.Grass = 2;
                        break;
                    case "ice":
                        dbType.Ice = 2;
                        break;
                    case "fighting":
                        dbType.Fighting = 2;
                        break;
                    case "poison":
                        dbType.Poison = 2;
                        break;
                    case "ground":
                        dbType.Ground = 2;
                        break;
                    case "flying":
                        dbType.Flying = 2;
                        break;
                    case "psychic":
                        dbType.Psychic = 2;
                        break;
                    case "bug":
                        dbType.Bug = 2;
                        break;
                    case "rock":
                        dbType.Rock = 2;
                        break;
                    case "ghost":
                        dbType.Ghost = 2;
                        break;
                    case "dragon":
                        dbType.Dragon = 2;
                        break;
                    case "dark":
                        dbType.Dark = 2;
                        break;
                    case "steel":
                        dbType.Steel = 2;
                        break;
                    case "fairy":
                        dbType.Fairy = 2;
                        break;
                    case "normal":
                        dbType.Normal = 2;
                        break;
                    case "unknown":
                        dbType.Unknown = 2;
                        break;
                    case "shadow":
                        dbType.Shadow = 2;
                        break;
                }
            }
            for (int j = 0; j < damageRelations.GetProperty("half_damage_to").GetArrayLength(); j++)
            {
                string typeString = damageRelations.GetProperty("half_damage_to")[j].GetProperty("name").GetString();
                switch (typeString)
                {
                    case "fire":
                        dbType.Fire = 0.5f;
                        break;
                    case "water":
                        dbType.Water = 0.5f;
                        break;
                    case "electric":
                        dbType.Electric = 0.5f;
                        break;
                    case "grass":
                        dbType.Grass = 0.5f;
                        break;
                    case "ice":
                        dbType.Ice = 0.5f;
                        break;
                    case "fighting":
                        dbType.Fighting = 0.5f;
                        break;
                    case "poison":
                        dbType.Poison = 0.5f;
                        break;
                    case "ground":
                        dbType.Ground = 0.5f;
                        break;
                    case "flying":
                        dbType.Flying = 0.5f;
                        break;
                    case "psychic":
                        dbType.Psychic = 0.5f;
                        break;
                    case "bug":
                        dbType.Bug = 0.5f;
                        break;
                    case "rock":
                        dbType.Rock = 0.5f;
                        break;
                    case "ghost":
                        dbType.Ghost = 0.5f;
                        break;
                    case "dragon":
                        dbType.Dragon = 0.5f;
                        break;
                    case "dark":
                        dbType.Dark = 0.5f;
                        break;
                    case "steel":
                        dbType.Steel = 0.5f;
                        break;
                    case "fairy":
                        dbType.Fairy = 0.5f;
                        break;
                    case "normal":
                        dbType.Normal = 0.5f;
                        break;
                    case "unknown":
                        dbType.Unknown = 0.5f;
                        break;
                    case "shadow":
                        dbType.Shadow = 0.5f;
                        break;
                }
            }
            for (int j = 0; j < damageRelations.GetProperty("no_damage_to").GetArrayLength(); j++)
            {
                string typeString = damageRelations.GetProperty("no_damage_to")[j].GetProperty("name").GetString();
                switch (typeString)
                {
                    case "fire":
                        dbType.Fire = 0;
                        break;
                    case "water":
                        dbType.Water = 0;
                        break;
                    case "electric":
                        dbType.Electric = 0;
                        break;
                    case "grass":
                        dbType.Grass = 0;
                        break;
                    case "ice":
                        dbType.Ice = 0;
                        break;
                    case "fighting":
                        dbType.Fighting = 0;
                        break;
                    case "poison":
                        dbType.Poison = 0;
                        break;
                    case "ground":
                        dbType.Ground = 0;
                        break;
                    case "flying":
                        dbType.Flying = 0;
                        break;
                    case "psychic":
                        dbType.Psychic = 0;
                        break;
                    case "bug":
                        dbType.Bug = 0;
                        break;
                    case "rock":
                        dbType.Rock = 0;
                        break;
                    case "ghost":
                        dbType.Ghost = 0;
                        break;
                    case "dragon":
                        dbType.Dragon = 0;
                        break;
                    case "dark":
                        dbType.Dark = 0;
                        break;
                    case "steel":
                        dbType.Steel = 0;
                        break;
                    case "fairy":
                        dbType.Fairy = 0;
                        break;
                    case "normal":
                        dbType.Normal = 0;
                        break;
                    case "unknown":
                        dbType.Unknown = 0;
                        break;
                    case "shadow":
                        dbType.Shadow = 0;
                        break;
                }
            }
            types.Add(dbType);
        }
        await _context.Types.AddRangeAsync(types);
        await _context.SaveChangesAsync();
        return types;
    }
}