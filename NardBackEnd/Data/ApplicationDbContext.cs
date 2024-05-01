using Microsoft.EntityFrameworkCore;
using Models;

namespace Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Pokemon> Pokemon { get; set; }
    public virtual DbSet<Move> Move { get; set; }
    public virtual DbSet<Battle> Battles { get; set; }
    public virtual DbSet<Leaderboard> Leaderboards {get; set;}
    public virtual DbSet<Models.Type> Types {get; set;}

}
