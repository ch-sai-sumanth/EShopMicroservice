namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add API services here


        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        // Configure API middleware here

        return app;
    }
}