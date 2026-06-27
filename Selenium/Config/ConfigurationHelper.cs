
using Microsoft.Extensions.Configuration;

namespace Selenium.Config
{
    public static class ConfigurationHelper
    {
        private static readonly IConfigurationRoot Configuration = BuildConfiguration();

        private static IConfigurationRoot BuildConfiguration()
        {
            // Find the solution/project root by looking for the Config folder
            var baseDirectory = AppContext.BaseDirectory;
            var configPath = Path.Combine(baseDirectory, "Config", "ValidData.json");

            // If not found in output directory, try searching upwards from the assembly location
            if (!File.Exists(configPath))
            {
                var directory = new DirectoryInfo(baseDirectory);
                while (directory != null)
                {
                    var potentialPath = Path.Combine(directory.FullName, "Config", "ValidData.json");
                    if (File.Exists(potentialPath))
                    {
                        configPath = potentialPath;
                        break;
                    }
                    directory = directory.Parent;
                }
            }

            return new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(configPath) ?? AppContext.BaseDirectory)
                .AddJsonFile(Path.GetFileName(configPath) ?? "ValidData.json", optional: false)
                .Build();
        }

        public static string? Email => Configuration["Login:Email"];
        public static string? Password => Configuration["Login:Password"];
    }
}