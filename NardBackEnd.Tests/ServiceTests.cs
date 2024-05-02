using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;
using Moq;
using Repository;
using Service;
using System.Text.Json;
using System.Net.Http;

namespace NardBackEnd.Tests;

public class ServiceTests
{
    [Fact]
    public async void PokemonServiceShouldGetGen1()
    {
        Mock<IPokemonRepository> testPokeRepo = new Mock<IPokemonRepository>();

        List<Pokemon> testPoke = 
        [
            new Pokemon
            {
                Id = 1,
                Name = "bulbasaur",
                Types = ["grass","poison"],
                Hp = 45,
                Atk = 49,
                Satk = 65,
                Def = 49,
                Sdef = 65,
                Spd = 45,
                MovePool = ["cut","bind","vine-whip" ],
                Sprite = "1.png"
            }
        ];

        testPokeRepo.Setup(Repository => Repository.MakePokemonDBTable()).ReturnsAsync(testPoke);
        
        Mock<HttpClient> http = new  Mock<HttpClient>();
        Mock<ApplicationDbContext> context = new Mock<ApplicationDbContext>();

        PokemonService pokeService = new PokemonService(http.Object,context.Object,testPokeRepo.Object);

        List<Pokemon> allPoke = await pokeService.MakePokemonDBTable();
    }

    //SearchPokemon should take a pokemon name and return its data
    [Fact]
    public async void PokemonServiceShouldSearchPokemon()
    {
        Mock<IPokemonRepository> testPokeRepo = new Mock<IPokemonRepository>();

        List<Pokemon> testPoke = 
        [
            new Pokemon
            {
                Id = 1,
                Name = "bulbasaur",
                Types = ["grass","poison"],
                Hp = 45,
                Atk = 49,
                Satk = 65,
                Def = 49,
                Sdef = 65,
                Spd = 45,
                MovePool = ["cut","bind","vine-whip" ],
                Sprite = "1.png"
            }
        ];

        testPokeRepo.Setup(Repository => Repository.MakePokemonDBTable()).ReturnsAsync(testPoke);
        
        Mock<HttpClient> http = new  Mock<HttpClient>();
        Mock<ApplicationDbContext> context = new Mock<ApplicationDbContext>();

        PokemonService pokeService = new PokemonService(http.Object,context.Object,testPokeRepo.Object);

        Pokemon foundPoke = await pokeService.SearchPokemon("bulbasaur");

        Assert.Equal("bulbasaur", foundPoke.Name);
    }

    //GetPokemon should take a pokemon id and return its data
    [Fact]
    public async void PokemonServiceShouldGetPokemon()
    {
        Mock<IPokemonRepository> testPokeRepo = new Mock<IPokemonRepository>();

        List<Pokemon> testPoke = 
        [
            new Pokemon
            {
                Id = 1,
                Name = "bulbasaur",
                Types = ["grass","poison"],
                Hp = 45,
                Atk = 49,
                Satk = 65,
                Def = 49,
                Sdef = 65,
                Spd = 45,
                MovePool = ["cut","bind","vine-whip" ],
                Sprite = "1.png"
            }
        ];

        testPokeRepo.Setup(Repository => Repository.MakePokemonDBTable()).ReturnsAsync(testPoke);
        
        Mock<HttpClient> http = new  Mock<HttpClient>();
        Mock<ApplicationDbContext> context = new Mock<ApplicationDbContext>();

        PokemonService pokeService = new PokemonService(http.Object,context.Object,testPokeRepo.Object);

        Pokemon foundPoke = await pokeService.GetPokemon(1);

        Assert.Equal("bulbasaur", foundPoke.Name);
    }

    //GetAllPokemon should return all pokemon in the database
    [Fact]
    public async void PokemonServiceShouldGetAllPokemon()
    {
        Mock<IPokemonRepository> testPokeRepo = new Mock<IPokemonRepository>();

        List<Pokemon> testPoke = 
        [
            new Pokemon
            {
                Id = 1,
                Name = "bulbasaur",
                Types = ["grass","poison"],
                Hp = 45,
                Atk = 49,
                Satk = 65,
                Def = 49,
                Sdef = 65,
                Spd = 45,
                MovePool = ["cut","bind","vine-whip" ],
                Sprite = "1.png"
            }
        ];

        testPokeRepo.Setup(Repository => Repository.MakePokemonDBTable()).ReturnsAsync(testPoke);
        
        Mock<HttpClient> http = new  Mock<HttpClient>();
        Mock<ApplicationDbContext> context = new Mock<ApplicationDbContext>();

        PokemonService pokeService = new PokemonService(http.Object,context.Object,testPokeRepo.Object);

        IEnumerable<Pokemon> allPoke = await pokeService.GetAllPokemon();

        Assert.Equal("bulbasaur", allPoke.First().Name);
    }

