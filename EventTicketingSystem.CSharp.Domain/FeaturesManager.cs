namespace EventTicketingSystem.CSharp.Domain;

public static class FeaturesManager
{
    public static IServiceCollection AddModularServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddDatabaseConnection(builder);
        services.AddBusinessLogic();
        services.AddDataAccessLogic();
        services.AddServices();
        builder.AddBuilders();
        return services;
    }

    public static IServiceCollection AddDatabaseConnection(this IServiceCollection services, WebApplicationBuilder builder)
    {

        return services;
    }

    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        services.AddScoped<BL_BusinessOwner>();

        return services;
    }

    public static IServiceCollection AddDataAccessLogic(this IServiceCollection services)
    {
        services.AddScoped<DA_BusinessOwner>();

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {

        return services;
    }

    public static WebApplicationBuilder AddBuilders(this WebApplicationBuilder builder)
    {

        return builder;
    }
}