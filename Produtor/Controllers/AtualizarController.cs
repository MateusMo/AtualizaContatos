using ContactZone.Application.Services;
using ContactZone.Domain.Domains;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Produtor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtualizarController : ControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IContactService _contactService;


        public AtualizarController(ISendEndpointProvider sendEndpointProvider, IContactService contactService)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _contactService = contactService;
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ContactDomain contact)
        {
            if (contact == null)
            {
                return BadRequest("Contato inválido.");
            }

            // Obtém o endpoint da fila "PutQueue"
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:PutQueue"));

            // Envia a mensagem para a fila "PutQueue"
            await endpoint.Send<ContactDomain>(contact);

            return Ok("Contato enviado para atualização.");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _contactService.GetAllAsync());
        }
    }
}