    //PokeAPI GetPokemon should return a pokemon from the API
    [Fact]
    public async void PokeAPIServiceShouldGetPokemon()
    {
        Mock<HttpClient> http = new  Mock<HttpClient>();
        Mock<ApplicationDbContext> context = new Mock<ApplicationDbContext>();
        PokeAPIService pokeAPI = new PokeAPIService(http.Object);

        JsonDocument poke = await pokeAPI.GetPokemon("1");

        Assert.Equal("bulbasaur", poke.RootElement.GetProperty("name").GetString());
    }

    //PokeAPI GetMove should return a move from the API
    [Fact]
    public async void PokeAPIServiceShouldGetMove()
    {
        Mock<HttpClient> http = new  Mock<HttpClient>();
        Mock<ApplicationDbContext> context = new Mock<ApplicationDbContext>();
        PokeAPIService pokeAPI = new PokeAPIService(http.Object);

        JsonDocument move = await pokeAPI.GetMove("1");

        Assert.Equal("pound", move.RootElement.GetProperty("name").GetString());
    }

    //PokeAPI GetPokeType should return a type from the API
    [Fact]
    public async void PokeAPIServiceShouldGetPokeType()
    {
        Mock<HttpClient> http = new  Mock<HttpClient>();
        Mock<ApplicationDbContext> context = new Mock<ApplicationDbContext>();
        PokeAPIService pokeAPI = new PokeAPIService(http.Object);

        JsonDocument type = await pokeAPI.GetPokeType(1);

        Assert.Equal("normal", type.RootElement.GetProperty("name").GetString());
    }

    //from the move service test GetMoves, GetMove, GetMoveByName, GetRandomMoveSet
    [Fact]
    public async void MoveServiceShouldGetMoves()
    {
        Mock<IMoveRepository> testMoveRepo = new Mock<IMoveRepository>();

        List<Move> testMove = 
        [
            new Move
            {
                MoveId = 1,
                Name = "tackle",
                Type = "normal",
                DamageClass = "physical",
                Power = 40,
                Acc = 100,
                Pp = 35,
                Description = "none",
                Priority = 0
            },
            new Move
            {
                MoveId = 2,
                Name = "scratch",
                Type = "normal",
                DamageClass = "physical",
                Power = 40,
                Acc = 100,
                Pp = 35,
                Description = "none",
                Priority = 0
            },
            new Move
            {
                MoveId = 3,
                Name = "ember",
                Type = "fire",
                DamageClass = "special",
                Power = 40,
                Acc = 100,
                Pp = 35,
                Description = "none",
                Priority = 0
            },
            new Move
            {
                MoveId = 4,
                Name = "water-gun",
                Type = "water",
                DamageClass = "special",
                Power = 40,
                Acc = 100,
                Pp = 35,
                Description = "none",
                Priority = 0
            },
            new Move
            {
                MoveId = 5,
                Name = "thunder-shock",
                Type = "electric",
                DamageClass = "special",
                Power = 40,
                Acc = 100,
                Pp = 35,
                Description = "none",
                Priority = 0
            },
            new Move
            {
                MoveId = 6,
                Name = "confusion",
                Type = "psychic",
                DamageClass = "special",
                Power = 50,
                Acc = 100,
                Pp = 25,
                Description = "none",
                Priority = 0
            }
        ];

        testMoveRepo.Setup(Repository => Repository.MakeMovesTable()).ReturnsAsync(testMove);

        Mock<HttpClient> http = new  Mock<HttpClient>();
        Mock<ApplicationDbContext> context = new Mock<ApplicationDbContext>();
        Mock<IPokemonService> pokeService = new Mock<IPokemonService>();

        MoveService moveService = new MoveService(http.Object,testMoveRepo.Object,context.Object,pokeService.Object);

        List<Move> allMoves = await moveService.GetMoves();

        Assert.Equal("tackle", allMoves.First().Name);
    }

    [Fact]
    public async void MoveServiceShouldGetMove()
    {
        Mock<IMoveRepository> testMoveRepo = new Mock<IMoveRepository>();

        List<Move> testMove = 
        [
            new Move
            {
                MoveId = 1,
                Name = "tackle",
                Type = "normal",
                DamageClass = "physical",
                Power = 40,
                Acc = 100,
                Pp = 35,
                Description = "none",
                Priority = 0
            },
            new Move
            {
                MoveId = 2,
                Name = "scratch",
                Type = "normal",
                DamageClass = "physical",
                Power = 40,
                Acc = 100,
                Pp = 35,
                Description = "none",
                Priority = 0
            },
            new Move
            {
                MoveId = 3,
                Name = "ember",
                Type = "fire",
                DamageClass = "special",
                Power = 40,
                Acc = 100,
                Pp = 35,
                Description = "none",
                Priority = 0
            },
            new Move
            {
                MoveId = 4,
                Name = "water-gun",
                Type = "water",
                DamageClass = "special",
                Power = 40,
                Acc = 100,
                Pp = 35,
                Description = "none",
                Priority = 0
            },
            new Move
            {
                MoveId = 5,
                Name = "thunder-shock",
                Type = "electric",
                DamageClass = "special",
                Power = 40,
                Acc = 100,
                Pp = 35,
                Description = "none",
                Priority = 0
            },
            new Move
            {
                MoveId = 6,
                Name = "confusion",
                Type = "psychic",
                DamageClass = "special",
                Power = 50,
                Acc = 100,
                Pp = 25,
                Description = "none",
                Priority = 0
            }
        ];

        testMoveRepo.Setup(Repository => Repository.MakeMovesTable()).ReturnsAsync(testMove);

        Mock<HttpClient> http = new  Mock<HttpClient>();
        Mock<ApplicationDbContext> context = new Mock<ApplicationDbContext>();
        Mock<IPokemonService> pokeService = new Mock<IPokemonService>();

        MoveService moveService = new MoveService(http.Object,testMoveRepo.Object,context.Object,pokeService.Object);

        Move foundMove = await moveService.GetMove(1);

        Assert.Equal("tackle", foundMove.Name);
    }

