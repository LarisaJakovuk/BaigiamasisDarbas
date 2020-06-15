using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace BaigiamasisDarbas.Pages
{
    public class KlientuPaslauguPaketuKainosPage : BasePage
    {
        private string pageAdress = "https://www.cvonline.lt/client/";
        //private static IWebElement PeriodoZymejimoElementas => Driver.FindElement(By.XPath("//*[@id='2_period']/div/div/div"));

        //private static IWebElement CvBazesDydzioPasirinkimas => Driver.FindElement(By.XPath("//*[@id='js-rangeSlider-b58ab3de-3a1f-c158-1da7-532f078e736c']/div[3]"));
        private static IWebElement KainosElementas => Driver.FindElement(By.Id("2_price"));

        private static IWebElement kryziukas => Driver.FindElement(By.XPath("//*[@id='centerbox']/div[1]"));
        private static IWebElement ReklamosLangas => Driver.FindElement(By.Id("inner-border"));
        private static IWebElement PeriodoRodykleAtgal => Driver.FindElement(By.XPath("//body[@id='lt']/div[2]/div/div/div/div/div/div/div/section/div/div[2]/div/div[2]/div/div[2]/div/div[2]/div/div[2]/div/button"));
        private static IWebElement PeriodoRodyklePirmyn => Driver.FindElement(By.XPath("//body[@id='lt']/div[2]/div/div/div/div/div/div/div/section/div/div[2]/div/div[2]/div/div[2]/div/div[2]/div/div[2]/div/button[2]"));

        private static IWebElement CvBazesRodykleAtgal => Driver.FindElement(By.XPath("//body[@id='lt']/div[2]/div/div/div/div/div/div/div/section/div/div[2]/div/div[2]/div/div[2]/div/div[2]/div[2]/div[2]/div/button"));
        private static IWebElement CvBazesRodyklePirmyn => Driver.FindElement(By.XPath("//body[@id='lt']/div[2]/div/div/div/div/div/div/div/section/div/div[2]/div/div[2]/div/div[2]/div/div[2]/div[2]/div[2]/div/button[2]"));
        public KlientuPaslauguPaketuKainosPage(IWebDriver webdriver) : base(webdriver)
        {
        }
        public  KlientuPaslauguPaketuKainosPage OpenKlientuPage()
        {
            if (Driver.Url != pageAdress)
                Driver.Url = pageAdress;
            return this;
        }
        public KlientuPaslauguPaketuKainosPage PazymetiPerioda(int periodas)
        {
            //SliderioStumimoTool.SliderioStumimas(Driver,PeriodoZymejimoElementas, periodas, 2, 1);
            if (periodas == 1)
            {
                if (PeriodoRodykleAtgal.Enabled)
                    PeriodoRodykleAtgal.Click();
            }
            else if (periodas == 2)

            {
                if (PeriodoRodyklePirmyn.Enabled)
                    PeriodoRodyklePirmyn.Click();
            }
        
            return this;
        }


    public KlientuPaslauguPaketuKainosPage PazymetiPasirinktaCVBazesDydi(int cvBaziuVariantas)
    {

        //SliderioStumimoTool.SliderioStumimas(Driver, CvBazesDydzioPasirinkimas, cvBaziuVariantas, 3, 1);
        while (CvBazesRodykleAtgal.Enabled)
        {
            CvBazesRodykleAtgal.Click();
        }
        if (cvBaziuVariantas > 1)
        {
            for (int i = 2; i <= cvBaziuVariantas; i++)
            {
                CvBazesRodyklePirmyn.Click();
            }
        }
        return this;

    }

    public KlientuPaslauguPaketuKainosPage PatikrintiKaina(int expectedKaina)
    {
        int kaina = Convert.ToInt32(KainosElementas.Text);
        Assert.AreEqual(expectedKaina, kaina, $"Gavome kaina {kaina} o turejo buti {expectedKaina}");
        return this;
    }


    public KlientuPaslauguPaketuKainosPage ReklamosUzdarymas()
    {
       // WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(2));
       //wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.Id("inner-border")));
            TimeSpan.FromSeconds(2);
            if (ReklamosLangas.Displayed)
            kryziukas.Click();
        return this;
    }
}

}
