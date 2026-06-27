using Microsoft.Extensions.Configuration;

namespace Selenium.Config
{

    public static class ConfigurationHelper
    {
        private static readonly IConfigurationRoot Configuration =
            new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("Config/ValidData.json", optional: true)
                .Build();

        public static string Email =>
            Environment.GetEnvironmentVariable("GAMEDEV_EMAIL")
            ?? Configuration["Login:Email"]
            ?? throw new InvalidOperationException("Email not found.");

        public static string Password =>
            Environment.GetEnvironmentVariable("GAMEDEV_PASSWORD")
            ?? Configuration["Login:Password"]
            ?? throw new InvalidOperationException("Password not found.");
    }
}