    [Fact]
    public async void MoveServiceShouldGetMoveByName()
    {
        Mock<IMoveRepository> testMoveRepo = new Mock<IMoveRepository>();

        List<Move> testMove = 
        [
            new Move
            {
                MoveId = 1,
                Name = "tackle",
                Type = "normal",
                DamageClass = "physical",
                Power = 40,
                Acc = 100,
                Pp = 35,
                Description = "none",
                Priority = 0
            },
            new Move
            {
                MoveId = 2,
                Name = "scratch",
                Type = "normal",
                DamageClass = "physical",
                Power = 40,
                Acc = 100,
                Pp = 35,
                Description = "none",
                Priority = 0
            },
            new Move
            {
                MoveId = 3,
                Name = "ember",
                Type = "fire",
                DamageClass = "special",
                Power = 40,
                Acc = 100,
                Pp = 35,
                Description = "none",
                Priority = 0
            },
            new Move
            {
                MoveId = 4,
                Name = "water-gun",
                Type = "water",
                DamageClass = "special",
                Power = 40,
                Acc = 100,
                Pp = 35,
                Description = "none",
                Priority = 0
            },
            new Move
            {
                MoveId = 5,
                Name = "thunder-shock",
                Type = "electric",
                DamageClass = "special",
                Power = 40,
                Acc = 100,
                Pp = 35,
                Description = "none",
                Priority = 0
            },
            new Move
            {
                MoveId = 6,
                Name = "confusion",
                Type = "psychic",
                DamageClass = "special",
                Power = 50,
                Acc = 100,
                Pp = 25,
                Description = "none",
                Priority = 0
            }
        ];

        testMoveRepo.Setup(Repository => Repository.MakeMovesTable()).ReturnsAsync(testMove);

        Mock<HttpClient> http = new  Mock<HttpClient>();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "test").Options;
        var context = new Mock<ApplicationDbContext>(options);
        var moveDbSet = new Mock<DbSet<Move>>();
        // moveDbSet.Setup(m => m.FindAsync("tackle")).Returns(testMove);

        moveDbSet.As<IQueryable<Move>>().Setup(m => m.Provider).Returns(testMove.AsQueryable().Provider);
        moveDbSet.As<IQueryable<Move>>().Setup(m => m.Expression).Returns(testMove.AsQueryable().Expression);
        moveDbSet.As<IQueryable<Move>>().Setup(m => m.ElementType).Returns(testMove.AsQueryable().ElementType);
        moveDbSet.As<IQueryable<Move>>().Setup(m => m.GetEnumerator()).Returns(testMove.GetEnumerator());



        context.Setup(c => c.Move).Returns(moveDbSet.Object);
        Mock<IPokemonService> pokeService = new Mock<IPokemonService>();

        MoveService moveService = new MoveService(http.Object,testMoveRepo.Object,context.Object,pokeService.Object);

        Move foundMove = await moveService.GetMoveByName("tackle");

        Assert.Equal("tackle", foundMove.Name);
    }

    [Fact]
    public async void MoveServiceShouldGetRandomMoveSet()
    {
        Mock<IMoveRepository> testMoveRepo = new Mock<IMoveRepository>();

        Mock<HttpClient> http = new  Mock<HttpClient>();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "test").Options;

        var context = new Mock<ApplicationDbContext>(options);
        Mock<IPokemonService> pokeService = new Mock<IPokemonService>();

        MoveService moveService = new MoveService(http.Object,testMoveRepo.Object,context.Object,pokeService.Object);
        
        // create a testPokemon to pass to GetRandomMoveSet method using the pokemon service
        Pokemon testPoke = new Pokemon
        {
            Id = 1,
            Name = "bulbasaur",
            Types = ["grass","poison"],
            Hp = 45,
            Atk = 49,
            Satk = 65,
            Def = 49,
            Sdef = 65,
            Spd = 45,
            MovePool = ["cut","bind","vine-whip", "scratch"],
            Sprite = "1.png"
        }; 

        string randomMoveSet =  moveService.GetRandomMoveSet(testPoke);

        Assert.Equal(4, randomMoveSet.Split(",").Length);
}