using BaigiamasisDarbas.Enums;
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
        [TestCase ("Jonas","Jonaitis","j.jonaitis44@gmail.com","866655534", "Informacinės technologijos", true,true,false,TestName ="Visi registravimo laukai uzpildyti")]
        public static void RegistracijaDarboPaieskai(string vardas, string pavarde, string email,string tel,string darboTipas,bool spam,bool sutikimas,bool infodarbdaviui)
        {

            _cvonlinePage
           .OpenCvonlinePage()
           .WaitUntilOpenPopUpMailerlite()
           .AddAdvertisingConsentCookies()
           .PaspaustiMygtukaRegistruotis()
           .WaitUntilOpenRegModal()
           .IvestiVarda(vardas)
           .IvestiPavarde(pavarde)
           .IvestiElpastoAdresa(email)
           .IvestiTelefonoNr(tel)
           .SelectDarboKategorija(darboTipas)
           .CheckSendSpam(spam)
          .CheckSutikimas(sutikimas)
           .CheckInfoDarbdaviui(infodarbdaviui)
           .PaspaustiSubmit()
           .PatikrintiArAtsidaroSekmingoRegistravimoLangas();        
        }



        //Panaudotas testo kodas iš SELENIUM IDE
        [Test]
        public void registravimas()
        {
            // Test name: registravimas
            // Step # | name | target | value
            // 1 | open | / | 
            _cvonlinePage.OpenCvonlinePage()
             .WaitUntilOpenPopUpMailerlite()
           .AddAdvertisingConsentCookies()
           .PaspaustiMygtukaRegistruotis()
           .WaitUntilOpenRegModal();

            // 4 | click | id=first_name | 
            _driver.FindElement(By.Id("first_name")).Click();
            // 5 | type | id=first_name | Larisa
            _driver.FindElement(By.Id("first_name")).SendKeys("Larisa");
            // 6 | click | id=last_name | 
            _driver.FindElement(By.Id("last_name")).Click();
            // 7 | type | id=last_name | J
            _driver.FindElement(By.Id("last_name")).SendKeys("J");
            // 8 | click | id=desc_seeker_epost | 
            _driver.FindElement(By.Id("desc_seeker_epost")).Click();
            // 9 | type | id=desc_seeker_epost | larisa.jakovuk@vpsc.lt
            _driver.FindElement(By.Id("desc_seeker_epost")).SendKeys("larisa.jakovuk@vpsc.lt");
            // 10 | click | id=desc_contact_tel | 
            _driver.FindElement(By.Id("desc_contact_tel")).Click();
            // 11 | type | id=desc_contact_tel | 866622233
            _driver.FindElement(By.Id("desc_contact_tel")).SendKeys("866622233");
            // 12 | click | id=tegvk_id | 
            _driver.FindElement(By.Id("tegvk_id")).Click();
            // 13 | select | id=tegvk_id | label=Informacinės technologijos
            {
                var dropdown = _driver.FindElement(By.Id("tegvk_id"));
                dropdown.FindElement(By.XPath("//option[. = 'Informacinės technologijos']")).Click();
            }
            // 14 | click | id=tegvk_id | 
            _driver.FindElement(By.Id("tegvk_id")).Click();
            // 15 | click | css=.form-group:nth-child(6) .input-check-outer > .text-small | 
            _driver.FindElement(By.CssSelector(".form-group:nth-child(6) .input-check-outer > .text-small")).Click();
            // 16 | click | css=.form-group:nth-child(7) .input-check-outer > .text-small | 
            _driver.FindElement(By.CssSelector(".form-group:nth-child(7) .input-check-outer > .text-small")).Click();
            // 17 | click | css=.form-group:nth-child(8) .input-check-outer > .text-small | 
            _driver.FindElement(By.CssSelector(".form-group:nth-child(8) .input-check-outer > .text-small")).Click();
            // 18 | click | css=.form-group:nth-child(9) .desktop-only | 
            _driver.FindElement(By.CssSelector(".form-group:nth-child(9) .desktop-only")).Click();
            // 19 | click | css=.remove-margin-top | 
            _driver.FindElement(By.CssSelector(".remove-margin-top")).Click();
            // 20 | assertText | css=.remove-margin-top | Dėkui!
            Assert.That(_driver.FindElement(By.CssSelector(".remove-margin-top")).Text, Is.EqualTo("Dėkui!"));
        }


    }
}
