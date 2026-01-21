using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SystemBiletowLotniczych;

namespace Bilet.Tests
{
    // bilet krajowy
    [TestClass]
    public class BiletKrajowyTests
    {
        [TestMethod]
        public void Konstruktor_Domyślny_UstawiaStawkePodatkowa()
        {
            var bilet = new BiletKrajowy();
            Assert.AreEqual(0.08, bilet.StawkaPodatkowa);
        }

        [TestMethod]
        public void ObliczCeneKoncowa_ZawieraPodatek()
        {
            // Arrange
            var data = DateTime.Now.AddDays(10);
            var godzina = TimeOnly.FromDateTime(DateTime.Now);
            var bilet = new BiletKrajowy("Jan", "Kowalski", 100, data, godzina, EnumKlasa.Ekonomiczna, DateTime.Now, "Warszawa", "Kraków");

            // Act
            double cenaKoncowa = bilet.ObliczCeneKoncowa();

            // Cena bazowa: 100 * mnożnik klasy (1.0) * mnożnik sezonowy (1.0) = 100
            // Cena z podatkiem: 100 * (1 + 0.08) = 108
            Assert.AreEqual(108, cenaKoncowa, 0.01);
        }

        [TestMethod]
        public void ObliczCeneKoncowa_BiznesowaWSezonieLetnim_PodatekUwzgledniony()
        {
            var data = new DateTime(DateTime.Now.Year, 7, 15); // lipiec
            var godzina = new TimeOnly(12, 0);
            var bilet = new BiletKrajowy("Anna", "Nowak", 200, data, godzina, EnumKlasa.Biznesowa, DateTime.Now, "Gdańsk", "Poznań");

            double cenaBazowa = 200 * 1.5 * 1.5; // cena * mnoznik klasy * mnoznik sezonowy
            double oczekiwana = cenaBazowa * 1.08; // podatek

            Assert.AreEqual(oczekiwana, bilet.ObliczCeneKoncowa(), 0.01);
        }


        [TestMethod]
        public void StawkaPodatkowa_MoznaZmienić()
        {
            var bilet = new BiletKrajowy();
            bilet.StawkaPodatkowa = 0.15;
            Assert.AreEqual(0.15, bilet.StawkaPodatkowa);
        }

        [TestMethod]
        public void ToString_ZawieraInformacjeOPodatku()
        {
            var data = DateTime.Now.AddDays(10);
            var godzina = TimeOnly.FromDateTime(DateTime.Now);
            var bilet = new BiletKrajowy("Jan", "Kowalski", 100, data, godzina, EnumKlasa.Ekonomiczna, DateTime.Now, "Warszawa", "Kraków");

            string opis = bilet.ToString();
            StringAssert.Contains(opis, "StawkaPodatkowa");
        }


    }

    // bilet miedzykontynentlany
    [TestClass]
    public class BiletMiedzykontynentalnyTests
    {
        [TestMethod]
        public void Konstruktor_Domyślny_WizaFalse()
        {
            var bilet = new BiletMiedzykontynentalny();
            Assert.IsFalse(bilet.WizaWymagana);
            Assert.AreEqual(0, bilet.CenaUslugDodatkowych);
        }

        [TestMethod]
        public void ObliczCeneKoncowa_WizaDodatkowe()
        {
            var data = DateTime.Now.AddDays(10);
            var godzina = TimeOnly.FromDateTime(DateTime.Now);
            var bilet = new BiletMiedzykontynentalny(
                "Anna", "Nowak", 300, data, godzina, EnumKlasa.Ekonomiczna,
                "NowyJork", "Warszawa", true, 0);

            // baza = 300 * mnoznik klasy * mnoznik sezonowy
            double cenaBazowa = 300 * 1.0 * 1.0;
            double oczekiwana = cenaBazowa + 200; // wiza + brak dodatkowych usług

            Assert.AreEqual(oczekiwana, bilet.ObliczCeneKoncowa(), 0.01);
        }

        [TestMethod]
        public void DodajPosilek_ZwiekszaCenaUslug()
        {
            var bilet = new BiletMiedzykontynentalny();
            bilet.DodajPosilek("Obiad");
            Assert.AreEqual(50, bilet.CenaUslugDodatkowych);

            bilet.DodajPosilek("Kolacja");
            Assert.AreEqual(100, bilet.CenaUslugDodatkowych);
        }

