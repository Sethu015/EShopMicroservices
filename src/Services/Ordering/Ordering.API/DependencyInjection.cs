namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            //Add Web related Services
            services.AddCarter(new DependencyContextAssemblyCatalogCustom());
            TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication webApplication)
        {
            //Add Middlewares
            webApplication.MapCarter();
            return webApplication;
        }
    }
}
