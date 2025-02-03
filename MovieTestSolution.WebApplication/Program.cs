using MovieTestSolution.Business.DependencyResolver;
using MovieTestSolution.Core.DependencyResolver;
using MovieTestSolution.Core.Utilities.IoC;
using MovieTestSolution.WebApp.Middlewears;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;

builder.Services.AddBusinessServices();
builder.Services.AddSingleton<ICoreModules>(new CoreModule(configuration));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddLogging();
builder.Services.AddTransient<GlobalHandlingExeptionsMiddlewear>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
