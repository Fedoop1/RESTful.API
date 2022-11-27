using Microsoft.EntityFrameworkCore;
using RESTful.API.Data;
using RESTful.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RestfulDbContext>(config =>
{
    config.UseInMemoryDatabase("restful");
});

var services = builder.Services.BuildServiceProvider();

await services.GetService<RestfulDbContext>()!.SeedAsync();

await services.DisposeAsync();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();