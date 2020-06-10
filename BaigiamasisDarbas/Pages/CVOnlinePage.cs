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
       
       
        private static IWebElement NaujielaiskioUzdarymoElementas => Driver.FindElement(By.CssSelector("a[href$='javascript:close()']"));
        private static IWebElement RegistruotisMygtukas => Driver.FindElement(By.XPath("//a[contains(@href, '/register')]"));

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
        public CVOnlinePage WaitUntilOpenPopUpMailerlite()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(100));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("ml-webforms-popup-295655")));
            return this;
        }
        public CVOnlinePage ReklamosUzdarymas()
        {
           
            ReklamosUzdarymoElementas.Click();
            return this;
        }

        
        public CVOnlinePage NaujielaiskioLangoUzdarymas()
        {
            
            NaujielaiskioUzdarymoElementas.Click();
           
            return this;
        }

        public CVOnlinePage PaspaustiMygtukaRegistruotis()
        {
            RegistruotisMygtukas.Click();
            return this;
        }
    }
}
