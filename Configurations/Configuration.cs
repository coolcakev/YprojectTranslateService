using y_nuget;
using y_nuget.RabbitMq;
using YprojectTranslateService.Database;
using YprojectTranslateService.RabbitMq;

namespace YprojectTranslateService.Configurations;

public static class Configuration
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, WebApplicationBuilder builder)
    {   
        var configuration = builder.Configuration;
        services.AddYNugetConfiguration(builder);
        services.AddRabbitMqConfig(builder, x =>
        {
            x.AddConsumer<TranslateConsumerService>();
        });
        services.AddDataBaseConfig(configuration);
        services.AddSwaggerGen();
        
        return services;
    }   
}       