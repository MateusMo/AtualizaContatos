using ContactZone.Domain.Domains;
using System.Linq.Expressions;

namespace ContactZone.Application.Services
{
    public interface IContactService
    {
        Task<ContactDomain> GetByIdAsync(int id);
        Task<IEnumerable<ContactDomain>> GetAllAsync();
        Task<IEnumerable<ContactDomain>> FindAsync(Expression<Func<ContactDomain, bool>> predicate);
        Task AddAsync(ContactDomain entity);
        Task AddRangeAsync(IEnumerable<ContactDomain> entities);
        Task RemoveAsync(ContactDomain entity);
        Task RemoveRangeAsync(IEnumerable<ContactDomain> entities);
        void Update(ContactDomain entity);
        public Task<IEnumerable<ContactDomain>> GetContactWithAllInformation();
        public Task<IEnumerable<ContactDomain>> GetContactFilteringByDDD(int ddd);
    }
}
