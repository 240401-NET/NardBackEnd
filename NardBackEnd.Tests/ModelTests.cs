using Data;
using Microsoft.EntityFrameworkCore;
using Models;
using Moq;
using Repository;
using Service;

namespace NardBackEnd.Tests;

public class ModelTests
{
    //create a test for each model: Pokemon, Move, Battle, Leaderboard, Type
    [Fact]
    public void PokemonShouldHaveProperties()
    {
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
            MovePool = ["cut","bind","vine-whip" ],
            Sprite = "1.png"
        };

        Assert.Equal(1, testPoke.Id);
        Assert.Equal("bulbasaur", testPoke.Name);
        Assert.Equal(["grass","poison"], testPoke.Types);
        Assert.Equal(45, testPoke.Hp);
        Assert.Equal(49, testPoke.Atk);
        Assert.Equal(65, testPoke.Satk);
        Assert.Equal(49, testPoke.Def);
        Assert.Equal(65, testPoke.Sdef);
        Assert.Equal(45, testPoke.Spd);
        Assert.Equal(["cut","bind","vine-whip" ], testPoke.MovePool);
        Assert.Equal("1.png", testPoke.Sprite);
    }

    [Fact]
    public void MoveShouldHaveProperties()
    {
        Move testMove = new Move
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
        };

        Assert.Equal(1, testMove.MoveId);
        Assert.Equal("tackle", testMove.Name);
        Assert.Equal("normal", testMove.Type);
        Assert.Equal("physical", testMove.DamageClass);
        Assert.Equal(40, testMove.Power);
        Assert.Equal(100, testMove.Acc);
        Assert.Equal(35, testMove.Pp);
        Assert.Equal("none", testMove.Description);
        Assert.Equal(0, testMove.Priority);
    }

    [Fact]
    public void BattleShouldHaveProperties()
    {
        Battle testBattle = new Battle
        {
            BattleId = 1,
            BattleStatus = Battle.Status.InProgress,
            BattleWinner = Battle.Winner.NotFinished,
            PokemonId1 = 1,
            P1StatBlock = new List<string> { "hp:45", "atk:49", "satk:65", "def:49", "sdef:65", "spd:45" },
            P1Moves = new List<string> { "cut", "bind", "vine-whip", "tackle" },
            PokemonId2 = 2,
            P2StatBlock = new List<string> { "hp:45", "atk:49", "satk:65", "def:49", "sdef:65", "spd:45" },
            P2Moves = new List<string> { "cut", "bind", "vine-whip", "tackle" },
            battlePhase = Battle.BattlePhase.Selection
        };

        Assert.Equal(1, testBattle.BattleId);
        Assert.Equal(Battle.Status.InProgress, testBattle.BattleStatus);
        Assert.Equal(Battle.Winner.NotFinished, testBattle.BattleWinner);
        Assert.Equal(1, testBattle.PokemonId1);
        Assert.Equal(new List<string> { "hp:45", "atk:49", "satk:65", "def:49", "sdef:65", "spd:45" }, testBattle.P1StatBlock);
        Assert.Equal(new List<string> { "cut", "bind", "vine-whip", "tackle" }, testBattle.P1Moves);
        Assert.Equal(2, testBattle.PokemonId2);
        Assert.Equal(new List<string> { "hp:45", "atk:49", "satk:65", "def:49", "sdef:65", "spd:45" }, testBattle.P2StatBlock);
        Assert.Equal(new List<string> { "cut", "bind", "vine-whip", "tackle" }, testBattle.P2Moves);
        Assert.Equal(Battle.BattlePhase.Selection, testBattle.battlePhase);
    }

    [Fact]
    public void LeaderboardShouldHaveProperties()
    {
        Leaderboard testLeaderboard = new Leaderboard
        {
            Id = 1,
            PokemonId = 1,
            Win = 1,
            Loss = 1,
            Rank = 1
        };

        Assert.Equal(1, testLeaderboard.Id);
        Assert.Equal(1, testLeaderboard.PokemonId);
        Assert.Equal(1, testLeaderboard.Win);
        Assert.Equal(1, testLeaderboard.Loss);
        Assert.Equal(1, testLeaderboard.Rank);
    }

    [Fact]
    public void TypeShouldHaveProperties()
    {
        Models.Type testType = new Models.Type
        {
            //type has an id, a name, and a list of floats corresponding to each other type
            Id = 1,
            Name = "normal",
            Fire = 1,
            Water = 1,
            Grass = 1,
            Electric = 1,
            Psychic = 1,
            Dark = 1,
            Fighting = 1,
            Flying = 1,
            Ground = 1,
            Rock = 1,
            Steel = 1,
            Ice = 1,
            Bug = 1,
            Poison = 1,
            Ghost = 1,
            Dragon = 1,
            Fairy = 1,
            Unknown = 1,
            Shadow = 1
        };

        Assert.Equal(1, testType.Id);
        Assert.Equal("normal", testType.Name);
        Assert.Equal(1, testType.Fire);
        Assert.Equal(1, testType.Water);
        Assert.Equal(1, testType.Grass);
        Assert.Equal(1, testType.Electric);
        Assert.Equal(1, testType.Psychic);
        Assert.Equal(1, testType.Dark);
        Assert.Equal(1, testType.Fighting);
        Assert.Equal(1, testType.Flying);
        Assert.Equal(1, testType.Ground);
        Assert.Equal(1, testType.Rock);
        Assert.Equal(1, testType.Steel);
        Assert.Equal(1, testType.Ice);
        Assert.Equal(1, testType.Bug);
        Assert.Equal(1, testType.Poison);
        Assert.Equal(1, testType.Ghost);
        Assert.Equal(1, testType.Dragon);
        Assert.Equal(1, testType.Fairy);
        Assert.Equal(1, testType.Unknown);
        Assert.Equal(1, testType.Shadow);
    }

}