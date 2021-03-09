using Microsoft.EntityFrameworkCore;

namespace Model
{
    public class SearchContext : DbContext
    {
        public DbSet<Word> Words { get; set; }
        public DbSet<Doc> Docs { get; set; }

        public SearchContext(DbContextOptions<SearchContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>()
                .HasMany(w => w.DocsContainer)
                .WithMany(d => d.WordsOfDoc).UsingEntity(j => j.ToTable("DocsWord"));
        }
    }
}