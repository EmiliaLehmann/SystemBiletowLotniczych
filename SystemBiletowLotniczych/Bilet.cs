using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;        //dla [Key]
using System.ComponentModel.DataAnnotations.Schema; //dla [NotMapped]

namespace SystemBiletowLotniczych
{
    public enum EnumKlasa {Ekonomiczna, Biznesowa, Pierwsza};
    [XmlInclude(typeof(BiletKrajowy))]                //do mechanizmu XML - klasa bazowa moze przyjmowac postac konkretnej klasy pochodnej -- i nasza lista ma rozne obiekty 
    [XmlInclude(typeof(BiletMiedzykontynentalny))]
    [XmlInclude(typeof(BiletMiedzykrajowy))]
    [XmlInclude(typeof(BiletZPrzesiadkami))]
    public abstract class Bilet : IComparable<Bilet>, IEquatable<Bilet>, ICloneable
    {
        private double cena;
        private string numerLotu;
        private string imiePasazera;
        private string nazwiskoPasazera;
        private DateTime dataWylotu;
        private TimeOnly godzinaWylotu;
        private  EnumKlasa klasa;
        private DateTime dataRezerwacji = DateTime.Now;
        private string miastoWylotu;
        private string miastoPrzylotu;
        private static Dictionary<string, int> licznikiMiejsc = new Dictionary<string, int>();
        private int numerMiejsca;
        private static int MAX_MIEJSC = 180;

        public static List<Bilet> kupioneBilety = new List<Bilet>();     //mamy polimorfizm wiec lista "zbiera" wszystkie bilety dziedziczace po bilet

        [XmlIgnore]
        [NotMapped]
        public double CenaKoncowa => ObliczCeneKoncowa();

        [XmlIgnore]
        [NotMapped]
        public DateTime DataIGodzinaWylotu
        {
            get => DataWylotu.Date + GodzinaWylotu.ToTimeSpan();
        }




        //entity framework robi kolumne dla kazdej publicznej wlasciwosci ktora ma get i set
        [Key]
        public int BiletId { get; set; }

        public string ImiePasazera { get => imiePasazera; set => imiePasazera = value; }
        public string NazwiskoPasazera { get => nazwiskoPasazera; set => nazwiskoPasazera = value; }
        public DateTime DataWylotu { get => dataWylotu; set
            {
                if (value < DateTime.Now)
                    throw new BlednaDataLotuException("Data lotu musi być w przyszłości!");
                dataWylotu = value;
            }
        }

        [XmlIgnore]    //dajemy to bo XML nie umie ladnie wczytac TimeOnly
        [NotMapped]   // nie uwzgledniamy do BazyDanych
        public TimeOnly GodzinaWylotu { get => godzinaWylotu; set => godzinaWylotu = value; }
        
        [XmlElement("GodzinaWylotu")]   // wlasciwosc bedzie "udawac" nasza godzine
        [NotMapped]
        public string GodzinaWylotu2
        {
            get => godzinaWylotu.ToString("HH:mm");
            set => godzinaWylotu = TimeOnly.Parse(value);  //z powrotem na czas
        }

        public double Cena { get => cena; set => cena = value; }
        public EnumKlasa Klasa { get => klasa; set => klasa = value; }
        public DateTime DataRezerwacji { get => dataRezerwacji; set => dataRezerwacji = value; }                    

        public string MiastoWylotu
        {
            get => miastoWylotu;
            set
            {
                if (value.Length < 3) throw new BledneMiastoException("Miasto ma za krótką nazwę");
                miastoWylotu = value;
            }
        }
        
        public string MiastoPrzylotu
        {
            get => miastoPrzylotu;
            set
            {
                if (value.Length < 3) throw new BledneMiastoException("Miasto ma za krótką nazwę");
                miastoPrzylotu = value;
            }
        }
        

