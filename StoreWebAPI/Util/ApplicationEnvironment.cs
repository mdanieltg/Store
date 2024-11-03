namespace StoreWebAPI.Util;

public class ApplicationEnvironment
{
    public ApplicationEnvironment(IConfiguration configuration, ILogger<ApplicationEnvironment> logger)
    {
        Environment = configuration["ASPNETCORE_ENVIRONMENT"] switch
        {
            "Production" => DevelopmentEnvironment.Production,
            "Staging" => DevelopmentEnvironment.Staging,
            _ => DevelopmentEnvironment.Development
        };
        logger.LogInformation("The detected environment is {Environment}", Environment);
    }

    public DevelopmentEnvironment Environment { get; }
}
