using OpenQA.Selenium;

namespace Selenium
{
    public static class SeleniumCustomMethods
    {

        public static void HandleInputFields(this IWebElement locator, string text)
        {
            locator.Clear();
            locator.SendKeys(text);
        }
    }
}
