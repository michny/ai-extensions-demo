using Microsoft.Extensions.Configuration;

public static class SecretsProvider
{
    private static IConfigurationRoot config;

    static SecretsProvider()
    {
        config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
    }
    public static string GetSecret(string key)
    {
        string? value = config[key];
        if (key == null)
        {
            Console.WriteLine("No Azure AI Key found");
            throw new InvalidOperationException("No Azure AI Key found");
        }
        return value!;
    }
}
