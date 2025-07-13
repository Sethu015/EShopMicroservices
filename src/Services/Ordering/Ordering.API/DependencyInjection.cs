namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            //Add Web related Services
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication webApplication)
        {
            //Add Middlewares
            return webApplication;
        }
    }
}
