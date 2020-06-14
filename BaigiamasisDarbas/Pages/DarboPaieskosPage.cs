using BaigiamasisDarbas.Tools;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaigiamasisDarbas.Pages
{
    public class DarboPaieskosPage : BasePage
    {
        private static IWebElement AtlyginimoSlideInput => Driver.FindElement(By.XPath("//*[@id='content-MinSalary']/div/output/small"));
        
        private static IWebElement PeriodoIkelimoElementas => Driver.FindElement(By.Id("label-Timeline"));
        private static IWebElement DarboTypoPasirinkimoLinkElement => Driver.FindElement(By.Id("label-Tegvk"));
        private static IWebElement MiestoLinkElement => Driver.FindElement(By.Id("label-Town"));
        private static IWebElement PeriodoElement => Driver.FindElement(By.XPath("//*[@id='content-Timeline']/div/div/label"));
        private static IWebElement RastuDarbuKiekisElementas => Driver.FindElement(By.XPath("//*[@id='cvpage - content']/div/h1/a"));
        
        private static IReadOnlyCollection<IWebElement> SkelbimuSarasas => Driver.FindElements(By.ClassName("cvo_module_offer_content"));
        private static IWebElement DarboTypoZymejimoElementas => Driver.FindElement(By.XPath("//*[@id='content-Tegvk']/div/div/label/a"));
        private static IWebElement FilntruPasalinimoElementas => Driver.FindElement(By.XPath("//*[@id='remove-tag']/div/a"));
            

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

            SliderioStumimoTool.SliderioStumimas(Driver,Slider, atlyginimas, 3000, 0);
            return this;
        }
        public DarboPaieskosPage PasirinktiDarboTypa(string pasirinktasDarbas)
        {
            DarboTypoPasirinkimoLinkElement.Click();
            IReadOnlyCollection<IWebElement> DarboTypuSarasas = Driver.FindElements(By.XPath("//*[@id='content-Tegvk']/div/div/label/a"));
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
            IReadOnlyCollection<IWebElement> MiestuSarasas = Driver.FindElements(By.XPath("//*[@id='content-Town']/div/div/label/a"));
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
            IReadOnlyCollection<IWebElement> Periodai = Driver.FindElements(By.XPath("//*[@id='content-Timeline']/div/div/label/a"));
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
            int filtruSkaicius = Filtrai.Count();
            Console.WriteLine($"Paieska pagal {filtruSkaicius} kriterujus");
            Assert.IsTrue(filtruSkaicius == kriterijuSkaicius, $"Buvo suvesta {kriterijuSkaicius} o rodo {filtruSkaicius} paieskos filtru");
            
            return this;
        }
        public DarboPaieskosPage PasalintiFiltrus()
        {
            FilntruPasalinimoElementas.Click();
            return this;
        }


        //public void SliderioStumimas(IWebElement element,int atlyginimas, int maximumas,int minimumas)
        //{
        //    int PixelsToMove = GetPixelsToMove(element, atlyginimas, maximumas, minimumas);
        //    Actions SliderAction = new Actions(Driver);
        //    SliderAction.ClickAndHold(element)
        //        .MoveByOffset((-(int)element.Size.Width / 2), 0)
        //        .MoveByOffset(PixelsToMove, 0).Release().Perform();
        //}
        //public static int GetPixelsToMove(IWebElement Slider, decimal Amount, decimal SliderMax, decimal SliderMin)
        //{
        //    int pixels = 0;
        //    decimal tempPixels = Slider.Size.Width;
        //    tempPixels = tempPixels / (SliderMax - SliderMin);
        //    tempPixels = tempPixels * (Amount - SliderMin);
        //    pixels = Convert.ToInt32(tempPixels);
        //    return pixels;
        //}


    }
}
