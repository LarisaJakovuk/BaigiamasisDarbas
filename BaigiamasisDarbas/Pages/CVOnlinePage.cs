using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaigiamasisDarbas.Pages
{
    public class CVOnlinePage : BasePage
    {
        //elementai:
        private string pageAdress = "http://cvonline.lt";
        private static IWebElement ReklamosUzdarymoElementas => Driver.FindElement(By.ClassName("close"));
        private static IWebElement NaujielaiskioUzdarymoElementas => Driver.FindElement(By.ClassName("close-popup.glyphicon.glyphicon-remove"));
        public CVOnlinePage(IWebDriver webdriver) : base(webdriver)
        { }

        //metodai:
        
        public CVOnlinePage OpenCvonlinePage()
        {
            if (Driver.Url != pageAdress)
                Driver.Url = pageAdress;
            return this;
        }
        public CVOnlinePage AddAdvertisingConsentCookies()
        {
            Driver.Manage().Cookies.AddCookie(new Cookie(
                "acceptcookies",
                "1",
                ".www.cvonline.lt", "/", DateTime.Now.AddDays(10)));

            Driver.Navigate().Refresh();

            return this;
        }
        public CVOnlinePage ReklamosUzdarymas()
        {
            GetWait(5);
            ReklamosUzdarymoElementas.Click();
            return this;
        }

        public CVOnlinePage DismissAlert()
        {
            IAlert alert = Driver.SwitchTo().Alert();

            Console.WriteLine($"Alert says {alert.Text}");

            alert.Dismiss();
            return this;
        }
        public CVOnlinePage NaujielaiskioLangoUzdarymas()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.ClassName("mailerlite-form-slidebox")));
           
            NaujielaiskioUzdarymoElementas.Click();
            return this;
        }
    }
}
