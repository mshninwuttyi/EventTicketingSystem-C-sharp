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
        builder.Services.AddDbContext<AppDbContext>(
                options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"))
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking),
                ServiceLifetime.Transient,
                ServiceLifetime.Transient
        );

        return services;
    }

    public static IServiceCollection AddBusinessLogic(this IServiceCollection services)
    {
        services.AddScoped<BL_BusinessOwner>();
        services.AddScoped<BL_EventCategory>();
        services.AddScoped<BL_BusinessEmail>();
        services.AddScoped<BL_QrCode>();
        services.AddScoped<BL_Ticket>();
        services.AddScoped<BL_SearchEventsAndVenues>();
        services.AddScoped<BL_Venue>();
        services.AddScoped<BL_Auth>();

        return services;
    }

    public static IServiceCollection AddDataAccessLogic(this IServiceCollection services)
    {
        services.AddScoped<DA_BusinessOwner>();
        services.AddScoped<DA_EventCategory>();
        services.AddScoped<DA_BusinessEmail>();
        services.AddScoped<DA_QrCode>();
        services.AddScoped<DA_Ticket>();
        services.AddScoped<DA_SearchEventsAndVenues>();
        services.AddScoped<DA_Venue>();
        services.AddScoped<DA_Auth>();
        
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<DapperService>();
        services.AddScoped<CommonService>();
        services.AddScoped<JwtService>();

        return services;
    }

    public static WebApplicationBuilder AddBuilders(this WebApplicationBuilder builder)
    {

        return builder;
    }
}