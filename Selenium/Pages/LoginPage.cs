using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Selenium.Pages
{
    public class LoginPage(IWebDriver _driver, WebDriverWait _wait) // Primary constructor
    {
        /*
        private IWebDriver _driver;
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
        }
        */
        public IWebElement LoginButton => _driver.FindElement(By.XPath("//a[@href='/login']"));
        public IWebElement EmailInput => _driver.FindElement(By.Id("email"));
        public IWebElement PasswordInput => _driver.FindElement(By.Id("password"));
        public IWebElement Submit => _driver.FindElement(By.XPath("//button[@type='submit']"));
        public IWebElement Products => _driver.FindElement(By.XPath("//*[@id=\'radix-_r_1k_-trigger-products\']"));
        public IWebElement Courses => _driver.FindElement(By.XPath("//a[@href='/products?page=1&types=Course']"));
        public IWebElement MyLibraryText => _driver.FindElement(By.XPath("//*[@id=\"main\"]/div/h1"));

        public IWebElement IncorrectCredentialsText => _driver.FindElement(By.XPath("//html/body/div[2]/div[4]/div/div[2]/h6"));

        public void Login(string username, string password)
        {
            _wait.Until(_ => LoginButton.Displayed);
            LoginButton.Click();

            _wait.Until(_ => EmailInput.Displayed && PasswordInput.Displayed);
            EmailInput.SendKeys(username);

            PasswordInput.SendKeys(password);

            Submit.Click();
        }
        public bool IsLoggedIn()
        {
            try
            {
                _wait.Until(_ => MyLibraryText.Displayed);
                return MyLibraryText.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public bool LoginErrorDisplayed()
        {
            try
            {
                _wait.Until(_ => IncorrectCredentialsText.Displayed);

                return IncorrectCredentialsText.Enabled;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
