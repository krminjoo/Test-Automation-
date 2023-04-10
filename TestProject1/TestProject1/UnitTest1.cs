using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Transactions;
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
        StringAssert.Contains("admin@sksolution.co.nz", driver.FindElement(By.ClassName("username")).Text);
    }

    [Test]
    public void InvalidLoginTest1()  //valid id + invalid password
    {
        // Go to url
        driver.Navigate().GoToUrl("https://box5877.bluehost.com:2096/");

        // Enter id
        driver.FindElement(By.Id("user")).SendKeys("admin@sksolution.co.nz");

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
    public void InvalidLoginTest2() //invalid id + invalid password
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
    public void InvalidLoginTest3() //invalid id + valid password
    {
        // Go to url
        driver.Navigate().GoToUrl("https://box5877.bluehost.com:2096/");

        // Enter id
        driver.FindElement(By.Id("user")).SendKeys("id");

        // Enter password
        driver.FindElement(By.Id("pass")).SendKeys("PAssword!@!@");

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
    public void InvalidLoginTest4() // without any credential
    {
        // Go to url
        driver.Navigate().GoToUrl("https://box5877.bluehost.com:2096/");

        // Enter id
        driver.FindElement(By.Id("user")).Click();

        // Enter password
        driver.FindElement(By.Id("pass")).Click();

        // Click login button
        driver.FindElement(By.Name("login")).Click();

        // set IWebElement variable
        IWebElement loginStatus = driver.FindElement(By.Id("login-status-message"));

        // Wait method
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        wait.Until(ExpectedConditions.TextToBePresentInElement(loginStatus, "You must specify a username to log in"));

        // Assert
        StringAssert.Contains("You must specify a username to log in", loginStatus.Text);
    }

    [Test]
    public void LoginKeyboardTest()  //Validate Tab and Enter keys
    {
        // Go to url
        driver.Navigate().GoToUrl("https://box5877.bluehost.com:2096/");

        // Enter ID
        driver.FindElement(By.Id("user")).SendKeys("admin@sksolution.co.nz");

        // Press Tab key to move to password field
        driver.FindElement(By.Id("user")).SendKeys(Keys.Tab);

        // Wait for password field to become enabled
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        var passwordField = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("pass")));

        // Enter password
        passwordField.SendKeys("PAssword!@!@");

        //Press Enter key to login
        driver.FindElement(By.Id("pass")).SendKeys(Keys.Enter);

        wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("username")));

        // Assert
        StringAssert.Contains("admin@sksolution.co.nz", driver.FindElement(By.ClassName("username")).Text);
    }

    [Test]
    public void LoginPlaceholder() // Validate Placeholder text (user(X), pass(o)
    {
        // Go to url
        driver.Navigate().GoToUrl("https://box5877.bluehost.com:2096/");

        // set IWebElement variable
        IWebElement userElement = driver.FindElement(By.CssSelector("input[value][placeholder='Enter your email address.']"));
        IWebElement passElement = driver.FindElement(By.CssSelector("input[placeholder='Enter your email password.']"));

        // Assert
        StringAssert.Contains("Enter your email address", passElement.GetAttribute("placeholder"));
        StringAssert.Contains("Enter your email password", passElement.GetAttribute("placeholder"));
    }

    [Test]
    public void LoginBrowseback() // Validate logging in and browsing back
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
        StringAssert.Contains("admin@sksolution.co.nz", driver.FindElement(By.ClassName("username")).Text);

        //Click back button
        driver.Navigate().Back();

        // Validate login status - logged out
        wait.Until(ExpectedConditions.TextToBePresentInElement(driver.FindElement(By.Id("user")), "admin@sksolution.co.nz"));

        // set IWebElement variable
        IWebElement loginStatus = driver.FindElement(By.Id("login-status-message"));

        // Assert
        StringAssert.Contains("The security token is missing from your request",loginStatus.Text);
            }

    [TearDown]
    public void TearDown()
    {
        driver.Close();
    }
}