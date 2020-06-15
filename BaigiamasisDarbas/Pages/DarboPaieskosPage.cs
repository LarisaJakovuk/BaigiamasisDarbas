using BaigiamasisDarbas.Tools;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BaigiamasisDarbas.Pages
{
    public class DarboPaieskosPage : BasePage
    {

        private static IWebElement PeriodoIkelimoElementas => Driver.FindElement(By.Id("label-Timeline"));
        private static IWebElement DarboTypoPasirinkimoLinkElement => Driver.FindElement(By.Id("label-Tegvk"));
        private static IWebElement MiestoLinkElement => Driver.FindElement(By.Id("label-Town"));
        private static IWebElement RastuDarbuKiekisElementas => Driver.FindElement(By.XPath("//*[@id='cvpage-content']/div/h1/a"));

        private static IReadOnlyCollection<IWebElement> SkelbimuSarasas => Driver.FindElements(By.ClassName("cvo_module_offer_content"));
        private static IWebElement DarboTypoZymejimoElementas => Driver.FindElement(By.XPath("//*[@id='content-Tegvk']/div/div/label/a"));
        private static IWebElement FilntruPasalinimoElementas => Driver.FindElement(By.XPath("//*[@id='remove-tag']/div/a"));
        private static IReadOnlyCollection<IWebElement> Periodai => Driver.FindElements(By.XPath("//*[@id='content-Timeline']/div/div/label/a"));
        private static IReadOnlyCollection<IWebElement> MiestuSarasas => Driver.FindElements(By.XPath("//*[@id='content-Town']/div/div/label/a"));
        private static IReadOnlyCollection<IWebElement> DarboTypuSarasas => Driver.FindElements(By.XPath("//*[@id='content-Tegvk']/div/div/label/a"));
        private static IWebElement kryziukas => Driver.FindElement(By.XPath("//*[@id='centerbox']/div[1]"));
        private static IWebElement ReklamosLangas => Driver.FindElement(By.Id("inner-border"));
        //Konstruktorius
        public DarboPaieskosPage(IWebDriver webdriver) : base(webdriver)
        {
        }

        public DarboPaieskosPage IvestiNorimaAtlyginimoNuo(string atlyginimasNuo)
        {
            int atlyginimas = Convert.ToInt32(atlyginimasNuo);

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(100));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.XPath("//*[@id='slidecheck']")));
            IWebElement Slider = Driver.FindElement(By.XPath("//*[@id='slidecheck']"));
            SliderioStumimoTool.SliderioStumimas(Driver, Slider, atlyginimas, 3000, 0);
            TimeSpan.FromSeconds(2);
            return this;
        }
        public DarboPaieskosPage PasirinktiDarboTypa(string pasirinktasDarbas)
        {
            DarboTypoPasirinkimoLinkElement.Click();

            foreach (var darbasIsSaraso in DarboTypuSarasas)
            {
                if (darbasIsSaraso.Text.Contains(pasirinktasDarbas))
                {
                    darbasIsSaraso.Click();
                }
            }
            return this;
        }

        public DarboPaieskosPage PasirinktiMiesta(string pasirinktasMiestas)
        {
            MiestoLinkElement.Click();

            foreach (var miestas in MiestuSarasas)
            {
                if (miestas.Text.Contains(pasirinktasMiestas))
                {
                    miestas.Click();
                }
            }
            return this;
        }
        public DarboPaieskosPage PasirinktiPerioda(string pasirinktasPeriodas)
        {
            PeriodoIkelimoElementas.Click();

            foreach (var periodas in Periodai)
            {
                if (periodas.Text.Contains(pasirinktasPeriodas))
                {
                    periodas.Click();
                }
            }
            return this;
        }

        public DarboPaieskosPage PatikrintiPaieskosFiltruIkelima(int kriterijuSkaicius)
        {

            IReadOnlyCollection<IWebElement> Filtrai = Driver.FindElements(By.XPath("//*[@id='keywords']/div"));
            foreach (var filtr in Filtrai)
            {
                string filtroPavadinimas = filtr.Text;
                Console.WriteLine(filtroPavadinimas);
            }
            int filtruSkaicius = Filtrai.Count();
            TimeSpan.FromSeconds(5);
            Console.WriteLine($"Paieska pagal {filtruSkaicius} kriterujus");
            Assert.IsTrue(filtruSkaicius == kriterijuSkaicius, $"Buvo suvesta {kriterijuSkaicius} o rodo {filtruSkaicius} paieskos filtru");

            return this;
        }
        public DarboPaieskosPage PasalintiFiltrus()
        {
            FilntruPasalinimoElementas.Click();
            return this;
        }

        public DarboPaieskosPage ReklamosUzdarymas()
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("inner-border")));
            //TimeSpan.FromSeconds(2);
            if (ReklamosLangas.Displayed)
                kryziukas.Click();
            return this;
        }
    }
}
