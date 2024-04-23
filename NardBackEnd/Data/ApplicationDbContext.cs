using Microsoft.EntityFrameworkCore;
using Models;
using DTOs;

namespace Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Pokemon> Pokemon { get; set; }
    public DbSet<Move> Moves { get; set; }
    public DbSet<Battle> Battles { get; set; }
    public DbSet<Leaderboard> Leaderboards {get; set;}
    public DbSet<Track> Tracks {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<PokemonTypeContainer>();
    }
}
