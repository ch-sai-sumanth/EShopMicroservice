var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddGrpc();

builder.Services.AddDbContext<DiscountContext>(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("DiscountDb")));

var app = builder.Build();

// ✅ Automatically apply any pending EF Core migrations on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DiscountContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline
app.MapGrpcService<DiscountService>();

app.Run();