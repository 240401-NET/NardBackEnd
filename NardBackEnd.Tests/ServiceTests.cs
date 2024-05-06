using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;
using Moq;
using Repository;
using Service;
using System.Text.Json;
using System.Net.Http;
using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace NardBackEnd.Tests;

public class ServiceTests
{

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

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "test")
        .Options;

        // Use a clean instance of the context to run the test
        using (var context = new ApplicationDbContext(options))
        {
            //first, we are going to make sure
            //that the DB is in clean slate
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Pokemon.AddRange(testPoke);
            context.SaveChanges();
        }

        // Use a separate instance of the context to verify test results
        using (var context = new ApplicationDbContext(options))
        {
            Mock<HttpClient> http = new  Mock<HttpClient>();

            PokemonService pokeService = new PokemonService(http.Object, context, testPokeRepo.Object);

            Pokemon foundPoke = await pokeService.SearchPokemon("bulbasaur");

            Assert.Equal("bulbasaur", foundPoke.Name);
        }
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

        
        var pokeDbSetMock = new Mock<DbSet<Pokemon>>();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "test").Options;
        // Use a clean instance of the context to run the test
        using (var context = new ApplicationDbContext(options))
        {  
            //first, we are going to make sure
            //that the DB is in clean slate
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Pokemon.AddRange(testPoke);
            context.SaveChanges();
        }

        // Use a separate instance of the context to verify test results
        using (var context = new ApplicationDbContext(options))
        {
            Mock<HttpClient> http = new  Mock<HttpClient>();

            PokemonService pokeService = new PokemonService(http.Object, context, testPokeRepo.Object);

            Pokemon poke = await pokeService.GetPokemon(1);

            Assert.True(poke != null);
            Assert.Equal("bulbasaur", poke.Name);
        }
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


        
        var pokeDbSetMock = new Mock<DbSet<Pokemon>>();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "test").Options;
        // Use a clean instance of the context to run the test
        using (var context = new ApplicationDbContext(options))
        {      
            //first, we are going to make sure
            //that the DB is in clean slate
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Pokemon.AddRange(testPoke);
            context.SaveChanges();
        }

        // Use a separate instance of the context to verify test results
        using (var context = new ApplicationDbContext(options))
        {
            Mock<HttpClient> http = new  Mock<HttpClient>();

            PokemonService pokeService = new PokemonService(http.Object, context, testPokeRepo.Object);

            IEnumerable<Pokemon> allPoke = await pokeService.GetAllPokemon();

            Assert.True(allPoke != null);
            Assert.Equal("bulbasaur", allPoke.First().Name);
        }
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

        Mock<HttpClient> http = new  Mock<HttpClient>();
        var moveDbSetMock = new Mock<DbSet<Move>>();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "test").Options;
        var context = new Mock<ApplicationDbContext>(options);

        moveDbSetMock.As<IQueryable<Move>>().Setup(m => m.Provider).Returns(testMove.AsQueryable().Provider);
        moveDbSetMock.As<IQueryable<Move>>().Setup(m => m.Expression).Returns(testMove.AsQueryable().Expression);
        moveDbSetMock.As<IQueryable<Move>>().Setup(m => m.ElementType).Returns(testMove.AsQueryable().ElementType);
        moveDbSetMock.As<IQueryable<Move>>().Setup(m => m.GetEnumerator()).Returns(testMove.AsQueryable().GetEnumerator());
        
        Mock<IPokemonService> pokeService = new Mock<IPokemonService>();

        context.Setup(c => c.Move).Returns(moveDbSetMock.Object);

        MoveService moveService = new MoveService(http.Object, testMoveRepo.Object, context.Object, pokeService.Object);
        
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
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "test").Options;
        var context = new Mock<ApplicationDbContext>(options);
        var moveDbSet = new Mock<DbSet<Move>>();
        // moveDbSet.Setup(m => m.FindAsync("tackle")).Returns(testMove);

        moveDbSet.As<IQueryable<Move>>().Setup(m => m.Provider).Returns(testMove.AsQueryable().Provider);
        moveDbSet.As<IQueryable<Move>>().Setup(m => m.Expression).Returns(testMove.AsQueryable().Expression);
        moveDbSet.As<IQueryable<Move>>().Setup(m => m.ElementType).Returns(testMove.AsQueryable().ElementType);
        moveDbSet.As<IQueryable<Move>>().Setup(m => m.GetEnumerator()).Returns(testMove.GetEnumerator());
        moveDbSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>(ids => testMove.FirstOrDefault(d => d.MoveId == (int)ids[0]));



        context.Setup(c => c.Move).Returns(moveDbSet.Object);
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

    //create tests for the battle service including create battle, update battle, delete battle, 
    //calculate priority, calculate hit, calculate damage, normalize pokemon, get battle, get battles

    [Fact]
    public void CreateBattle_ShouldAddBattleToDatabase()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;
        var context = new ApplicationDbContext(options);

        var httpClient = new HttpClient();
        var apiService = new PokeAPIService(httpClient); // Assuming you have a PokeAPIService class
        var pokemonRepo = new PokemonRepository(context, apiService);

        var pokemonService = new PokemonService(httpClient, context, pokemonRepo);

        var service = new BattleService(context, pokemonService, httpClient);

        var battle = new Battle 
                {
                    BattleId = 1,
                    PokemonId1 = 1,
                    P1StatBlock = new List<string> { "hp:0", "atk:0", "def:0", "satk:0", "sdef:0", "spd:0" },
                    PokemonId2 = 2,
                    P2StatBlock = new List<string> { "hp:0", "atk:0", "def:0", "satk:0", "sdef:0", "spd:0" },
                    BattleWinner = Battle.Winner.NotFinished,
                    BattleStatus = Battle.Status.InProgress,
                    battlePhase = Battle.BattlePhase.Selection,
                    P1Moves = new List<string> { "tackle", "scratch", "ember", "water-gun" },
                    P2Moves = new List<string> { "tackle", "scratch", "ember", "water-gun" }
                };

        // Act
        service.CreateBattle(battle);
        var addedBattle = context.Battles.Find(battle.BattleId);

        // Assert
        Assert.Equal(battle, addedBattle);
    }

    [Fact]
    public async Task UpdateBattle_ShouldUpdateBattleAndReturnRoundResult()
    {
    // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;
        var context = new ApplicationDbContext(options);

        var httpClient = new HttpClient();
        var apiService = new PokeAPIService(httpClient); // Assuming you have a PokeAPIService class
        var pokemonRepo = new PokemonRepository(context, apiService);
        var pokemonService = new PokemonService(httpClient, context, pokemonRepo); // Assuming you have a PokemonService class
        var service = new BattleService(context, pokemonService, httpClient);


        var battle = new Battle 
                    {
                        BattleId = 1,
                        PokemonId1 = 1,
                        P1StatBlock = new List<string> { "hp:0", "atk:0", "def:0", "satk:0", "sdef:0", "spd:0" },
                        PokemonId2 = 2,
                        P2StatBlock = new List<string> { "hp:0", "atk:0", "def:0", "satk:0", "sdef:0", "spd:0" },
                        BattleWinner = Battle.Winner.NotFinished,
                        BattleStatus = Battle.Status.InProgress,
                        battlePhase = Battle.BattlePhase.Selection,
                        P1Moves = new List<string> { "tackle", "scratch", "ember", "water-gun" },
                        P2Moves = new List<string> { "tackle", "scratch", "ember", "water-gun" }
                    };
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.Battles.Add(battle);
        context.SaveChanges();

        int firstToMove = 1;
        bool move1Hit = true;
        bool move2Hit = false;
        Task<string> damageResult = Task.FromResult("10");

        // Act
        string result = service.UpdateBattle(battle, firstToMove, move1Hit, move2Hit, damageResult);

        // Assert
        Assert.Equal("10", result);

        var updatedBattle = context.Battles.Find(battle.BattleId);
        Assert.Equal(new List<string> { "hp:0", "atk:0", "def:0", "satk:0", "sdef:0", "spd:0" }, updatedBattle.P1StatBlock);
        Assert.Equal(new List<string> { "hp:0", "atk:0", "def:0", "satk:0", "sdef:0", "spd:0" }, updatedBattle.P2StatBlock);
    }

    [Fact]
    public void DeleteBattle_ShouldDeleteBattle()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;
        var context = new ApplicationDbContext(options);

        var httpClient = new HttpClient();
        var apiService = new PokeAPIService(httpClient); // Assuming you have a PokeAPIService class
        var pokemonRepo = new PokemonRepository(context, apiService);
        var pokemonService = new PokemonService(httpClient, context, pokemonRepo); // Assuming you have a PokemonService class
        var service = new BattleService(context, pokemonService, httpClient);

        var battle = new Battle 
            {
                BattleId = 1,
                PokemonId1 = 1,
                P1StatBlock = new List<string> { "hp:0", "atk:0", "def:0", "satk:0", "sdef:0", "spd:0" },
                PokemonId2 = 2,
                P2StatBlock = new List<string> { "hp:0", "atk:0", "def:0", "satk:0", "sdef:0", "spd:0" },
                BattleWinner = Battle.Winner.NotFinished,
                BattleStatus = Battle.Status.InProgress,
                battlePhase = Battle.BattlePhase.Selection,
                P1Moves = new List<string> { "tackle", "scratch", "ember", "water-gun" },
                P2Moves = new List<string> { "tackle", "scratch", "ember", "water-gun" }
            };
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.Battles.Add(battle);
        context.SaveChanges();

        // Act
        service.DeleteBattle(battle.BattleId);

        // Assert
        var deletedBattle = context.Battles.Find(battle.BattleId);
        Assert.Null(deletedBattle);
    }

    [Fact]
    public void CalculatePriority_ShouldReturnCorrectPriority()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;
        var context = new ApplicationDbContext(options);

        var httpClient = new HttpClient();
        var apiService = new PokeAPIService(httpClient); // Assuming you have a PokeAPIService class
        var pokemonRepo = new PokemonRepository(context, apiService);
        var pokemonService = new PokemonService(httpClient, context, pokemonRepo); // Assuming you have a PokemonService class
        var service = new BattleService(context, pokemonService, httpClient);

        var move1 = new Move { MoveId = 1, Name = "move1", Priority = 1, Power = 40, Acc = 100, Pp = 35, Type = "normal", DamageClass = "physical"};
        var move2 = new Move { MoveId = 2, Name = "move2", Priority = 2, Power = 40, Acc = 100, Pp = 35, Type = "normal", DamageClass = "physical"};
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.Move.Add(move1);
        context.Move.Add(move2);
        context.SaveChanges();

        var battle = new Battle 
            {
                BattleId = 1,
                PokemonId1 = 1,
                P1StatBlock = new List<string> { "hp:0", "atk:0", "def:0", "satk:0", "sdef:0", "spd:0" },
                PokemonId2 = 2,
                P2StatBlock = new List<string> { "hp:0", "atk:0", "def:0", "satk:0", "sdef:0", "spd:0" },
                BattleWinner = Battle.Winner.NotFinished,
                BattleStatus = Battle.Status.InProgress,
                battlePhase = Battle.BattlePhase.Selection,
                P1Moves = new List<string> { "tackle", "scratch", "ember", "water-gun" },
                P2Moves = new List<string> { "tackle", "scratch", "ember", "water-gun" }
            };
    
        context.Battles.Add(battle);
        context.SaveChanges();

        // Act
        int result = service.CalculatePriority(battle, move1.Name, move2.Name);

        // Assert
        Assert.Equal(2, result);
    }

    [Fact]
    public void CalculateHit_ShouldReturnCorrectResult()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;
        var context = new ApplicationDbContext(options);

        var httpClient = new HttpClient();
        var apiService = new PokeAPIService(httpClient); // Assuming you have a PokeAPIService class
        var pokemonRepo = new PokemonRepository(context, apiService); // Assuming you have a PokemonRepository class
        var pokemonService = new PokemonService(httpClient, context, pokemonRepo); // Assuming you have a PokemonService class
        var service = new BattleService(context, pokemonService, httpClient);

        var move1 = new Move { MoveId = 1, Name = "move1", Acc = 100, Power = 40, Type = "normal", DamageClass = "physical"};
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.Move.Add(move1);
        context.SaveChanges();

        var battle = new Battle 
        {
            BattleId = 1,
            PokemonId1 = 1,
            P1StatBlock = new List<string> { "hp:0", "atk:0", "def:0", "satk:0", "sdef:0", "spd:0" },
            PokemonId2 = 2,
            P2StatBlock = new List<string> { "hp:0", "atk:0", "def:0", "satk:0", "sdef:0", "spd:0" },
            BattleWinner = Battle.Winner.NotFinished,
            BattleStatus = Battle.Status.InProgress,
            battlePhase = Battle.BattlePhase.Selection,
            P1Moves = new List<string> { "tackle", "scratch", "ember", "water-gun" },
            P2Moves = new List<string> { "tackle", "scratch", "ember", "water-gun" }
        };
        context.Battles.Add(battle);
        context.SaveChanges();

        // Act
        bool result = service.CalculateHit(battle, move1.Name);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task CalculateDamageTest()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;
        var context = new ApplicationDbContext(options);

        var httpClient = new HttpClient();
        var apiService = new PokeAPIService(httpClient); // Assuming you have a PokeAPIService class
        var pokemonRepo = new PokemonRepository(context, apiService); // Assuming you have a PokemonRepository class
        var pokemonService = new PokemonService(httpClient, context, pokemonRepo); // Assuming you have a PokemonService class
        BattleService service = new BattleService(context, pokemonService, httpClient);

        //mock normal, grass and poison types in context.Types
        var normal = new Models.Type { Id = 1, Name = "normal", Fire = 1, Water = 1, Electric = 1, Grass = 1, Ice = 1, Fighting = 1, Poison = 1, Ground = 1, Flying = 1, Psychic = 1, Bug = 1, Rock = 0.5f, Ghost = 0, Dragon = 1, Dark = 1, Steel = 0.5f, Fairy = 1, Unknown = 1, Shadow = 1 };
        var grass = new Models.Type { Id = 2, Name = "grass", Fire = 2, Water = 0.5f, Electric = 0.5f, Grass = 0.5f, Ice = 2, Fighting = 1, Poison = 2, Ground = 0.5f, Flying = 2, Psychic = 1, Bug = 2, Rock = 1, Ghost = 1, Dragon = 1, Dark = 1, Steel = 1, Fairy = 1, Unknown = 1, Shadow = 1 };
        var poison = new Models.Type { Id = 3, Name = "poison", Fire = 1, Water = 1, Electric = 1, Grass = 2, Ice = 1, Fighting = 0.5f, Poison = 0.5f, Ground = 2, Flying = 1, Psychic = 1, Bug = 1, Rock = 0.5f, Ghost = 0.5f, Dragon = 1, Dark = 1, Steel = 0, Fairy = 2, Unknown = 1, Shadow = 1 };

        //create two moves in the database
        var move1 = new Move { MoveId = 1, Name = "tackle", Power = 40, Acc = 100, Type = "normal", DamageClass = "physical"};
        var move2 = new Move { MoveId = 2, Name = "growl", Power = 0, Acc = 100, Type = "normal", DamageClass = "status"};

        //create two pokemon in the database
        var pokemon1 = new Pokemon { Id = 1, Name = "bulbasaur", Types = new List<string> { "grass", "poison" }, Hp = 45, Atk = 49, Satk = 65, Def = 49, Sdef = 65, Spd = 45, MovePool = new List<string> { "tackle", "scratch", "ember", "water-gun" }, Sprite = "1.png" };
        var pokemon2 = new Pokemon { Id = 3, Name = "venusaur", Types = new List<string> { "grass", "poison" }, Hp = 80, Atk = 82, Satk = 100, Def = 83, Sdef = 100, Spd = 80, MovePool = new List<string> { "tackle", "scratch", "ember", "water-gun" }, Sprite = "3.png" };

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.Add(normal);
        context.Add(grass);
        context.Add(poison);
        context.Move.Add(move1);
        context.Move.Add(move2);
        context.Pokemon.Add(pokemon1);
        context.Pokemon.Add(pokemon2);
        context.SaveChanges();
        
        var battle = new Battle 
            {
                BattleId = 1,
                PokemonId1 = 1,
                P1StatBlock = new List<string> { "hp:0", "atk:0", "def:0", "satk:0", "sdef:0", "spd:0" },
                PokemonId2 = 3,
                P2StatBlock = new List<string> { "hp:0", "atk:0", "def:0", "satk:0", "sdef:0", "spd:0" },
                BattleWinner = Battle.Winner.NotFinished,
                BattleStatus = Battle.Status.InProgress,
                battlePhase = Battle.BattlePhase.Selection,
                P1Moves = new List<string> { "tackle", "scratch", "ember", "water-gun" },
                P2Moves = new List<string> { "tackle", "scratch", "ember", "growl" }
            };
        string pokemon1Move = "tackle"; // Changed to lowercase
        string pokemon2Move = "growl"; // Changed to lowercase

        // normalize the pokemon stats
        var normalizedBattle = service.NormalizePokemon(battle);

        // Act
        string result = await service.CalculateDamage(normalizedBattle, pokemon1Move, pokemon2Move);

        // Assert
        // Assert.NotNull();
        Assert.NotNull(result);
    }

    [Fact]
    public void BattleServiceShouldNormalizePokemon()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "test").Options;
        var context = new Mock<ApplicationDbContext>(options);
        context.Setup(c => c.Pokemon.Find(1)).Returns(new Pokemon { Id = 1, Name = "bulbasaur", Types = new List<string> { "grass", "poison" }, Hp = 45, Atk = 49, Satk = 65, Def = 49, Sdef = 65, Spd = 45, MovePool = new List<string> { "cut", "bind", "vine-whip", "scratch" }, Sprite = "1.png" });
        context.Setup(c => c.Pokemon.Find(2)).Returns(new Pokemon { Id = 2, Name = "venusaur", Types = new List<string> { "grass", "poison" }, Hp = 80, Atk = 82, Satk = 100, Def = 83, Sdef = 100, Spd = 80, MovePool = new List<string> { "cut", "bind", "vine-whip", "scratch" }, Sprite = "3.png" });

        var pokemonService = new Mock<IPokemonService>();
        var httpClient = new HttpClient();

        var service = new BattleService(context.Object, pokemonService.Object, httpClient);
        var battle = new Battle
        {
            BattleId = 1,
            PokemonId1 = 1,
            P1StatBlock = new List<string> { "hp:0", "atk:0", "def:0", "satk:0", "sdef:0", "spd:0" },
            PokemonId2 = 2,
            P2StatBlock = new List<string> { "hp:0", "atk:0", "def:0", "satk:0", "sdef:0", "spd:0" },
            BattleWinner = Battle.Winner.NotFinished,
            BattleStatus = Battle.Status.InProgress,
            battlePhase = Battle.BattlePhase.Selection,
            P1Moves = new List<string> { "tackle", "scratch", "ember", "water-gun" },
            P2Moves = new List<string> { "tackle", "scratch", "ember", "water-gun" }
        };

        // Act
        var normalizedBattle = service.NormalizePokemon(battle);

        // Assert
        Assert.NotEqual("hp:0", normalizedBattle.P1StatBlock[0]);
        Assert.NotEqual("hp:0", normalizedBattle.P2StatBlock[0]);
    }

    [Fact]
    public void GetBattle_ReturnsCorrectBattle()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "test").Options;
        var mockContext = new Mock<ApplicationDbContext>(options);
        var mockPokemonService = new Mock<IPokemonService>();
        var mockHttpClient = new Mock<HttpClient>();
        var mockDbSet = new Mock<DbSet<Battle>>();
        var battleId = 1;
        var expectedBattle = new Battle { BattleId = battleId };

        mockDbSet.Setup(m => m.Find(battleId)).Returns(expectedBattle);
        mockContext.Setup(m => m.Battles).Returns(mockDbSet.Object);

        var service = new BattleService(mockContext.Object, mockPokemonService.Object, mockHttpClient.Object);

        // Act
        var result = service.GetBattle(battleId);

        // Assert
        Assert.Equal(expectedBattle, result);
    }

    [Fact]
    public async void BattleServiceShouldGetBattles()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "test").Options;
        var context = new ApplicationDbContext(options);
        var httpClient = new HttpClient();
        var PokemonService = new PokemonService(httpClient, context, new PokemonRepository(context, new PokeAPIService(httpClient)));
        var battleId = 1;
        Battle expectedBattle = new Battle { BattleId = battleId };

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.Battles.Add(expectedBattle);

        var service = new BattleService(context, PokemonService, httpClient);

        // Act
        var result = service.GetBattles();

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void GetTypeMultiplier_ShouldReturnCorrectMultiplier()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "test").Options;
        var mockContext = new ApplicationDbContext(options);
        // var mockDbSet = new Mock<DbSet<Models.Type>>();
        var move = new Move { MoveId = 1, Name = "ember", Power = 40, Acc = 100, Pp = 12, Description = "it's a move", Priority = 1, Type = "fire", DamageClass = "special" };
        var pokemon = new Pokemon { Id = 1, Name = "bulbasaur", Types = new List<string> { "grass", "bug" }, Hp = 45, Atk = 49, Satk = 65, Def = 49, Sdef = 65, Spd = 45, MovePool = new List<string> { "cut", "bind", "vine-whip", "scratch" }, Sprite = "1.png" };
        var type1 = new Models.Type { Id = 1, Name = "grass", Fire = 2, Water = 0.5f, Electric = 0.5f, Grass = 0.5f, Ice = 2, Fighting = 1, Poison = 2, Ground = 0.5f, Flying = 2, Psychic = 1, Bug = 2, Rock = 1, Ghost = 1, Dragon = 1, Dark = 1, Steel = 1, Fairy = 1, Unknown = 1, Shadow = 1 };
        var type2 = new Models.Type { Id = 2, Name = "bug", Fire = 2, Water = 1, Electric = 1, Grass = 2, Ice = 1, Fighting = 0.5f, Poison = 0.5f, Ground = 2, Flying = 1, Psychic = 1, Bug = 1, Rock = 0.5f, Ghost = 0.5f, Dragon = 1, Dark = 1, Steel = 0, Fairy = 2, Unknown = 1, Shadow = 1 };
        var type3 = new Models.Type { Id = 3, Name = "fire", Fire = 1, Water = 0.5f, Electric = 1, Grass = 2, Ice = 2, Fighting = 1, Poison = 1, Ground = 1, Flying = 1, Psychic = 1, Bug = 2, Rock = 0.5f, Ghost = 1, Dragon = 1, Dark = 1, Steel = 2, Fairy = 1, Unknown = 1, Shadow = 1 };


        mockContext.Database.EnsureDeleted();
        mockContext.Database.EnsureCreated();
        mockContext.Move.Add(move);
        mockContext.Pokemon.Add(pokemon);
        mockContext.Types.Add(type1);
        mockContext.Types.Add(type2);
        mockContext.Types.Add(type3);
        mockContext.SaveChanges();

        var service = new BattleService(mockContext,new PokemonService(new HttpClient(), mockContext, new PokemonRepository(mockContext, new PokeAPIService(new HttpClient()))), new HttpClient());

        // Act
        var result = service.GetTypeMultiplier(move, pokemon);

        // Assert
        Assert.Equal(4, result);
    }