        public string NumerLotu {
            get
            {
                return $"{MiastoWylotu.Substring(0, 3).ToUpper()}-" +
                       $"{MiastoPrzylotu.Substring(0, 3).ToUpper()}-" +
                       $"{DataWylotu:yyyyMMdd}-" +
                       $"{GodzinaWylotu:HHmm}";
            }
            set { }
        }

        public int NumerMiejsca { get => numerMiejsca; set => numerMiejsca = value; }





        public Bilet()
        {
            ImiePasazera = string.Empty;
            NazwiskoPasazera = string.Empty;
            Cena = 0;           
            GodzinaWylotu = TimeOnly.FromDateTime(DateTime.Now);
            Klasa = EnumKlasa.Ekonomiczna;
            DataRezerwacji = DateTime.Now;
            miastoPrzylotu = string.Empty;
            miastoWylotu = "Kraków";

        }

       public Bilet( string imiePasazera, string nazwiskoPasazera, double cena, DateTime dataWylotu, TimeOnly godzinaWylotu, EnumKlasa klasa, string miastoPrzylotu, string miastoWylotu)
        {
            ImiePasazera = imiePasazera;
            NazwiskoPasazera = nazwiskoPasazera;
            Cena = cena;          
            DataWylotu = dataWylotu;
            GodzinaWylotu = godzinaWylotu;
            Klasa = klasa;
            MiastoPrzylotu = miastoPrzylotu;
            MiastoWylotu = miastoWylotu;
            string kluczLotu = this.NumerLotu;

            if (!licznikiMiejsc.ContainsKey(kluczLotu))
            {
                licznikiMiejsc[kluczLotu] = 0;
            }
            if (licznikiMiejsc[kluczLotu] >= MAX_MIEJSC)
            {
                throw new BrakMiejscException($"Błąd: Brak wolnych miejsc na lot {kluczLotu}. Maksymalna liczba miejsc to {MAX_MIEJSC}.");
            }

            licznikiMiejsc[kluczLotu]++;
            numerMiejsca = licznikiMiejsc[kluczLotu];

            doListyBiletow(this);
        }

               public string PelnyNumerBiletu => $"{NumerLotu}-{NumerMiejsca:D3}";




        #region MetodyWirtualne
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

        #endregion MetodyWirtualne

        public override string ToString()
        {
            return $"Numer biletu: {PelnyNumerBiletu}\n" +
             $"===============================================\n" +
             $"Numer lotu: {NumerLotu}\n" +
                   $"Numer miejsca: {NumerMiejsca}\n" +
                   $"Imię pasażera: {ImiePasazera}\n" +
                   $"Nazwisko pasażera: {NazwiskoPasazera}\n" +
                   $"Data wylotu: {DataWylotu:dd-MM-yyyy}\n" +
                   $"Godzina wylotu: {GodzinaWylotu}\n" +
                   $"Klasa: {Klasa}\n" +
                   $"Data rezerwacji: {DataRezerwacji:dd-MM-yyyy}\n" +
                   $"Miasto wylotu: {miastoWylotu}\n" +
                   $"Miasto przylotu: {miastoPrzylotu}\n" +
                   $"Cena Biletu: {ObliczCeneKoncowa():C}\n";
        }

        public int CompareTo(Bilet? other)
        {
            if (other == null)
                return 1;
            return this.ObliczCeneKoncowa().CompareTo(other.ObliczCeneKoncowa());
        }

