using ContactZone.Domain.Domains;
using ContactZone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ContactZone.Application.Repositories;

namespace ContactZone.Infrastructure.Repositories
{
    public class ContactRepository : GenericRepository<ContactDomain>, IContactRepository
    {
        private readonly AtualizaContatosDbContext _context;
        public ContactRepository(AtualizaContatosDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactDomain>> GetContactWithAllInformation()
        { 
            return await _context.Contatos
                .OrderBy(contact => contact.DDD)
                .ThenBy(contact => contact.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<ContactDomain>> GetContactFilteringByDDD(int ddd)
        {
            return await _context.Contatos
                .Where(x => x.DDD == ddd.ToString())
                .ToListAsync();
        }
    }
}
