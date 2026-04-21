using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using RegistrationAuthority.Web.Infrastructure.Messaging;
using RegistrationAuthority.Web.Infrastructure.Persistence;
using RegistrationAuthority.Web.Infrastructure.Repositories;
using RegistrationAuthority.Web.Services;
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
                       ?? "Data Source=registration-authority.db";
builder.Services.AddDbContext<RaDbContext>(options => options.UseSqlite(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICertRequestRepository, CertRequestRepository>();
builder.Services.AddScoped<ICertificateRepository, CertificateRepository>();
builder.Services.AddScoped<IRevRequestRepository, RevRequestRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICertRequestService, CertRequestService>();
builder.Services.AddScoped<ICertificateService, CertificateService>();
builder.Services.AddScoped<IRevRequestService, RevRequestService>();

builder.Services.AddMassTransit(configurator =>
{
    configurator.SetKebabCaseEndpointNameFormatter();
    configurator.AddConsumer<CertificateIssuedConsumer>();

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
    var dbContext = scope.ServiceProvider.GetRequiredService<RaDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseSerilogRequestLogging();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
