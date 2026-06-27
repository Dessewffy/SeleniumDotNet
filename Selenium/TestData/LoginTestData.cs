using NUnit.Framework;
using Selenium.Config;

namespace Selenium.TestData;



public static class LoginTestData
{
    public static IEnumerable<TestCaseData> LoginCases
    {
        get
        {
            string? email = null;
            string? password = null;
            Exception? configError = null;

            try
            {
                email = ConfigurationHelper.Email;
                password = ConfigurationHelper.Password;

                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                    throw new InvalidOperationException(
                        "Email vagy Password hiányzik a ValidData.json-ból.");
            }
            catch (Exception ex)
            {
                configError = ex;
            }

            if (configError != null)
            {
                yield return new TestCaseData(null, null, false)
                    .SetName("Config load FAILED")
                    .Explicit("Configuration error: " + configError.Message);
                yield break;
            }
            yield return new TestCaseData(email!, password!, true)
                .SetName("Valid login");
            yield return new TestCaseData("wrong@example.com", password!, false)
                .SetName("Invalid email");
            yield return new TestCaseData(email!, "WrongPassword", false)
                .SetName("Invalid password");

        }
    }
}