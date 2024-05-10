namespace SeleniumTest;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

public class Tests
{
    private IWebDriver driver;

    private readonly By _buttonPlayWithComp = By.XPath("//button[text() = 'Сыграть с компьютером']");
    private readonly By _buttonRandomColor = By.XPath("//button[@class = 'button button-metal color-submits__button black']");
    private readonly By _buttonEndGame = By.XPath("//button[@class = 'fbt abort']");
    private readonly By _buttenSignIn = By.XPath("//*[@id=\"top\"]/div[2]/a");
    private readonly By _login = By.XPath("//*[@id=\"form3-username\"]");
    private readonly By _password = By.XPath("//*[@id=\"form3-password\"]");
    private readonly By _enter = By.XPath("//*[@id=\"main-wrap\"]/main/form/div[1]/button");
    private readonly By _statusGame = By.XPath("//*[@id=\"main-wrap\"]/main/aside/div/section[2]");
    private readonly By _currentLogin = By.XPath("//*[@id=\"user_tag\"]");

    private const string expectedStatusGame = "Игра отменена";

    [SetUp]
    public void Setup()
    {
        this.driver = new ChromeDriver();
        this.driver.Navigate().GoToUrl("https://lichess.org");
        this.driver.Manage().Window.Maximize();
    }

    [Test]
    public void TestOpen()
    {
        var startPlay = driver.FindElement(_buttonPlayWithComp);
        startPlay.Click();
        Thread.Sleep(1000);

        var choiceColor = driver.FindElement(_buttonRandomColor);
        choiceColor.Click();
        Thread.Sleep(1000);

        var endGame = driver.FindElement(_buttonEndGame);
        endGame.Click();
        Thread.Sleep(1000);

        var statusGame = driver.FindElement(_statusGame).Text;

        Assert.That(statusGame, Is.EqualTo(expectedStatusGame));
    }

    [Test]
    public void TestLoginAndPasswordInput()
    {
        var signin = driver.FindElement(_buttenSignIn);
        signin.Click();
        Thread.Sleep(400);

        var login = driver.FindElement(_login);
        login.SendKeys(LogPas.Login);
        Thread.Sleep(400);

        var password = driver.FindElement(_password);
        password.SendKeys(LogPas.Password);
        Thread.Sleep(400);

        var enter = driver.FindElement(_enter);
        enter.Click();
        Thread.Sleep(400);

        var current = driver.FindElement(_currentLogin).Text;

        Assert.That(current, Is.EqualTo(LogPas.Login));
    }

    [TearDown]
    public void TearDown()
    {
        this.driver.Close();
        this.driver.Quit();
    }
}