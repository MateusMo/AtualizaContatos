using ContactZone.Domain.Domains;
using ContactZone.Infrastructure.Data.FluentMap;
using Microsoft.EntityFrameworkCore;

namespace ContactZone.Infrastructure.Data
{
    public class AtualizaContatosDbContext : DbContext
    {
        public DbSet<ContactDomain> Contatos { get; set; }

        public AtualizaContatosDbContext(DbContextOptions<AtualizaContatosDbContext> options)
           : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ContactMap());
        }
    }
}
