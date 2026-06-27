using System.Runtime.InteropServices;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using Selenium.Driver;
using Selenium.Pages;
using Selenium.TestData;

namespace Selenium
{
    public class Tests
    {

        private IWebDriver _driver;
        private WebDriverWait _wait;
        private LoginPage? _loginPage;
        private DriverType _driverType;
        private ExtentReports _report = new ExtentReports();
        private ExtentTest _extentTest;
        public IWebDriver GetDriver(DriverType driverType)
        {
            return driverType switch
            {
                DriverType.Chrome => new ChromeDriver(),
                DriverType.Firefox => new FirefoxDriver(),
                DriverType.Edge => new EdgeDriver(),
                _ => new ChromeDriver(),
            };
        }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _report = new ExtentReports();

            var spark = new ExtentSparkReporter("TestReport.html");
            _report.AttachReporter(spark);
        }
        [SetUp]
        public void Setup()
        {
            _driverType = DriverType.Chrome;
            _driver = GetDriver(_driverType);
            SetupExtentReports();
            _extentTest.Log(Status.Info, "Browser launched");
            _driver.Navigate().GoToUrl("https://gamedev.tv");
            _driver.Manage().Window.Maximize();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10))
            {
                PollingInterval = TimeSpan.FromMilliseconds(200)
            };
            _wait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
        }

        private void SetupExtentReports()
        {
            _report.AddSystemInfo("OS", RuntimeInformation.OSDescription);
            _report.AddSystemInfo("Browser", _driverType.ToString());
            _extentTest = _report.CreateTest(TestContext.CurrentContext.Test.Name);

        }
        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            switch (status)
            {
                case NUnit.Framework.Interfaces.TestStatus.Passed:
                    _extentTest.Pass("Test passed");
                    break;

                case NUnit.Framework.Interfaces.TestStatus.Failed:
                    _extentTest.Fail(TestContext.CurrentContext.Result.Message);
                    _extentTest.Fail(TestContext.CurrentContext.Result.StackTrace);
                    break;

                case NUnit.Framework.Interfaces.TestStatus.Skipped:
                    _extentTest.Skip("Test skipped");
                    break;
            }

            _extentTest.Info("Browser quit");

            _driver?.Quit();
            _driver?.Dispose();

            _report.Flush();
        }


        [Category("Smoke")]
        [TestCaseSource(typeof(LoginTestData), nameof(LoginTestData.LoginCases))]
        public void Login(string username, string password, bool shouldLogin)
        {


            //Arrange
            _loginPage = new LoginPage(_driver, _wait);

            //Act
            _loginPage.Login(username, password);


            //Assert
            if (shouldLogin)
            {
                Assert.That(_loginPage.IsLoggedIn(), Is.True);
                _extentTest.Log(Status.Info, "Login was successfull");
            }
            else
            {
                Assert.That(_loginPage.LoginErrorDisplayed(), Is.True);
                _extentTest.Log(Status.Info, "Error message appeared");
            }
        }


        [Category("Fluent")]
        [TestCaseSource(typeof(LoginTestData), nameof(LoginTestData.LoginCases))]
        public void LoginUseFluentAssertion(string username, string password, bool shouldLogin)
        {
            if (username == null || password == null)
                Assert.Fail("Json file path is wrong!");

            //Arrange
            _loginPage = new LoginPage(_driver, _wait);

            //Act
            _loginPage.Login(username, password);

            //Assert
            if (shouldLogin)
            {
                _wait.Until(_ => _loginPage.MyLibraryText.Displayed);
                _loginPage.MyLibraryText.Should().NotBeNull();
                _extentTest.Log(Status.Info, "Login was successfull");
            }
            else
            {
                Assert.That(_loginPage.LoginErrorDisplayed(), Is.True);
                _extentTest.Log(Status.Info, "Error message appeared");
            }
        }


    }

}