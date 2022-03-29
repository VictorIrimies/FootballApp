using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AplicatieFotbal.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Models.League> League { get; set; }
        public DbSet<Models.Team> Team { get; set; }
        public DbSet<Models.Player> Player { get; set; }
        public DbSet<Models.Match> Match { get; set; }
        public DbSet<Models.MatchGoal> MatchGoal { get; set; }
    }
}
