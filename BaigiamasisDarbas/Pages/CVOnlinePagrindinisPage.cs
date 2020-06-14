using BaigiamasisDarbas.Enums;
using BaigiamasisDarbas.Tests;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace BaigiamasisDarbas.Pages
{
    public class CVOnlinePagrindinisPage : BasePage
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
        private static IWebElement SendSpamCheckBox => Driver.FindElement(By.CssSelector(".form-group:nth-child(6) .input-check-outer"));
        //(By.XPath("//label[contains(.,'Ar norėtumėte gauti informacinius CV-Online laiškus?')]")); 
        private static IWebElement SutikimasSuSalygomisCheckBox => Driver.FindElement(By.CssSelector(".form-group:nth-child(7) .input-check-outer"));
        //".form-group:nth-child(7) .input-check-outer > .text-small"
        private static IWebElement InfoSiuntimasDarbdaviuiCheckBox => Driver.FindElement(By.CssSelector(".form-group:nth-child(8) .input-check-outer"));
        //(By.XPath("//label[contains(.,'Sutinku, kad mano informacija bus išsiųsta darbdaviui')]"));
        private static IWebElement SubmitButton => Driver.FindElement(By.XPath("(//button[@type='submit'])[3]"));
        private static IWebElement RegModalWindow => Driver.FindElement(By.XPath("//*[@id='register-modal']/div"));
        private static IWebElement SuccessRegisrElement => Driver.FindElement(By.XPath("//h2[contains(.,'Dėkui!')]"));
        private static IWebElement SuccessModalWindow => Driver.FindElement(By.CssSelector(".remove-margin-top"));

        //Elementai paieska inicijuoti pagrindiniame puslapyje
        private static IWebElement DarboPaieskosMygtukas => Driver.FindElement(By.Id("searchbutton"));
        private static IWebElement DarboTypoIvedimoLaukas => Driver.FindElement(By.CssSelector(".select-large:nth-child(1) .select2-search__field"));
        private static IWebElement DarboTypoPasirnikimoElementas => Driver.FindElement(By.XPath("//*[@id='lt']/span/span/span"));
        private static IWebElement MiestoIvedimoLaukas => Driver.FindElement(By.ClassName("select-large select-location-width"));
        private static IWebElement DarboVietosElementas => Driver.FindElement(By.Id("select2-select-location-results"));
        private static IWebElement DarbuSkaiciusPagalFiltra => Driver.FindElement(By.Id("frontpagecount"));
        public static List<string> PasirinktiDarbai = new List<string>();
        public static List<string> PasirinktiMiestai = new List<string>();
        public int pasirinktuDarbuSkaicius;
        public string pasirinktasDarbas;

        //Prisijungimo / Atsijungimo elementai

        private static IWebElement PrisijungimoMygtukas => Driver.FindElement(By.XPath("//*[@id='header-users']/ul[1]/li[1]/a"));
        private static IWebElement PrisijungimoVardoLaukas => Driver.FindElement(By.XPath("//input[@name='username']"));
        private static IWebElement SlaptazodzioLaukas => Driver.FindElement(By.XPath("//input[@type='password']"));
        private static IWebElement MygtukasPrisijungtiZalias => Driver.FindElement(By.XPath("//*[@id='passlogin']/form/div[5]/div/button")); 
        private static IWebElement LoginMessage => Driver.FindElement(By.Id("loginmessage"));
        private static IWebElement UserLoggedInElement => Driver.FindElement(By.XPath("//*[@id='header-users']/ul[1]/li/a/span"));

        private static IWebElement AtsijungimoMygtukas => Driver.FindElement(By.XPath("//*[@id='header-users']/ul[2]/li[1]/a"));

        private static IWebElement VisiSkelbimaiMygtukas => Driver.FindElement(By.XPath("//*[@id='cvonl_mainmenu_sub_personal']/ul/li[1]/a"));


        //Konstruktorius
        public CVOnlinePagrindinisPage(IWebDriver webdriver) : base(webdriver)
        { }

        //METODAI:
        //-----Pagrindinio puslapio isvalymas nuo cookies, reklamos ir t.t.
        public CVOnlinePagrindinisPage OpenCvonlinePage()
        {
            if (Driver.Url != pageAdress)
                Driver.Url = pageAdress;
            return this;
        }
        public CVOnlinePagrindinisPage AddAdvertisingConsentCookies()
        {
            Driver.Manage().Cookies.AddCookie(new Cookie(
                "acceptcookies",
                "1",
                ".www.cvonline.lt", "/", DateTime.Now.AddDays(10)));

            Driver.Navigate().Refresh();

            return this;
        }
        public CVOnlinePagrindinisPage WaitUntilOpenPopUpMailerlite()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(100));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("ml-webforms-popup-295655")));
            return this;
        }

        public CVOnlinePagrindinisPage ReklamosUzdarymas()
        {

            ReklamosUzdarymoElementas.Click();
            return this;
        }


        public CVOnlinePagrindinisPage NaujielaiskioLangoUzdarymas()
        {
            // Sio elemento wedriver nerado...
            NaujielaiskioUzdarymoElementas.Click();

            return this;
        }
        //-----Registracijos metodai
        public CVOnlinePagrindinisPage WaitUntilOpenRegModal()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(100));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("register-modal")));
            return this;
        }
        public CVOnlinePagrindinisPage PaspaustiMygtukaRegistruotis()
        {
            RegistruotisMygtukas.Click();
            return this;
        }

        public CVOnlinePagrindinisPage PasirinktiIeskanciamDarboTaba()
        {
            IeskantisDarboTab.Click();
            return this;
        }

        public CVOnlinePagrindinisPage IvestiVarda(string vardas)
        {
            FirstNameLangas.SendKeys(vardas);
            return this;
        }

        public CVOnlinePagrindinisPage IvestiPavarde(string pavarde)
        {
            LastNameLangas.SendKeys(pavarde);
            return this;
        }
        public CVOnlinePagrindinisPage IvestiElpastoAdresa(string emailas)
        {
            EpostLangas.SendKeys(emailas);
            return this;
        }
        public CVOnlinePagrindinisPage IvestiTelefonoNr(string tel)
        {
            ContactTelLangas.SendKeys(tel);
            return this;
        }
        public CVOnlinePagrindinisPage SelectDarboKategorija(string darboKategorija)
        {
            string kategorija = darboKategorija.ToString();
            DarboKategorijuSarasas.SelectByText(darboKategorija);
            return this;
        }
        public CVOnlinePagrindinisPage CheckSendSpam(bool spamcheck)
        {
            if (spamcheck == true)
            {
                SendSpamCheckBox.Click();
            }

            return this;
        }

        public CVOnlinePagrindinisPage CheckSutikimas(bool sutikimocheck)
        {

            if (sutikimocheck)
            {
                SutikimasSuSalygomisCheckBox.Click();
                //}
                //Actions build = new Actions(Driver);
                //build.MoveToElement(SutikimasSuSalygomisCheckBox).MoveByOffset(10, 10).Click().Build().Perform();
            }

            return this;
        }
        public CVOnlinePagrindinisPage CheckInfoDarbdaviui(bool infodarbdaviuicheck)
        {
            if (infodarbdaviuicheck)
            {
                InfoSiuntimasDarbdaviuiCheckBox.Click();
                //Actions build = new Actions(Driver);
                //build.MoveToElement(InfoSiuntimasDarbdaviuiCheckBox).MoveByOffset(10, 10).Click().Build().Perform();
            }
            return this;
        }
        public CVOnlinePagrindinisPage PaspaustiSubmit()
        {
            SubmitButton.Click();
            return this;
        }
        public CVOnlinePagrindinisPage PatikrintiSekmingaRegistravima()
        {
            // Thread.Sleep(TimeSpan.FromSeconds(5));
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(100));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//h2[contains(.,'Dėkui!')]")));
            string message = SuccessRegisrElement.Text;
            Assert.IsTrue(message.Contains("Dėkui!"), $"Registracija įvykdyta nesekmingai, parasyta {message}");
            Actions build = new Actions(Driver);
            build.MoveToElement(Driver.FindElement(By.XPath("//*[@id='modal-register']/a"))).MoveByOffset(-5, -5).Click().Build().Perform();
            // Driver.FindElement(By.XPath("//*[@id='modal-register']/a")).Click();
            return this;
        }

        public void ClickElement(IWebElement element)
        {
            Actions actions = new Actions(Driver);
            actions.KeyDown(Keys.Control);
            actions.Click(element);
            actions.KeyUp(Keys.Control);
            actions.Build().Perform();
        }
        public CVOnlinePagrindinisPage PasirinktiDarbaPagalFiltra(string darbai)
        {
            Driver.Url = pageAdress;
            ClickElement(DarboTypoIvedimoLaukas);
            IReadOnlyCollection<IWebElement> DarboTypuSarasas = Driver.FindElements(By.XPath("//*[contains(@id,'select2-select-field-result')]"));

            string pasirinktasDarbas;
            int tikSkaicius;

            foreach (var darbotypas in DarboTypuSarasas)
            {
                //DarboTypoIvedimoLaukas.Click();
                if (darbotypas.Text.Length < 70 && darbotypas.Text.Length > 10)
                {
                    string darb1 = darbotypas.Text.Substring(0, 10);

                    if (darbai.Contains(darb1))
                    {
                        ClickElement(darbotypas);
                        pasirinktasDarbas = darbotypas.Text;
                        string newString = Regex.Replace(pasirinktasDarbas, "[^.0-9]", "");
                        pasirinktuDarbuSkaicius = pasirinktuDarbuSkaicius + Convert.ToInt32(newString);
                        PasirinktiDarbai.Add(pasirinktasDarbas);
                    }
                }
            }


            return this;
        }
        //public CVOnlineRegistracijosPage PasirinktiDarboMiestaPagalFiltra(List<string> siulomiMiestai)
        //{
        //    IReadOnlyCollection<IWebElement> MiestuSarasas = Driver.FindElements(By.XPath("//*[@id='select2-select-location-results']"));
        //    foreach (var miestas in MiestuSarasas)
        //    {

        //        MiestoIvedimoLaukas.Click();

        //        if (siulomiMiestai.Contains(miestas.Text)  )
        //            ClickElement(miestas);


        //        PasirinktiMiestai.Add(miestas.Text);
        //    }

        //    return this;
        //}

        public CVOnlinePagrindinisPage PatikrintiAtrinktuDarbuSkaciuRezultateZemiauMygtukoIeskoti(int isrinktuDarbuSkaicius)
        {
            int rezultatoSkaicius = Convert.ToInt32(DarbuSkaiciusPagalFiltra.Text.Replace(" ", ""));
            string expectedSk = DarbuSkaiciusPagalFiltra.Text;
            string actualSk = isrinktuDarbuSkaicius.ToString();

            Assert.IsTrue(rezultatoSkaicius == isrinktuDarbuSkaicius, $"Rezultate {actualSk} darbu, o pasirinkta {expectedSk}");

            return this;
        }


        //public DarboPaieskosPage PaspaustiIeskotiMygtuka(int isrinktuDarbuSkaicius)
        //{
        //    DarboPaieskosMygtukas.Click();

        //    return new DarboPaieskosPage(Driver);
        //}

        public CVOnlinePagrindinisPage PaspaustiPrisijungti()
        {
            PrisijungimoMygtukas.Click();
            return this;
        }
        public CVOnlinePagrindinisPage SuvestiPrisijungimoDuomenis(string vardas, string slaptazodis)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(100));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id='login-modal']/div"))); 
            PrisijungimoVardoLaukas.SendKeys(vardas);
            SlaptazodzioLaukas.SendKeys(slaptazodis);
            return this;
        }
        public CVOnlinePagrindinisPage PaspaustiZaliaMygtukaPrisijungti()
        {
            MygtukasPrisijungtiZalias.Click();
            return this;
        }
        public CVOnlinePagrindinisPage PatikrintiSekmingoPrisijungimoRezultata()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(100));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id='header-users']/ul[1]/li/a/span"))); 
            IWebElement useris = Driver.FindElement(By.XPath("//*[@id='header-users']/ul[1]/li/a/span"));
            
            Assert.That(AtsijungimoMygtukas.Displayed, "Atsijungimo mygtuko nerasta");
            Console.WriteLine($"Prisijunge:{useris.Text}");
            return this;
        }
        public CVOnlinePagrindinisPage Atsijungimas()
        {
            AtsijungimoMygtukas.Click();
            return this;
        }
        public CVOnlinePagrindinisPage PatikrintiSekmingoAtsijungimoRezultata()
        {
            Assert.That(PrisijungimoMygtukas.Displayed, "Prisijungimo mygtuko nerasta");
            Console.WriteLine($"Atsirado mygtukas:{PrisijungimoMygtukas.Text}");
            return this;
        }
        public DarboPaieskosPage DarboPaieskosPuslapioAtidarymas()
        {
            VisiSkelbimaiMygtukas.Click();
            return new DarboPaieskosPage(Driver);
        }
        private static IWebElement KlientamsMygtukoElementas => Driver.FindElement(By.XPath("//*[@id='header-tabs']/ul/li[2]/a/span"));
        public CVOnlinePagrindinisPage GoKlientuPage()
        {
            KlientamsMygtukoElementas.Click();
            return this;
        }
    }

}
