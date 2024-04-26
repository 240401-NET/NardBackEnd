using Microsoft.EntityFrameworkCore;
using Models;

namespace Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Pokemon> Pokemon { get; set; }
    public DbSet<Move> Move { get; set; }
    public DbSet<Battle> Battles { get; set; }
    public DbSet<Leaderboard> Leaderboards {get; set;}
    public DbSet<Track> Tracks {get; set;}
    public DbSet<Models.Type> Types {get; set;}

}
