using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services
    .AddApiServices(builder.Configuration)
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

//Configure the HTTP request pipeline.
app.Run();