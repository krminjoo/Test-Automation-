using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Xml.Linq;

namespace TestProject1;

[TestFixture]
class UnitTest1
{
    IWebDriver driver;

    [SetUp]
    public void Setup()
    {
        string driverPath = @"C:\Program Files\Google\Chrome\Application";
        driver = new ChromeDriver(driverPath);
    }

    [Test]
    public void InvalidLoginTest()
    {
        // Go to url
        driver.Navigate().GoToUrl("https://box5877.bluehost.com:2096/");

        // Enter id
        driver.FindElement(By.Id("user")).SendKeys("id");

        // Enter password
        driver.FindElement(By.Id("pass")).SendKeys("ssss");

        // Click login button
        driver.FindElement(By.Name("login")).Click();

        // set IWebElement variable
        IWebElement loginStatus = driver.FindElement(By.Id("login-status-message"));

        // Wait method
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        wait.Until(ExpectedConditions.TextToBePresentInElement(loginStatus, "The login is invalid"));

        // Assert
        StringAssert.Contains("The login is invalid", loginStatus.Text);
    }

    [Test]
    public void ValidLoginTest()
    {
        // Go to url
        driver.Navigate().GoToUrl("https://box5877.bluehost.com:2096/");

        // Enter id
        driver.FindElement(By.Id("user")).SendKeys("admin@sksolution.co.nz");

        // Enter password
        driver.FindElement(By.Id("pass")).SendKeys("PAssword!@!@");

        // Click login button
        driver.FindElement(By.Name("login")).Click();

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("username")));
        
        // Assert
        StringAssert.Contains("admin@sksolution.co.nz",driver.FindElement(By.ClassName("username")).Text);
    }

    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}