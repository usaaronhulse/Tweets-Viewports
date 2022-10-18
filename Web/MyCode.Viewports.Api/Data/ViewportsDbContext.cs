using Microsoft.EntityFrameworkCore;
using MyCode.Viewports.Data.Data.DashboardObjects;

namespace MyCode.Viewports.Api.Data
{
    /// <summary>
    /// Creates a <see cref="DbContext"/> which communicates with our SQL Server database.
    /// </summary>
    public class ViewportsDbContext : DbContext
    {
        /// <summary>
        /// An database set of <see cref="Author"/>.
        /// </summary>
        public DbSet<Author> AuthorEntities { get; set; }

        /// <summary>
        /// An database set of <see cref="Dashboard"/>.
        /// </summary>
        public DbSet<Dashboard> DashboardEntities { get; set; }

        /// <summary>
        /// An database set of <see cref="DashboardTweet"/>.
        /// </summary>
        public DbSet<DashboardTweet> DashboardTweetEntities { get; set; }

        /// <summary>
        /// An database set of <see cref="Tweet"/>.
        /// </summary>
        public DbSet<Tweet> TweetEntities { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="ViewportsDbContext"/>.
        /// </summary>
        /// <param name="options">The options required for the <see cref="DbContext"/>.</param>
        public ViewportsDbContext(DbContextOptions<ViewportsDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// When the model is being created.
        /// </summary>
        /// <param name="builder">An instance of <see cref="ModelBuilder"/>.</param>
        protected override void OnModelCreating(ModelBuilder builder)
        {           
            Author.OnModelCreating(builder.Entity<Author>()); // Sets column types for author.
            Dashboard.OnModelCreating(builder.Entity<Dashboard>());// Sets column types for dashboard.
            DashboardTweet.OnModelCreating(builder.Entity<DashboardTweet>()); // Sets column types for dashboardtweet.
            Tweet.OnModelCreating(builder.Entity<Tweet>()); // Sets column types for tweet.

            base.OnModelCreating(builder);
        }
    }
}
