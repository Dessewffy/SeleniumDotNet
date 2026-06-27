using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Selenium
{
    public static class SeleniumCustomMethods
    {

        // Here we need the this keyword + static callss and method for extension methods
        public static void HandleInputFields(this IWebElement locator, string text)
        {
            locator.Clear();
            locator.SendKeys(text);
        }
    }
}
