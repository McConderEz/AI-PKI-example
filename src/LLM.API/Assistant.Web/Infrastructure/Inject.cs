using Assistant.Web.Application.Abstractions;
using Assistant.Web.Application.Services;
using Assistant.Web.Infrastructure.Clients;
using Assistant.Web.Infrastructure.Providers;
using Refit;
using Serilog;

namespace Assistant.Web.Infrastructure;

/// <summary>
/// Содержит методы расширения для регистрации инфраструктурных зависимостей.
/// </summary>
public static class Inject
{
    /// <summary>
    /// Регистрирует инфраструктурный слой, клиент LLM и конфигурацию логирования.
    /// </summary>
    /// <param name="builder">Построитель веб-приложения.</param>
    /// <returns>Переданный построитель веб-приложения.</returns>
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, services, loggerConfiguration) =>
        {
            var seqUrl = context.Configuration["Seq:Url"] ?? "http://localhost:5341";
            var seqApiKey = context.Configuration["Seq:ApiKey"];

            loggerConfiguration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Seq(
                    serverUrl: seqUrl,
                    apiKey: string.IsNullOrWhiteSpace(seqApiKey) ? null : seqApiKey);
        });

        builder.Services.AddRefitClient<ILLMClient>()
            .ConfigureHttpClient((serviceProvider, client) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                var baseUrl = configuration["LLM:BaseUrl"] ?? "http://localhost:11434";
                client.BaseAddress = new Uri(baseUrl);
            });

        builder.Services.AddScoped<ILLMProvider, LLMProvider>();
        builder.Services.AddScoped<LLMChatService>();

        return builder;
    }
}
