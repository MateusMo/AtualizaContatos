using Consumer;
using ContactZone.Application.Repositories;
using ContactZone.Application.Services;
using ContactZone.Infrastructure.Data;
using ContactZone.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Registrar o DbContext (se necessário para o Consumer)
builder.Services.AddDbContext<AtualizaContatosDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ContactZone"),
        b => b.MigrationsAssembly("ContactZone.Infrastructure"))
);

// Registrar serviços e repositórios (ajuste conforme seus namespaces)
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();

// Configurar o MassTransit com Consumer
builder.Services.AddMassTransit(config =>
{
    var rabbitMqHost = builder.Configuration["RABBITMQ_HOST"];

    // Adicionar o Consumer
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

// Add Swagger (opcional)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();