using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Ordering.API.Endpoints;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly IConfiguration _config;

    public HealthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet("db-check")]
    public IActionResult CheckDatabase()
    {
        var connectionString = _config.GetConnectionString("Database");
        try
        {
            using var conn = new SqlConnection(connectionString);
            conn.Open();
            return Ok("✅ Connected to SQL Server successfully!");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"❌ Database connection failed: {ex.Message}");
        }
    }
}
