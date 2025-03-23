using ContactZone.Application.Repositories;
using ContactZone.Domain.Domains;
using System.Linq.Expressions;

namespace ContactZone.Application.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;
        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }
        public async Task AddAsync(ContactDomain entity)
        {
            await _contactRepository.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<ContactDomain> entities)
        {
            await _contactRepository.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<ContactDomain>> FindAsync(Expression<Func<ContactDomain, bool>> predicate)
        {
            return await _contactRepository.FindAsync(predicate);
        }

        public async Task<IEnumerable<ContactDomain>> GetAllAsync()
        {
            return await _contactRepository.GetAllAsync();
        }

        public async Task<ContactDomain> GetByIdAsync(int id)
        {
            return await _contactRepository.GetByIdAsync(id);
        }

        public async Task RemoveAsync(ContactDomain entity)
        {
            await _contactRepository.RemoveAsync(entity);
        }

        public async Task RemoveRangeAsync(IEnumerable<ContactDomain> entities)
        {
            await _contactRepository.RemoveRangeAsync(entities);
        }

        public async void Update(ContactDomain entity)
        {
            _contactRepository.Update(entity);
        }

        public async Task<IEnumerable<ContactDomain>> GetContactWithAllInformation()
        {
            return await _contactRepository.GetContactWithAllInformation();
        }

        public async Task<IEnumerable<ContactDomain>> GetContactFilteringByDDD(int ddd)
        {
            return await _contactRepository.GetContactFilteringByDDD(ddd);
        }
    }
}
