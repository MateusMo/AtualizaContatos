using ContactZone.Application.Services;
using ContactZone.Domain.Domains;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Consumer
{
    public class UpdateContactConsumer : IConsumer<ContactDomain>
    {
        private readonly IContactService _contactService;
        private readonly ILogger<UpdateContactConsumer> _logger;

        public UpdateContactConsumer(
            IContactService contactService,
            ILogger<UpdateContactConsumer> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<ContactDomain> context)
        {
            var contact = context.Message;

            var contactModel = await _contactService.GetByIdAsync(contact.Id);
            
            contactModel.DDD = contact.DDD;
            contactModel.Phone = contact.Phone;
            contactModel.Email = contact.Email;
            contactModel.Name = contact.Name;

            _contactService.Update(contactModel);

            _logger.LogInformation("Contato atualizado: {ContactId}", contact.Id);
        }
    }
}