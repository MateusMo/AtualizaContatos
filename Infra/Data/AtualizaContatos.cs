using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ContactZone.Infrastructure.Data
{
    public class AtualizaContatos : IDesignTimeDbContextFactory<AtualizaContatosDbContext>
    {
        public AtualizaContatosDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AtualizaContatosDbContext>();
            
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "ContactZone.Api"))
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ContactZone");
            optionsBuilder.UseSqlServer(connectionString);

            return new AtualizaContatosDbContext(optionsBuilder.Options);
        }
    }
}