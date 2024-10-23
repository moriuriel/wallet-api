namespace Wallets.Api.Commons;

public class Configuration
{
     public static IConfigurationRoot GetConfiguration()
     {
          var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.Trim();
          if (string.IsNullOrEmpty(environment))
          {
               Console.WriteLine("");
               Environment.Exit(exitCode: 1);
          }

          var initialData = new List<KeyValuePair<string, string?>>()
        {
            new KeyValuePair<string, string?>("Environment", environment)
        };

          return new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile(path: Path.Combine(AppContext.BaseDirectory, "appsettings.json"), false)
              .AddJsonFile(path: Path.Combine(AppContext.BaseDirectory, $"appsettings.{environment}.json"), false)
              .AddInMemoryCollection(initialData).Build();
     }
}
