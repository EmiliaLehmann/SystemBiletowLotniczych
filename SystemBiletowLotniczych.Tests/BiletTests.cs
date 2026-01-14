using Microsoft.VisualStudio.TestTools.UnitTesting;
using SystemBiletowLotniczych;
using System;

namespace SystemBiletowLotniczychTests
{
    [TestClass]
    public class BiletTests
    {
        [TestMethod]
        public void UstawienieWlasciwosci_Bilet_Poprawnie()
        {
            // Arrange
            var dataWylotu = DateTime.Now.AddDays(1); // przyszlosc
            var godzinaWylotu = new TimeOnly(12, 30);

            var bilet = new BiletKrajowy();

            // Act
            bilet.ImiePasazera = "Jan";
            bilet.NazwiskoPasazera = "Kowalski";
            bilet.Cena = 100;
            bilet.DataWylotu1 = dataWylotu;
            bilet.GodzinaWylotu = godzinaWylotu;
            bilet.MiastoWylotu1 = "Warszawa";
            bilet.MiastoPrzylotu1 = "Kraków";

            // Assert
            Assert.AreEqual("Jan", bilet.ImiePasazera);
            Assert.AreEqual("Kowalski", bilet.NazwiskoPasazera);
            Assert.AreEqual(100, bilet.Cena);
            Assert.AreEqual(dataWylotu, bilet.DataWylotu1);
            Assert.AreEqual(godzinaWylotu, bilet.GodzinaWylotu);
            Assert.AreEqual("Warszawa", bilet.MiastoWylotu1);
            Assert.AreEqual("Kraków", bilet.MiastoPrzylotu1);
        }

        [TestMethod]
        [ExpectedException(typeof(BlednaDataLotuException))]
        public void Bilet_UstawienieDatyWLosciuWPrzeszlosci_RzucaWyjatek()
        {
            // Arrange
            var bilet = new BiletKrajowy();
            var przeszlaData = DateTime.Now.AddDays(-1);

            // Act
            bilet.DataWylotu1 = przeszlaData;
        }

        [TestMethod]
        public void BiletKrajowy_ObliczCeneKoncowa_Poprawnie()
        {
            // Arrange
            var dataWylotu = new DateTime(2026, 7, 10); // lato, mnożnik sezonowy 1.5
            var godzinaWylotu = new TimeOnly(10, 0);
            var bilet = new BiletKrajowy("Anna", "Nowak", 200, dataWylotu, godzinaWylotu, EnumKlasa.Biznesowa, DateTime.Now, "Kraków", "Warszawa");

            // Act
            double cena = bilet.ObliczCeneKoncowa();

            // Cena = 200 * 1.5 (mnoznik sezonowy) * 1.5 (mnoznik klasy biznesowej) * 1.08 (stawka podatkowa)
            double oczekiwanaCena = 200 * 1.5 * 1.5 * 1.08;

            // Assert
            Assert.AreEqual(oczekiwanaCena, cena, 0.01);
        }

    }
}
