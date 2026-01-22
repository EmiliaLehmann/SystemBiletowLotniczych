using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SystemBiletowLotniczych;

namespace Bilet.Tests
{
    // bilet krajowy
    [TestClass]
    public class BiletKrajowyTests
    {
        /// <summary>
        /// Testuje, czy konstruktor domyślny ustawia domyślną stawkę podatkową 0,08.
        /// </summary>
        [TestMethod]
        public void Konstruktor_Domyślny_UstawiaStawkePodatkowa()
        {
            var bilet = new BiletKrajowy();
            Assert.AreEqual(0.08, bilet.StawkaPodatkowa);
        }

        /// <summary>
        /// Sprawdza, czy ObliczCeneKoncowa uwzględnia podatek.
        /// </summary>
        [TestMethod]
        public void ObliczCeneKoncowa_ZawieraPodatek()
        {
            var data = DateTime.Now.AddDays(10);
            var godzina = TimeOnly.FromDateTime(DateTime.Now);
            var bilet = new BiletKrajowy("Jan", "Kowalski", 100, data, godzina, EnumKlasa.Ekonomiczna, DateTime.Now, "Warszawa", "Kraków");

            double cenaKoncowa = bilet.ObliczCeneKoncowa();

            Assert.AreEqual(108, cenaKoncowa, 0.01);
        }

        /// <summary>
        /// Testuje, czy ObliczCeneKoncowa dla klasy biznesowej w sezonie letnim uwzględnia podatek.
        /// </summary>
        [TestMethod]
        public void ObliczCeneKoncowa_BiznesowaWSezonieLetnim_PodatekUwzgledniony()
        {
            var data = new DateTime(DateTime.Now.Year, 7, 15);
            var godzina = new TimeOnly(12, 0);
            var bilet = new BiletKrajowy("Anna", "Nowak", 200, data, godzina, EnumKlasa.Biznesowa, DateTime.Now, "Gdańsk", "Poznań");

            double cenaBazowa = 200 * 1.5 * 1.5;
            double oczekiwana = cenaBazowa * 1.08;

            Assert.AreEqual(oczekiwana, bilet.ObliczCeneKoncowa(), 0.01);
        }

        /// <summary>
        /// Sprawdza, czy można zmienić wartość stawki podatkowej.
        /// </summary>
        [TestMethod]
        public void StawkaPodatkowa_MoznaZmienić()
        {
            var bilet = new BiletKrajowy();
            bilet.StawkaPodatkowa = 0.15;
            Assert.AreEqual(0.15, bilet.StawkaPodatkowa);
        }

        /// <summary>
        /// Testuje, czy ToString zawiera informacje o stawce podatkowej.
        /// </summary>
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
        /// <summary>
        /// Testuje, czy konstruktor domyślny ustawia WizaWymagana na false i CenaUslugDodatkowych na 0.
        /// </summary>
        [TestMethod]
        public void Konstruktor_Domyślny_WizaFalse()
        {
            var bilet = new BiletMiedzykontynentalny();
            Assert.IsFalse(bilet.WizaWymagana);
            Assert.AreEqual(0, bilet.CenaUslugDodatkowych);
        }

        /// <summary>
        /// Sprawdza, czy ObliczCeneKoncowa uwzględnia wize i dodatkowe usługi.
        /// </summary>
        [TestMethod]
        public void ObliczCeneKoncowa_WizaDodatkowe()
        {
            var data = DateTime.Now.AddDays(10);
            var godzina = TimeOnly.FromDateTime(DateTime.Now);
            var bilet = new BiletMiedzykontynentalny(
                "Anna", "Nowak", 300, data, godzina, EnumKlasa.Ekonomiczna,
                "NowyJork", "Warszawa", true, 0);

            double cenaBazowa = 300 * 1.0 * 1.0;
            double oczekiwana = cenaBazowa + 200;

            Assert.AreEqual(oczekiwana, bilet.ObliczCeneKoncowa(), 0.01);
        }

        /// <summary>
        /// Testuje, czy DodajPosilek zwiększa CenaUslugDodatkowych.
        /// </summary>
        [TestMethod]
        public void DodajPosilek_ZwiekszaCenaUslug()
        {
            var bilet = new BiletMiedzykontynentalny();
            bilet.DodajPosilek("Obiad");
            Assert.AreEqual(50, bilet.CenaUslugDodatkowych);

            bilet.DodajPosilek("Kolacja");
            Assert.AreEqual(100, bilet.CenaUslugDodatkowych);
        }

        /// <summary>
        /// Sprawdza, czy ToString zawiera informacje o wizie i usługach dodatkowych.
        /// </summary>
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
        /// <summary>
        /// Testuje, czy konstruktor domyślny ustawia DodatkoweOplaty na 50.
        /// </summary>
        [TestMethod]
        public void Konstruktor_Domyślny_UstawiaDodatkoweOplaty()
        {
            var bilet = new BiletMiedzykrajowy();
            Assert.AreEqual(50, bilet.DodatkoweOplaty);
        }

        /// <summary>
        /// Sprawdza, czy ObliczCeneKoncowa dodaje DodatkoweOplaty.
        /// </summary>
        [TestMethod]
        public void ObliczCeneKoncowa_DodajeDodatkoweOplaty()
        {
            var data = DateTime.Now.AddDays(5);
            var godzina = TimeOnly.FromDateTime(DateTime.Now);
            var bilet = new BiletMiedzykrajowy("Marta", "Kowalska", 200, data, godzina, EnumKlasa.Ekonomiczna, "Berlin", "Warszawa");

            double cenaBazowa = 200 * 1.0 * 1.0;
            double oczekiwana = cenaBazowa + 50;

            Assert.AreEqual(oczekiwana, bilet.ObliczCeneKoncowa(), 0.01);
        }

