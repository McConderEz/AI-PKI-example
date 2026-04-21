using System.Reflection;
using CertificationAuthority.Web.Infrastructure.Messaging;
using CertificationAuthority.Web.Infrastructure.Persistence;
using CertificationAuthority.Web.Infrastructure.Repositories;
using CertificationAuthority.Web.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, _, loggerConfiguration) =>
{
    var seqUrl = context.Configuration["Seq:Url"] ?? "http://localhost:5341";
    loggerConfiguration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.Seq(seqUrl);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    }
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? "Data Source=certification-authority.db";
builder.Services.AddDbContext<CaDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICertificateRepository, CertificateRepository>();
builder.Services.AddScoped<ICertificateService, CertificateService>();

builder.Services.AddMassTransit(configurator =>
{
    configurator.SetKebabCaseEndpointNameFormatter();
    configurator.AddConsumer<CreateCertificateCommandConsumer>();

    configurator.UsingRabbitMq((context, cfg) =>
    {
        var host = builder.Configuration["RabbitMq:Host"] ?? "localhost";
        var username = builder.Configuration["RabbitMq:Username"] ?? "guest";
        var password = builder.Configuration["RabbitMq:Password"] ?? "guest";

        cfg.Host(host, "/", hostConfigurator =>
        {
            hostConfigurator.Username(username);
            hostConfigurator.Password(password);
        });

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CaDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseSerilogRequestLogging();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