// create tests for MakeTypeDbTable
    [Fact]
    public async Task MakeTypeDBTable_ShouldReturnCorrectList()
    {
        // Arrange
        var mockTypeRepository = new Mock<ITypeRepository>();
        var expectedList = new List<Models.Type> { new Models.Type { Id = 1, Name = "Fire" }, new Models.Type { Id = 2, Name = "Water" } };
        mockTypeRepository.Setup(repo => repo.MakeTypeDBTable()).ReturnsAsync(expectedList);

        var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "test").Options;
        var context = new ApplicationDbContext(options);
        var httpClient = new HttpClient();

        var service = new TypeService(mockTypeRepository.Object, context, httpClient);

        // Act
        var result = await service.MakeTypeDBTable();

        // Assert
        Assert.Equal(expectedList, result);
    }

    // create testsfor CreateLeaderboard, UpdateLeaderboard, DeleteLeaderboard, GetLeaderboard, GetLeaderboards
    [Fact]
    public void CreateLeaderboard_ShouldAddNewLeaderboard()
    {
        // Arrange
        var mockSet = new Mock<DbSet<Leaderboard>>();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;
        var mockContext = new ApplicationDbContext(options);
        mockSet.Setup(m => m.Add(It.IsAny<Leaderboard>())).Callback<Leaderboard>(arg => mockContext.Leaderboards.Add(arg));

        var httpClient = new HttpClient();

        var service = new LeaderboardService(httpClient, mockContext);

        var newLeaderboard = new Leaderboard { PokemonId = 3, PokemonName = "Test Pokemon" };

        // Act
        service.CreateLeaderboard(newLeaderboard);

        // Assert
        Assert.Contains(newLeaderboard, mockContext.Leaderboards);
    }
    [Fact]
    public void UpdateLeaderboard_ShouldUpdateLeaderboardCorrectly()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;
        var context = new ApplicationDbContext(options);

        var httpClient = new HttpClient();

        var service = new LeaderboardService(httpClient, context);

        var leaderboard = new Leaderboard { Id = 1, PokemonId = 3, PokemonName = "Test Pokemon", Win = 5, Loss = 2 };
        context.Leaderboards.Add(leaderboard);
        context.SaveChanges();

        // Act
        service.UpdateLeaderboard(leaderboard, 3); // Win should be updated to 8
        var updatedLeaderboard = context.Leaderboards.Find(leaderboard.Id);

        // Assert
        Assert.Equal(8, updatedLeaderboard.Win);

        // Act
        service.UpdateLeaderboard(leaderboard, -2); // Loss should be updated to 4
        updatedLeaderboard = context.Leaderboards.Find(leaderboard.Id);

        // Assert
        Assert.Equal(4, updatedLeaderboard.Loss);

        // Act
        var newLeaderboard = new Leaderboard { Id = 2, PokemonId = 4, PokemonName = "New Pokemon" };
        service.UpdateLeaderboard(newLeaderboard, 2); // New leaderboard should be added with Win = 2
        var addedLeaderboard = context.Leaderboards.Find(newLeaderboard.Id);

        // Assert
        Assert.Equal(2, addedLeaderboard.Win);
    }

    [Fact]
    public void DeleteLeaderboard_ShouldDeleteLeaderboardCorrectly()
{
    // Arrange
    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "test")
        .Options;
    var context = new ApplicationDbContext(options);

    var httpClient = new HttpClient();

    var service = new LeaderboardService(httpClient, context);

    var leaderboard = new Leaderboard { Id = 1, PokemonId = 3, PokemonName = "Test Pokemon", Win = 5, Loss = 2 };
    
    //first, we are going to make sure
    //that the DB is in clean slate
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();
    context.Leaderboards.Add(leaderboard);
    context.SaveChanges();

    // Act
    service.DeleteLeaderboard(leaderboard.Id);
    var deletedLeaderboard = context.Leaderboards.Find(leaderboard.Id);

    // Assert
    Assert.Null(deletedLeaderboard);
}

    [Fact]
    public void GetLeaderboard_ShouldReturnCorrectLeaderboard()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;
        var context = new ApplicationDbContext(options);

        var httpClient = new HttpClient();

        var service = new LeaderboardService(httpClient, context);

        var leaderboard = new Leaderboard { Id = 1, PokemonId = 3, PokemonName = "Test Pokemon", Win = 5, Loss = 2 };
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.Leaderboards.Add(leaderboard);
        context.SaveChanges();

        // Act
        var retrievedLeaderboard = service.GetLeaderboard(leaderboard.Id);

        // Assert
        Assert.Equal(leaderboard, retrievedLeaderboard);
    }

    [Fact]
    public void GetLeaderboards_ShouldReturnLeaderboardsInCorrectOrder()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "test")
            .Options;
        var context = new ApplicationDbContext(options);

        var httpClient = new HttpClient();

        var service = new LeaderboardService(httpClient, context);

        var leaderboard1 = new Leaderboard { Id = 1, PokemonId = 3, PokemonName = "Test Pokemon 1", Win = 5, Loss = 2, Rank = 2 };
        var leaderboard2 = new Leaderboard { Id = 2, PokemonId = 4, PokemonName = "Test Pokemon 2", Win = 6, Loss = 1, Rank = 1 };
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.Leaderboards.AddRange(new[] { leaderboard1, leaderboard2 });
        context.SaveChanges();

        // Act
        var leaderboards = service.GetLeaderboards();

        // Assert
        Assert.Equal(2, leaderboards.Count);
        Assert.Equal(leaderboard2, leaderboards[0]);
        Assert.Equal(leaderboard1, leaderboards[1]);
    }
}