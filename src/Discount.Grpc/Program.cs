var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddGrpc();

builder.Services.AddDbContext<DiscountContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();

// ✅ Automatically apply any pending EF Core migrations on startup
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DiscountContext>();
    try
    {
        db.Database.Migrate();
        Console.WriteLine("✅ Database migrated successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Migration failed: {ex.Message}");
    }
}

// Configure the HTTP request pipeline
app.MapGrpcService<DiscountService>();

app.Run();