        public bool Equals(Bilet? other)
        {
            if (other == null) return false;
            return this.PelnyNumerBiletu == other.PelnyNumerBiletu;                      //uwaga Emilia zmieniam NumerLotu na PelnyNumerBiletu
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public Bilet CloneZNowaGodnoscia(string klonImie, string klonNazwisko)
        {
            Bilet klon = (Bilet)this.Clone();   //rzutowanie na bilet bo CLone zwraca object

            klon.ImiePasazera = klonImie;
            klon.NazwiskoPasazera = klonNazwisko;

            string kluczLotu = klon.NumerLotu;

            if (licznikiMiejsc[kluczLotu] >= MAX_MIEJSC)
            {
                throw new BrakMiejscException($"Błąd: Brak wolnych miejsc na lot {kluczLotu}. Maksymalna liczba miejsc to {MAX_MIEJSC}.");
            }

            licznikiMiejsc[kluczLotu]++;
            klon.numerMiejsca = licznikiMiejsc[kluczLotu];

            kupioneBilety.Add(klon);
            return klon;
        }

        public void doListyBiletow(Bilet b)
        {
            kupioneBilety.Add(b);
        }

        public static void wyswietlKupioneBilety()
        {
            Console.WriteLine("Lista sprzedanych biletów:\n===============================================\n");
            if (kupioneBilety.Count == 0)
            {
                Console.WriteLine("Brak sprzedanych biletów");
                return;
            }
            foreach(Bilet b in kupioneBilety)
            {
                Console.WriteLine(b.ToString());
                Console.WriteLine("\n-----------------------------------------------");
            }
        }

        public static void ZapisXML(string nazwa, List<Bilet> kupioneBilety)     //XmlSerializer zapisuje rzeczy ktore sa Publiczna wlasciwoscia co ma Get i Set
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Bilet>));
                StreamWriter sw = new StreamWriter(nazwa);
                serializer.Serialize(sw, kupioneBilety);
                sw.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Błąd przy zapisie: {e.Message}");
            }


            
        }

        public static List<Bilet> OdczytajXML(string nazwa)
        {
            List<Bilet> odczytany = new List<Bilet>();
            try
            {
                TextReader tr = new StreamReader(nazwa);
                XmlSerializer serializer = new XmlSerializer(typeof(List<Bilet>));
                odczytany = (List<Bilet>)serializer.Deserialize(tr);
                tr.Close();

                AktualizacjaZOdczytu(odczytany);

                return odczytany;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Brak pliku");
                return null;
            }
        }

        public static void AktualizacjaZOdczytu(List<Bilet> odczytany)    //dajemy static zeby mozna bylo wywolac nawet bez zadnej instacji biletu w main
        {
            kupioneBilety.Clear();
            licznikiMiejsc.Clear();

            if(odczytany ==  null) {return; }

            foreach(Bilet b in  odczytany)
            {
                kupioneBilety.Add(b);

                string kluczLotu = b.NumerLotu;

                if (!licznikiMiejsc.ContainsKey(kluczLotu))
                {
                    licznikiMiejsc[kluczLotu] = b.NumerMiejsca;            
                }
                else
                {
                    if(b.NumerMiejsca > licznikiMiejsc[kluczLotu])    //jezeli by w slowniku cos juz bylo 
                    {
                        licznikiMiejsc[kluczLotu] = b.NumerMiejsca;
                    }
                }
                if (licznikiMiejsc[kluczLotu] >= MAX_MIEJSC)
                {
                    throw new BrakMiejscException($"Błąd: Brak wolnych miejsc na lot {kluczLotu}. Maksymalna liczba miejsc to {MAX_MIEJSC}.");
                }
            }
        }

        public void SaveToDB()
        {
            using (var db = new BiletDbContext())
            {
                db.Bilets.Add(this);
                db.SaveChanges();
            }
        }



        public delegate double DelegatZnizka(double jakasZnizka);    // przyjmuje double i zwracam double (kazda metoda ktora tu przyjme ma miec taki ksztalt)

        public void ZastosujRabat(DelegatZnizka przyznanieZnizki)   //to przyznanieZnizki to nasza metoda konkretna
        {
            this.Cena = przyznanieZnizki(this.Cena);
        }
                    //np takie byloby wywolanie    produkt.ZastosujRabat(Znizki.Student);
    }

    public static class Znizki           //robie sobie statyczna zeby nie tworzyc obiektu a latwo wziac sobie wzor                             
    {
        public static double Student(double c) => c * 0.5;
        public static double Senior(double c) => c * 0.7;
    }



}
