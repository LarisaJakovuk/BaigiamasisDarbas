using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;

namespace BaigiamasisDarbas.Tests
{
    class CVonlineTest : BaseTest
    {
        [Order(1)]
        [TestCase("Jonas",
            "Jonaitis",
            "j.jonaitis44@gmail.com",
            "866655534",
            "Informacinės technologijos",
            false,
            true,
            true,
            TestName = "Visi registravimo laukai uzpildyti")]

        public static void RegistracijaDarboPaieskai(string vardas, string pavarde, string email, string tel, string darboTipas, bool spam, bool sutikimas, bool infodarbdaviui)
        {
            _cvonlinePagrindinisPage
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
           .PatikrintiSekmingaRegistravima();
        }
        [Order(2)]

        [TestCase
            ("Administravimas / Sekretoriavimas",
            TestName = "Administravimas_Sekretoriavimas")]
        [TestCase
            ("Bankai / Draudimas,Energetika ,Farmacija ",
             TestName = "Bankai_Energetika_Farmacija")]

        [TestCase
            ("Bankai / Draudimas,Gamyba / Pramonė,Informacinės technologijos",
             TestName = "Gamyba_Bankai_Draudimas_Informacinės technologijos")]
        public static void DarboPaieskosRezultatoPatikrinimas(string darbuFiltras)
        {
            // List<string> darbuSarasas = darbuFiltras.Split(',').ToList();

            _cvonlinePagrindinisPage
           .PasirinktiDarbaPagalFiltra(darbuFiltras)
           .PatikrintiAtrinktuDarbuSkaciuRezultateZemiauMygtukoIeskoti(_cvonlinePagrindinisPage.pasirinktuDarbuSkaicius);

        }

        [Test]
        public void PrisijungimoAtsijungimoPatikrinimas()
        {
            _cvonlinePagrindinisPage
                .PaspaustiPrisijungti()
                .SuvestiPrisijungimoDuomenis("larisa.jakovuk@vpsc.lt", "Caule20*20")
                .PaspaustiZaliaMygtukaPrisijungti()
                .PatikrintiSekmingoPrisijungimoRezultata()
                .Atsijungimas()
                .PatikrintiSekmingoAtsijungimoRezultata()
            ;
        }
        [Test]
        public void DarboPaieska()
        {
            _cvonlinePagrindinisPage.DarboPaieskosPuslapioAtidarymas();
            _darboPaieskosPage
                .IvestiNorimaAtlyginimoNuo("1600")
                .PasirinktiDarboTypa("Informacinės technologijos")
                .PasirinktiMiesta("Vilnius")
                .PasirinktiPerioda("24 valandos")
                .PatikrintiPaieskosFiltruIkelima(4)
                .PasalintiFiltrus()
                ;

               

        }

        // KlientuPaslauguPaketuKainuTestas
        
            [TestCase(1, 1, 229)]
            [TestCase(1, 2, 329)]
            [TestCase(1, 3, 399)]
            [TestCase(2, 1, 329)]
            [TestCase(2, 2, 389)]
            [TestCase(2, 3, 450)]


            public void KainuPatikrinimasPagalPasirinktusKriterijus(int periodas, int cvbazesdydis, int kaina)
            {
                _cvonlinePagrindinisPage.GoKlientuPage();
                _klientuPaslauguPaketuKainosPage
                    .PazymetiPerioda(periodas)
                    .PazymetiPasirinktaCVBazesDydi(cvbazesdydis)
                    .PatikrintiKaina(kaina)
                    ;

            }
        

    }
}
