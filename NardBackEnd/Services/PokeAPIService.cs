using Models;
using System.Text.Json;

namespace Service;

public class PokeAPIService:IPokeAPIService
{
    private readonly HttpClient _httpClient;
    private const string baseUrl = "https://pokeapi.co/api/v2/";
    
    

    public PokeAPIService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // public async Task<IEnumerable<Pokemon>> GetGen1PokemonAsync()
    // {
    //     var pokemons = new List<Pokemon>();
    //     string url = $"{baseUrl}pokemon?limit=151";

    //     while(!string.IsNullOrEmpty(url))
    //     {
    //         var response = await _httpClient.GetAsync(url);
    //         response.EnsureSuccessStatusCode();
    //         var jsonString = await response.Content.ReadAsStringAsync();
    //         var apiResponse = JsonSerializer.Deserialize<PokemonApiResponse>(jsonString);

    //         pokemons.AddRange(apiResponse.Results.Select(p => new Pokemon {Name = p.Name}));
    //         url = apiResponse.Next;
    //     }
    //     return pokemons;
    // }

    public async Task<JsonDocument> GetPokemon(string pokemon)
    {
        string url  = $"{baseUrl}pokemon/{pokemon}";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();
        var jsonDoc = await JsonDocument.ParseAsync(stream);
        return jsonDoc;


        
    }

        public async Task<JsonDocument> GetMove(string moveId)
    {
        string url  = $"{baseUrl}move/{moveId}";
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var stream = await response.Content.ReadAsStreamAsync();
        var jsonDoc = await JsonDocument.ParseAsync(stream);
        return jsonDoc;


        
    }


    private class PokemonApiResponse
    {
        public List<Result> Results {get; set;}
        public string Next {get; set;}
    }

    private class Result
    {
        public string Name {get; set;}
        public string Url {get; set;}
    }
}