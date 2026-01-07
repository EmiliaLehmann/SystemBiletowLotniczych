using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    public enum EnumKlasa {Ekonomiczna, Biznesowa, Pierwsza};
    public abstract class Bilet
    {
        private double cena;
        private string numerBiletu;
        private string imiePasazera;
        private string nazwiskoPasazera;
        private DateTime DataWylotu;
        private TimeOnly godzinaWylotu;
        private  EnumKlasa klasa;
        private DateTime dataRezerwacji = DateTime.Now;
        private string MiastoWylotu= "Kraków";
        private string MiastoPrzylotu;
        private int IdBiletu;


        public string NumerBiletu { get => numerBiletu; set => numerBiletu = value; }
        public string ImiePasazera { get => imiePasazera; set => imiePasazera = value; }
        public string NazwiskoPasazera { get => nazwiskoPasazera; set => nazwiskoPasazera = value; }
        public DateTime DataWylotu1 { get => DataWylotu; set
            {
                if (value < DateTime.Now)
                    throw new BlednaDataLotuException("Data lotu musi być w przyszłości!");
                DataWylotu = value;
            }
        }
        public TimeOnly GodzinaWylotu { get => godzinaWylotu; set => godzinaWylotu = value; }
        public double Cena { get => cena; set => cena = value; }
        public EnumKlasa Klasa { get => klasa; set => klasa = value; }
        public DateTime DataRezerwacji { get => dataRezerwacji; set => dataRezerwacji = value; }
        public string MiastoWylotu1 { get => MiastoWylotu; set => MiastoWylotu = value; }
        public string MiastoPrzylotu1 { get => MiastoPrzylotu; set => MiastoPrzylotu = value; }
        public int IdBiletu1 { get => IdBiletu; set => IdBiletu = value; }

        public Bilet()
        {
            NumerBiletu = "000000";
            ImiePasazera = string.Empty;
            NazwiskoPasazera = string.Empty;
            Cena = 0;           
            GodzinaWylotu = TimeOnly.FromDateTime(DateTime.Now);
            Klasa = EnumKlasa.Ekonomiczna;
            DataRezerwacji = DateTime.Now;
            MiastoPrzylotu = string.Empty;
            IdBiletu = 0;

        }

       public Bilet(string numerBiletu, string imiePasazera, string nazwiskoPasazera, double cena, DateTime dataWylotu, TimeOnly godzinaWylotu, EnumKlasa klasa, string miastoPrzylotu)
        {
            NumerBiletu = GenerujNumer();
            ImiePasazera = imiePasazera;
            NazwiskoPasazera = nazwiskoPasazera;
            Cena = cena;          
            DataWylotu = dataWylotu;
            GodzinaWylotu = godzinaWylotu;
            Klasa = klasa;
            MiastoPrzylotu = miastoPrzylotu;
            IdBiletu++;
        }

        private string GenerujNumer()
        {
            string numer = string.Empty;
            numer = $"{MiastoPrzylotu.Substring(0,2)}-{Klasa.ToString().Substring(0, 3).ToUpper()}-{DataWylotu:dd - MM - yyyy}-{IdBiletu.ToString("D3")}";
            return numer;
        }


        public virtual double MnoznikSezonowy()
        {
            double mnoznik = 1.0;
            int miesiac = DataWylotu.Month;
            int dzien = DataWylotu.Day;
            if (miesiac == 7 || miesiac == 8) mnoznik = 1.5;
            else if (miesiac == 12)
            {
                if (dzien >= 23 && dzien <= 26) mnoznik = 1.8; 
                else if (dzien == 22 || dzien == 27) mnoznik = 1.3; 
                else if (dzien == 31) mnoznik = 1.6; 
            }

            return mnoznik;
        }

        public virtual double MnoznikKlasy()
        {
            return klasa switch
            {
                EnumKlasa.Ekonomiczna => 1.0,
                EnumKlasa.Biznesowa => 1.5,
                EnumKlasa.Pierwsza => 2.0,
                _ => 1.0,
            };
        }

        public virtual double ObliczCeneKoncowa()
        {
            return cena* MnoznikKlasy() * MnoznikSezonowy() ;
        }

        public override string ToString()
        {
            return $"Numer biletu: {NumerBiletu}\n" +
                   $"Imię pasażera: {ImiePasazera}\n" +
                   $"Nazwisko pasażera: {NazwiskoPasazera}\n" +
                   $"Data wylotu: {DataWylotu:dd-MM-yyyy}\n" +
                   $"Godzina wylotu: {GodzinaWylotu}\n" +
                   $"Klasa: {Klasa}\n" +
                   $"Data rezerwacji: {DataRezerwacji:dd-MM-yyyy}\n" +
                   $"Miasto wylotu: {MiastoWylotu}\n" +
                   $"Miasto przylotu: {MiastoPrzylotu}\n" +
                   $"Cena Biletu: {ObliczCeneKoncowa():C}\n";
        }
    }
}
