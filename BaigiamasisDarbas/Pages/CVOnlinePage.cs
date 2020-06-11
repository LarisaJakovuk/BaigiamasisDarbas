using BaigiamasisDarbas.Enums;
using BaigiamasisDarbas.Tests;
using NUnit.Framework;
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
        
        private static IWebElement IeskantisDarboTab => Driver.FindElement(By.Id("tab-1"));
        private static IWebElement FirstNameLangas => Driver.FindElement(By.Id("first_name"));
        private static IWebElement LastNameLangas => Driver.FindElement(By.Id("last_name"));
        private static IWebElement EpostLangas => Driver.FindElement(By.Id("desc_seeker_epost"));
        private static IWebElement ContactTelLangas => Driver.FindElement(By.Id("desc_contact_tel"));
        private static SelectElement DarboKategorijuSarasas => new SelectElement(Driver.FindElement(By.Id("tegvk_id")));
        private static IWebElement SendSpamCheckBox => Driver.FindElement(By.XPath("//label[contains(.,'Ar norėtumėte gauti informacinius CV-Online laiškus?')]")); 
        private static IWebElement SutikimasSuSalygomisCheckBox => Driver.FindElement(By.CssSelector(".form-group:nth-child(7) .input-check-outer > .text-small"));
        private static IWebElement InfoSiuntimasDarbdaviuiCheckBox => Driver.FindElement(By.XPath("//label[contains(.,'Sutinku, kad mano informacija bus išsiųsta darbdaviui')]"));
        private static IWebElement SubmitButton => Driver.FindElement(By.XPath("(//button[@type='submit'])[3]"));
        private static IWebElement RegModalWindow => Driver.FindElement(By.XPath("//*[@id='register-modal']/div"));
        private static IWebElement SuccessRegisrElement => Driver.FindElement(By.CssSelector(".text-center.remove-margin-top"));
        private static IWebElement SuccessModalWindow => Driver.FindElement(By.XPath("//*[@id='register-modal']/div"));
        

        //Konstruktorius
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
        public CVOnlinePage WaitUntilOpenRegModal()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(100));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("register-modal")));
            return this;
        }
        public CVOnlinePage ReklamosUzdarymas()
        {
           
            ReklamosUzdarymoElementas.Click();
            return this;
        }

        
        public CVOnlinePage NaujielaiskioLangoUzdarymas()
        {
            // Sio elemento wedriver nerado...
            NaujielaiskioUzdarymoElementas.Click();
           
            return this;
        }

        public CVOnlinePage PaspaustiMygtukaRegistruotis()
        {
            RegistruotisMygtukas.Click();
            return this;
        }

        public CVOnlinePage PasirinktiIeskanciamDarboTaba()
        {
            IeskantisDarboTab.Click();
            return this;
        }

        public CVOnlinePage IvestiVarda(string vardas)
        {
            FirstNameLangas.SendKeys(vardas);
            return this;
        }

        public CVOnlinePage IvestiPavarde(string pavarde)
        {
            LastNameLangas.SendKeys(pavarde);
            return this;
        }
        public CVOnlinePage IvestiElpastoAdresa(string emailas)
        {
            EpostLangas.SendKeys(emailas);
            return this;
        }
        public CVOnlinePage IvestiTelefonoNr(string tel)
        {
            ContactTelLangas.SendKeys(tel);
            return this;
        }
        public CVOnlinePage SelectDarboKategorija(string darboKategorija)
        {
            string kategorija = darboKategorija.ToString();
            DarboKategorijuSarasas.SelectByText(darboKategorija);
            return this;
        }
        public CVOnlinePage CheckSendSpam(bool spamcheck)
        {
            if (spamcheck==true)
            {
                SendSpamCheckBox.Click();
            }

            return this;
        }

        public CVOnlinePage CheckSutikimas(bool sutikimocheck)
        {
           
            if (sutikimocheck == true)
            {
                            SutikimasSuSalygomisCheckBox.Click();
            }

            return this;
        }
        public CVOnlinePage CheckInfoDarbdaviui(bool infodarbdaviuicheck)
        {
            if (infodarbdaviuicheck == true)
            {
                InfoSiuntimasDarbdaviuiCheckBox.Click();
            }
            return this;
        }
        public CVOnlinePage PaspaustiSubmit()
        {
            SubmitButton.Click();
            return this;
        }
        public CVOnlinePage PatikrintiSekmingaRegistravima()
        {
            Assert.That(SuccessRegisrElement.Text, Is.EqualTo("Dėkui!"), "Registracija įvykdyta nesekmingai");
            return this;
        }
        public CVOnlinePage PatikrintiArAtsidaroSekmingoRegistravimoLangas()
        {
            
            Assert.IsTrue(RegModalWindow.Text.Contains("Dėkui!"), "Registracija įvykdyta nesekmingai");
            return this;
        }
    }
    
}
