using Assistant.Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.AddInfrastructure();
builder.Services.AddControllers();
builder.Services.AddOpenApi()
    .AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*app.UseHttpsRedirection();*/
app.MapControllers();

await app.RunAsync()
    .ConfigureAwait(false);
