using Microsoft.OpenApi.Models;
using UdemyCarBook.Application.Features.CQRS.Handlers.AboutHandlers;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Persistence.Context;
using UdemyCarBook.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Swagger servislerini ekle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddScoped<CarBookContext>();


builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<GetAboutQueryHandler>();
builder.Services.AddScoped<GetAboutByIdQueryHandler>();
builder.Services.AddScoped<CreateAboutCommandHandler>();
builder.Services.AddScoped<UpdateAboutCommandHandler>();
builder.Services.AddScoped<RemoveAboutCommandHandler>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1"
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
