using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaigiamasisDarbas.Tools
{
    public static class SliderioStumimoTool
    {
        public static void SliderioStumimas(IWebDriver webdriver, IWebElement element, int atlyginimas, int maximumas, int minimumas)
        {
            int PixelsToMove = GetPixelsToMove(element, atlyginimas, maximumas, minimumas);
            Actions SliderAction = new Actions(webdriver);
            SliderAction.ClickAndHold(element)
                .MoveByOffset((-(int)element.Size.Width / 2), 0)
                .MoveByOffset(PixelsToMove, 0).Release().Perform();
        }
        public static int GetPixelsToMove(IWebElement Slider, decimal Amount, decimal SliderMax, decimal SliderMin)
        {
            int pixels = 0;
            decimal tempPixels = Slider.Size.Width;
            tempPixels = tempPixels / (SliderMax - SliderMin);
            tempPixels = tempPixels * (Amount - SliderMin);
            pixels = Convert.ToInt32(tempPixels);
            return pixels;
        }
    }
}
