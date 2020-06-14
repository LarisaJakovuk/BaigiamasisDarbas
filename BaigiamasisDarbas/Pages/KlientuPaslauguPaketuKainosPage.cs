using BaigiamasisDarbas.Tools;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaigiamasisDarbas.Pages
{
    public class KlientuPaslauguPaketuKainosPage : BasePage
    {
        
        private static IWebElement PeriodoZymejimoElementas => Driver.FindElement(By.XPath("//*[@id='2_period']/div/div/div"));

        private static IWebElement CvBazesDydzioPasirinkimas => Driver.FindElement(By.XPath("//*[@id='js-rangeSlider-b58ab3de-3a1f-c158-1da7-532f078e736c']/div[3]"));
        private static IWebElement KainosElementas => Driver.FindElement(By.Id("2_price"));
        public KlientuPaslauguPaketuKainosPage (IWebDriver webdriver) : base(webdriver)
        { 
        }
        public KlientuPaslauguPaketuKainosPage PazymetiPerioda(int periodas)
        {
            SliderioStumimoTool.SliderioStumimas(Driver,PeriodoZymejimoElementas, periodas, 2, 1);
            return this;
        }

        public KlientuPaslauguPaketuKainosPage PazymetiPasirinktaCVBazesDydi(int cvBaziuVariantas)
        {

            SliderioStumimoTool.SliderioStumimas(Driver, CvBazesDydzioPasirinkimas, cvBaziuVariantas, 3,1);
            return this;
            
        }

        public KlientuPaslauguPaketuKainosPage PatikrintiKaina(int expectedKaina)
        {
            int kaina = Convert.ToInt32(KainosElementas.Text);
            Assert.AreEqual(expectedKaina, kaina);
            return this;
        }
    }
}