        [TestMethod]
        public void ToString_ZawieraWizeIPosilki()
        {
            var data = DateTime.Now.AddDays(10);
            var godzina = TimeOnly.FromDateTime(DateTime.Now);
            var bilet = new BiletMiedzykontynentalny(
                "Jan", "Kowalski", 200, data, godzina, EnumKlasa.Ekonomiczna,
                "Londyn", "Warszawa", true, 0);
            bilet.DodajPosilek("Śniadanie");

            string opis = bilet.ToString();
            StringAssert.Contains(opis, "Wiza wymagana: TAK");
            StringAssert.Contains(opis, "Cena usług dodatkowych: 50,00");
        }
    }

    //bilet miedzykrajowy
    [TestClass]
        public class BiletMiedzykrajowyTests
        {
            [TestMethod]
            public void Konstruktor_Domyślny_UstawiaDodatkoweOplaty()
            {
                var bilet = new BiletMiedzykrajowy();
                Assert.AreEqual(50, bilet.DodatkoweOplaty);
            }

            [TestMethod]
            public void ObliczCeneKoncowa_DodajeDodatkoweOplaty()
            {
                var data = DateTime.Now.AddDays(5);
                var godzina = TimeOnly.FromDateTime(DateTime.Now);
                var bilet = new BiletMiedzykrajowy("Marta", "Kowalska", 200, data, godzina, EnumKlasa.Ekonomiczna, "Berlin", "Warszawa");

                // Bazowa cena = 200 * mnoznik klasy * mnoznik sezonowy
                double cenaBazowa = 200 * 1.0 * 1.0;
                double oczekiwana = cenaBazowa + 50; // + dodatkowe opłaty

                Assert.AreEqual(oczekiwana, bilet.ObliczCeneKoncowa(), 0.01);
            }

            [TestMethod]
            public void ToString_ZawieraDodatkowaOplate()
            {
                var data = DateTime.Now.AddDays(5);
                var godzina = TimeOnly.FromDateTime(DateTime.Now);
                var bilet = new BiletMiedzykrajowy("Marta", "Kowalska", 200, data, godzina, EnumKlasa.Ekonomiczna, "Berlin", "Warszawa");

                string opis = bilet.ToString();
                StringAssert.Contains(opis, "Dodatkowa opłata");
                StringAssert.Contains(opis, "50"); // sprawdzamy, że 50 jest w stringu
            }

            [TestMethod]
            public void DodatkoweOplaty_MoznaZmienić()
            {
                var bilet = new BiletMiedzykrajowy();
                bilet.DodatkoweOplaty = 80;
                Assert.AreEqual(80, bilet.DodatkoweOplaty);
            }
        }
    //bilet comparer
    [TestClass]
    public class BiletPoDacieComparerTests
    {
        [TestMethod]
        public void Compare_RozneDaty_ZwracaPoprawnyWynik()
        {
            var data1 = DateTime.Now.AddDays(5);
            var data2 = DateTime.Now.AddDays(10);

            var bilet1 = new BiletKrajowy("Jan", "Kowalski", 100, data1, TimeOnly.FromDateTime(DateTime.Now), EnumKlasa.Ekonomiczna, DateTime.Now, "Warszawa", "Kraków");
            var bilet2 = new BiletKrajowy("Anna", "Nowak", 100, data2, TimeOnly.FromDateTime(DateTime.Now), EnumKlasa.Ekonomiczna, DateTime.Now, "Warszawa", "Kraków");

            var comparer = new BiletPoDacieComparer();

            int wynik = comparer.Compare(bilet1, bilet2);
            Assert.IsTrue(wynik < 0); // bilet1 ma wcześniejszą datę
        }

        [TestMethod]
        public void Compare_TakaSamaData_ZwracaZero()
        {
            var data = DateTime.Now.AddDays(5);

            var bilet1 = new BiletKrajowy("Jan", "Kowalski", 100, data, TimeOnly.FromDateTime(DateTime.Now), EnumKlasa.Ekonomiczna, DateTime.Now, "Warszawa", "Kraków");
            var bilet2 = new BiletKrajowy("Anna", "Nowak", 100, data, TimeOnly.FromDateTime(DateTime.Now), EnumKlasa.Ekonomiczna, DateTime.Now, "Warszawa", "Kraków");

            var comparer = new BiletPoDacieComparer();

            int wynik = comparer.Compare(bilet1, bilet2);
            Assert.AreEqual(0, wynik);
        }

