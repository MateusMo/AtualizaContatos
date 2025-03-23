using ContactZone.Domain.Domains;

namespace ContactZone.Application.Repositories
{
    public interface IContactRepository : IGenericRepository<ContactDomain>
    {
        public Task<IEnumerable<ContactDomain>> GetContactWithAllInformation();
        public Task<IEnumerable<ContactDomain>> GetContactFilteringByDDD(int ddd);
    }
}