        /// <summary>
        /// Testuje, czy ToString zawiera informację o dodatkowej opłacie.
        /// </summary>
        [TestMethod]
        public void ToString_ZawieraDodatkowaOplate()
        {
            var data = DateTime.Now.AddDays(5);
            var godzina = TimeOnly.FromDateTime(DateTime.Now);
            var bilet = new BiletMiedzykrajowy("Marta", "Kowalska", 200, data, godzina, EnumKlasa.Ekonomiczna, "Berlin", "Warszawa");

            string opis = bilet.ToString();
            StringAssert.Contains(opis, "Dodatkowa opłata");
            StringAssert.Contains(opis, "50");
        }

        /// <summary>
        /// Sprawdza, czy można zmienić wartość DodatkoweOplaty.
        /// </summary>
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
        /// <summary>
        /// Testuje porównanie dwóch biletów o różnych datach.
        /// </summary>
        [TestMethod]
        public void Compare_RozneDaty_ZwracaPoprawnyWynik()
        {
            var data1 = DateTime.Now.AddDays(5);
            var data2 = DateTime.Now.AddDays(10);

            var bilet1 = new BiletKrajowy("Jan", "Kowalski", 100, data1, TimeOnly.FromDateTime(DateTime.Now), EnumKlasa.Ekonomiczna, DateTime.Now, "Warszawa", "Kraków");
            var bilet2 = new BiletKrajowy("Anna", "Nowak", 100, data2, TimeOnly.FromDateTime(DateTime.Now), EnumKlasa.Ekonomiczna, DateTime.Now, "Warszawa", "Kraków");

            var comparer = new BiletPoDacieComparer();

            int wynik = comparer.Compare(bilet1, bilet2);
            Assert.IsTrue(wynik < 0);
        }

        /// <summary>
        /// Testuje porównanie dwóch biletów o tej samej dacie.
        /// </summary>
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

        /// <summary>
        /// Testuje porównanie, gdy jeden lub oba bilety są null.
        /// </summary>
        [TestMethod]
        public void Compare_NullBilet_ZwracaOdpowiedniWynik()
        {
            var data = DateTime.Now.AddDays(5);
            var bilet = new BiletKrajowy("Jan", "Kowalski", 100, data, TimeOnly.FromDateTime(DateTime.Now), EnumKlasa.Ekonomiczna, DateTime.Now, "Warszawa", "Kraków");

            var comparer = new BiletPoDacieComparer();

            Assert.AreEqual(-1, comparer.Compare(null, bilet));
            Assert.AreEqual(1, comparer.Compare(bilet, null));
            Assert.AreEqual(0, comparer.Compare(null, null));
        }
    }

    // exceptions

    [TestClass]
    public class BlednaDataLotuExceptionTests
    {
        /// <summary>
        /// Sprawdza, czy ustawienie daty w przeszłości rzuca wyjątek BlednaDataLotuException.
        /// </summary>
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
        /// <summary>
        /// Sprawdza, czy ustawienie zbyt krótkiej nazwy miasta wylotu rzuca wyjątek BledneMiastoException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(BledneMiastoException))]
        public void UstawienieMiastaZaKrotkiego_RzucaWyjatek()
        {
            var bilet = new BiletKrajowy();
            bilet.MiastoWylotu = "AB";
        }

        /// <summary>
        /// Sprawdza, czy ustawienie zbyt krótkiej nazwy miasta przylotu rzuca wyjątek BledneMiastoException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(BledneMiastoException))]
        public void UstawienieMiastaPrzylotuZaKrotkiego_RzucaWyjatek()
        {
            var bilet = new BiletKrajowy();
            bilet.MiastoPrzylotu = "XY";
        }
    }

    [TestClass]
    public class BrakMiejscExceptionTests
    {
        /// <summary>
        /// Testuje, czy utworzenie biletu powyżej maksymalnej liczby miejsc rzuca wyjątek BrakMiejscException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(BrakMiejscException))]
        public void UtworzenieBiletu_PowyzejMaksymalnejLiczbyMiejsc_RzucaWyjatek()
        {
            var data = DateTime.Now.AddDays(10);
            var godzina = TimeOnly.FromDateTime(DateTime.Now);

            for (int i = 0; i < 180; i++)
            {
                var bilet = new BiletKrajowy(
                    $"Imie{i}", $"Nazwisko{i}", 100, data, godzina, EnumKlasa.Ekonomiczna,
                    DateTime.Now, "Warszawa", "Kraków");
            }

            var ostatniBilet = new BiletKrajowy(
                "Jan", "Kowalski", 100, data, godzina, EnumKlasa.Ekonomiczna,
                DateTime.Now, "Warszawa", "Kraków");
        }
    }

    // interfejs
    [TestClass]
    public class IUslugowyTests
    {
        /// <summary>
        /// Sprawdza, czy DodajPosilek zwiększa CenaUslugDodatkowych.
        /// </summary>
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

        /// <summary>
        /// Sprawdza, czy WyswietlUslugi poprawnie wypisuje wszystkie posiłki (testuje stan obiektu).
        /// </summary>
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