        [TestMethod]
        public void Compare_NullBilet_ZwracaOdpowiedniWynik()
        {
            var data = DateTime.Now.AddDays(5);
            var bilet = new BiletKrajowy("Jan", "Kowalski", 100, data, TimeOnly.FromDateTime(DateTime.Now), EnumKlasa.Ekonomiczna, DateTime.Now, "Warszawa", "Kraków");

            var comparer = new BiletPoDacieComparer();

            Assert.AreEqual(-1, comparer.Compare(null, bilet)); // null jest mniejsze
            Assert.AreEqual(1, comparer.Compare(bilet, null));  // bilet większy od null
            Assert.AreEqual(0, comparer.Compare(null, null));   // oba null -> 0
        }
    }

    // exceptions

    [TestClass]
    public class BlednaDataLotuExceptionTests
    {
        [TestMethod]
        [ExpectedException(typeof(BlednaDataLotuException))]
        public void UstawienieDatyWPrzeszlosci_RzucaWyjatek()
        {
            var dataPrzeszla = DateTime.Now.AddDays(-5);

            var bilet = new BiletKrajowy();
            bilet.DataWylotu = dataPrzeszla;
        }
    }

    [TestClass]
    public class BledneMiastoExceptionTests
    {
        [TestMethod]
        [ExpectedException(typeof(BledneMiastoException))]
        public void UstawienieMiastaZaKrotkiego_RzucaWyjatek()
        {
            var bilet = new BiletKrajowy();

            // Miasto wylotu za krótkie
            bilet.MiastoWylotu = "AB";
        }

        [TestMethod]
        [ExpectedException(typeof(BledneMiastoException))]
        public void UstawienieMiastaPrzylotuZaKrotkiego_RzucaWyjatek()
        {
            var bilet = new BiletKrajowy();

            // Miasto przylotu za krótkie
            bilet.MiastoPrzylotu = "XY";
        }
    }

    [TestClass]
    public class BrakMiejscExceptionTests
    {
        [TestMethod]
        [ExpectedException(typeof(BrakMiejscException))]
        public void UtworzenieBiletu_PowyzejMaksymalnejLiczbyMiejsc_RzucaWyjatek()
        {
            var data = DateTime.Now.AddDays(10);
            var godzina = TimeOnly.FromDateTime(DateTime.Now);

            // 180 biletów na ten sam lot
            for (int i = 0; i < 180; i++)
            {
                var bilet = new BiletKrajowy(
                    $"Imie{i}", $"Nazwisko{i}", 100, data, godzina, EnumKlasa.Ekonomiczna,
                    DateTime.Now, "Warszawa", "Kraków");
            }

            // 181 bilet powinien wyrzucić wyjątek
            var ostatniBilet = new BiletKrajowy(
                "Jan", "Kowalski", 100, data, godzina, EnumKlasa.Ekonomiczna,
                DateTime.Now, "Warszawa", "Kraków");
        }
    }

    // interfejs
    [TestClass]
    public class IUslugowyTests
    {
        [TestMethod]
        public void DodajPosilek_ZwiekszaCeneUslugDodatkowych()
        {
            var data = DateTime.Now.AddDays(10);
            var godzina = TimeOnly.FromDateTime(DateTime.Now);

            var bilet = new BiletMiedzykontynentalny(
                "Jan", "Kowalski", 100, data, godzina, EnumKlasa.Ekonomiczna,
                "Nowy Jork", "Warszawa", false, 0);

            Assert.AreEqual(0, bilet.CenaUslugDodatkowych);

            bilet.DodajPosilek("Obiad");

            Assert.AreEqual(50, bilet.CenaUslugDodatkowych);

            bilet.DodajPosilek("Kolacja");

            Assert.AreEqual(100, bilet.CenaUslugDodatkowych);
        }

        [TestMethod]
        public void WyswietlUslugi_WypisujePosilki()
        {
            var data = DateTime.Now.AddDays(10);
            var godzina = TimeOnly.FromDateTime(DateTime.Now);

            var bilet = new BiletMiedzykontynentalny(
                "Anna", "Nowak", 100, data, godzina, EnumKlasa.Ekonomiczna,
                "Nowy Jork", "Warszawa", false, 0);

            bilet.DodajPosilek("Śniadanie");
            bilet.DodajPosilek("Obiad");

            Assert.AreEqual(100, bilet.CenaUslugDodatkowych);
        }
    }
}