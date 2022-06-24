using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Models;

namespace Server.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Ticker> Tickers {get; set;} = null!;
        public DbSet<UserTicker> UserTickers {get; set;} = null!;

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Ticker>(e => {
                e.ToTable("Ticker");
                e.HasKey(e => e.IdTicker);

                e.Property(e => e.TickerSymbol).HasMaxLength(16).IsRequired(true);
            });

            builder.Entity<UserTicker>(e => {
                e.ToTable("Watchlist");
                e.HasKey(e => new {e.IdTicker, e.IdUser});

                e.HasOne(e => e.Ticker).WithMany(e => e.UsersTickers).HasForeignKey(e => e.IdTicker).OnDelete(DeleteBehavior.NoAction);
                e.HasOne(e => e.User).WithMany(e => e.FollowedTickers).HasForeignKey(e => e.IdUser).OnDelete(DeleteBehavior.ClientCascade);
            });
        }
    }
}