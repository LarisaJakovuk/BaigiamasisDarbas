using System;
using System.IO;
using System.Reflection;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;

namespace BaigiamasisDarbas.Tools
{
    public static class ErrorScreenShot
    {
        public static void MakePhoto(IWebDriver webdriver)
        {
          Screenshot errorScreenShot = webdriver.TakeScreenshot();

            
            string screenShotDirectory =
                Path.GetDirectoryName(
                    Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));

            string sceenShotFolder = Path.Combine(screenShotDirectory, "screenshots");

         Directory.CreateDirectory(sceenShotFolder);

            string screenShotName = $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:HH-mm}.png";

          string screenshotPath = Path.Combine(sceenShotFolder, screenShotName);

            errorScreenShot.SaveAsFile(screenshotPath, ScreenshotImageFormat.Png);

            Console.WriteLine($"Issisaugojom {screenshotPath}");
        }
    }
}
