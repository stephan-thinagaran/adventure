using Adventure.AdventureWork.Person.Infrastructure.Configuration;
using Carter;
using FluentValidation;

var assembly = typeof(Program).Assembly;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.AddServiceDefaults();
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
builder.Services.AddCarter();

builder.AddInfrastructureServices(builder.Configuration);
builder.Services.RegisterDependencies();

var app = builder.Build();

app.MapDefaultEndpoints();
app.MapCarter();
app.UseOutputCache();
app.Run();
