using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Model
{
    public class SearchContextFactory : IDesignTimeDbContextFactory<SearchContext>
    {
        public SearchContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SearchContext>();
            optionsBuilder.UseSqlServer(@"Server=.;Database=InvertedIndex;Trusted_Connection=True;");
            return new SearchContext(optionsBuilder.Options);
        }
    }
}