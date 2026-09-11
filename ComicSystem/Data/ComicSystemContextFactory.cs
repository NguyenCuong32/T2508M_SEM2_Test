using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ComicSystem.Data
{
    public class ComicSystemContextFactory : IDesignTimeDbContextFactory<ComicSystemContext>
    {
        public ComicSystemContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ComicSystemContext>();
            var connectionString = "Server=localhost;Port=3306;Database=ComicSystem;User=root;Password=;";
            optionsBuilder.UseMySql(connectionString, ServerVersion.Create(8, 0, 0, Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MySql));
            return new ComicSystemContext(optionsBuilder.Options);
        }
    }
}
