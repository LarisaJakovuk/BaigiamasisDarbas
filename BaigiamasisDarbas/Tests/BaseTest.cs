using BaigiamasisDarbas.Drivers;
using BaigiamasisDarbas.Pages;
using BaigiamasisDarbas.Tools;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;


namespace BaigiamasisDarbas.Tests
{
    public class BaseTest

    {
        public static IWebDriver _driver;
        public static CVOnlinePage _cvonlinePage;

        [OneTimeSetUp]
        public static void SetUpChrome()
        {
            _driver = CustomDrivers.GetChromeWithOptions();
            _cvonlinePage = new CVOnlinePage(_driver);
        }

        // vykdomas kaskart po kiekvieno testo
        [TearDown]
        public static void SingleTestTearDown()
        {
            if (TestContext.CurrentContext.Result.Outcome != ResultState.Success)
            {
                // darom screenshota:
                ErrorScreenShot.MakePhoto(_driver);
            }
        }

        // vykdomas viena karta po visu testu
        [OneTimeTearDown]
        public static void CloseBrowser()
        {
            //_driver.Quit();
        }
    }
}
