namespace Backend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Rule> Rules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new GameConfiguration().Configure(modelBuilder.Entity<Game>());
            new RuleConfiguration().Configure(modelBuilder.Entity<Rule>());
        }
    }
}
// Add-Migration InitialCreate -StartupProject Backend -OutputDir Data\Migrations -Context ApplicationDbContext
// Update-Database -StartupProject Backend -Context ApplicationDbContext
// Drop-Database -StartupProject Backend -Context ApplicationDbContext