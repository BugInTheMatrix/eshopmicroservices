using BuildingBlocks.Exceptions.Handler;
using Carter;

namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddCarter();
            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddHealthChecks();
                        // Register API services here
            return services;
        }
        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.MapCarter();
            app.UseExceptionHandler(options => { });
            // Configure API middleware here
            app.UseHealthChecks("/health");
            return app;
        }
    }
}
