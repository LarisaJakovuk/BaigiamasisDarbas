using BaigiamasisDarbas.Drivers;
using BaigiamasisDarbas.Pages;
using BaigiamasisDarbas.Tools;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace BaigiamasisDarbas.Tests
{
    public class BaseTest

    {
        public static IWebDriver _driver;
        public static CVOnlinePagrindinisPage _cvonlinePagrindinisPage;
        public static DarboPaieskosPage _darboPaieskosPage;
        public static KlientuPaslauguPaketuKainosPage _klientuPaslauguPaketuKainosPage;

        [OneTimeSetUp]
        public static void SetUpChrome()
        {
            _driver = CustomDrivers.GetChromeWithOptions();
            _cvonlinePagrindinisPage = new CVOnlinePagrindinisPage(_driver);
            _darboPaieskosPage = new DarboPaieskosPage(_driver);
           _klientuPaslauguPaketuKainosPage = new KlientuPaslauguPaketuKainosPage(_driver);

            _cvonlinePagrindinisPage.OpenCvonlinePage()
           .WaitUntilOpenPopUpMailerlite()
           .AddAdvertisingConsentCookies();
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
           _driver.Quit();
        }

    }
}
