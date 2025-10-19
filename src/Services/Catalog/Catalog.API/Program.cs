using Carter;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});

var app = builder.Build();

app.MapCarter();
app.UseHttpsRedirection();
app.Run();