using System;
using System.Collections.Generic;
using System.Text;

namespace SystemBiletowLotniczych
{
    public enum EnumKlasa {Ekonomiczna, Ekonomiczna_Premium, Biznesowa, Pierwsza_Klasa};
    public abstract class Bilet
    {
        private double cena;
        private string numerBiletu;
        private string imiePasazera;
        private string nazwiskoPasazera;
        private DateTime DataWylotu;
        private TimeOnly godzinaWylotu;
        private  EnumKlasa klasa;
        private DateTime dataRezerwacji;


        public string NumerBiletu { get => numerBiletu; set => numerBiletu = value; }
        public string ImiePasazera { get => imiePasazera; set => imiePasazera = value; }
        public string NazwiskoPasazera { get => nazwiskoPasazera; set => nazwiskoPasazera = value; }
        public DateTime DataWylotu1 { get => DataWylotu; set => DataWylotu = value; }
        public TimeOnly GodzinaWylotu { get => godzinaWylotu; set => godzinaWylotu = value; }
        public double Cena { get => cena; set => cena = value; }
        public EnumKlasa Klasa { get => klasa; set => klasa = value; }
        public DateTime DataRezerwacji { get => dataRezerwacji; set => dataRezerwacji = value; }

        public Bilet()
        {
            NumerBiletu = "000000";
            ImiePasazera = string.Empty;
            NazwiskoPasazera = string.Empty;
            Cena = 0;          
            DataWylotu = DateTime.Now;
            GodzinaWylotu = TimeOnly.FromDateTime(DateTime.Now);
        }

       


        public virtual double PobierzMnoznikSezonowy()
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

      
        public double ObliczCeneKoncowa()
        {
            return cena * PobierzMnoznikSezonowy();
        }
    }
}
