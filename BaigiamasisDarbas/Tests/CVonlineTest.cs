using BaigiamasisDarbas.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaigiamasisDarbas.Tests
{
    class CVonlineTest : BaseTest
    {
        [Test]
        public static void PirmasTestas()
        {
            _cvonlinePage
           .OpenCvonlinePage()
           .WaitUntilOpenPopUpMailerlite()
           .AddAdvertisingConsentCookies();
             // .ReklamosUzdarymas()
        }
        


    }
}
