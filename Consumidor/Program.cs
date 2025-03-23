using Consumer;
using Consumidor;
using ContactZone.Application.Repositories;
using ContactZone.Application.Services;
using ContactZone.Infrastructure.Data;
using ContactZone.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);

// Registrar o DbContext
builder.Services.AddDbContext<AtualizaContatosDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ContactZone"),
        b => b.MigrationsAssembly("ContactZone.Infrastructure"))
    , ServiceLifetime.Scoped);

// Registrar os repositórios e serviços
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();

// Registrar o consumidor no contêiner de DI
builder.Services.AddScoped<UpdateContactConsumer>();

// Configurar o MassTransit com RabbitMQ
builder.Services.AddMassTransit(config =>
{
    var rabbitMqHost = builder.Configuration["RABBITMQ_HOST"];
    // Adicionar o consumidor
    config.AddConsumer<UpdateContactConsumer>();

    config.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitMqHost);

        // Configurar o endpoint para a fila "PutQueue"
        cfg.ReceiveEndpoint("PutQueue", e =>
        {
            e.ConfigureConsumer<UpdateContactConsumer>(context);
        });
    });
});

// Registrar o Worker
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();