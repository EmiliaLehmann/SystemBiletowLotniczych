using System;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using SystemBiletowLotniczych;

namespace ProjektGUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl-PL");

            InitializeComponent();
            InicjalizujComboBoxy();
            OdswiezListeBiletow();
        }

        // 1 zakładka
        private void InicjalizujComboBoxy()
        {
            comboTypBiletu.Items.Add("Krajowy");
            comboTypBiletu.Items.Add("Międzykrajowy");
            comboTypBiletu.Items.Add("Międzykontynentalny");
            comboTypBiletu.Items.Add("Z przesiadkami");

            comboTypBiletu.SelectedIndex = 0;

            comboKlasa.ItemsSource = Enum.GetValues(typeof(EnumKlasa));
            comboKlasa.SelectedIndex = 0;
        }

        private void comboTypBiletu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            chkWiza.Visibility = Visibility.Collapsed;
            panelPosilki.Visibility = Visibility.Collapsed;

            string typ = comboTypBiletu.SelectedItem?.ToString() ?? "";
            if (typ == "Międzykontynentalny")
            {
                chkWiza.Visibility = Visibility.Visible;
                panelPosilki.Visibility = Visibility.Visible;
            }
        }

        private void btnDodajBilet_Click(object sender, RoutedEventArgs e)
        {
            SystemSounds.Beep.Play();
            try
            {
                string imie = txtImie.Text;
                string nazwisko = txtNazwisko.Text;
                string miastoWylotu = txtMiastoWylotu.Text;
                string miastoPrzylotu = txtMiastoPrzylotu.Text;

                DateTime data = dateWylotu.SelectedDate ?? throw new Exception("Wybierz datę");
                if (!TimeOnly.TryParseExact(
                    txtGodzina.Text,
                    "HH:mm",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out TimeOnly godzina))
                {
                    MessageBox.Show("Podaj godzinę w formacie HH:mm (np. 09:30)");
                    return;
                }


                EnumKlasa klasa = (EnumKlasa)comboKlasa.SelectedItem;
                double cenaBazowa = 300;

                Bilet nowyBilet;

                switch (comboTypBiletu.SelectedItem.ToString())
                {
                    case "Krajowy":
                        nowyBilet = new BiletKrajowy(imie, nazwisko, cenaBazowa,
                            data, godzina, klasa, DateTime.Now,
                            miastoWylotu, miastoPrzylotu);
                        break;

                    case "Międzykrajowy":
                        nowyBilet = new BiletMiedzykrajowy(imie, nazwisko, cenaBazowa,
                            data, godzina, klasa, miastoPrzylotu, miastoWylotu);
                        break;

                    case "Międzykontynentalny":
                        var bmk = new BiletMiedzykontynentalny(imie, nazwisko, cenaBazowa,
                            data, godzina, klasa, miastoPrzylotu, miastoWylotu,
                            chkWiza.IsChecked == true, 0);

                        if (chkPosilek1.IsChecked == true) bmk.DodajPosilek("Posiłek 1");
                        if (chkPosilek2.IsChecked == true) bmk.DodajPosilek("Posiłek 2");
                        if (chkPosilek3.IsChecked == true) bmk.DodajPosilek("Posiłek 3");

                        nowyBilet = bmk;
                        break;

                    default:
                        throw new Exception("Nieobsługiwany typ biletu");
                }

                OdswiezListeBiletow();
                MessageBox.Show("Bilet dodany poprawnie");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        

        // 2 zakładka

        private void OdswiezListeBiletow()
        {
            listViewBilety.ItemsSource = null;
            listViewBilety.ItemsSource = Bilet.kupioneBilety;
        }

        private void btnSzczegoly_Click(object sender, RoutedEventArgs e)
        {
            SystemSounds.Beep.Play();
            if (listViewBilety.SelectedItem is Bilet bilet)
            {
                MessageBox.Show(bilet.ToString(), "Szczegóły biletu");
            }
            else
            {
                MessageBox.Show("Wybierz bilet z listy");
            }
        }

        //3 zakładka
        private void btnZapiszXML_Click(object sender, RoutedEventArgs e)
        {
            SystemSounds.Beep.Play();
            try
            {
                string nazwaPliku = txtNazwaPliku.Text.Trim();
                if (string.IsNullOrEmpty(nazwaPliku))
                {
                    MessageBox.Show("Podaj nazwę pliku");
                    return;
                }

                Bilet.ZapisXML(nazwaPliku, Bilet.kupioneBilety);
                MessageBox.Show($"Lista biletów zapisana do {nazwaPliku}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd przy zapisie: {ex.Message}");
            }
        }

        private void btnOdczytajXML_Click(object sender, RoutedEventArgs e)
        {
            SystemSounds.Beep.Play();
            try
            {
                string nazwaPliku = txtNazwaPliku.Text.Trim();
                if (string.IsNullOrEmpty(nazwaPliku))
                {
                    MessageBox.Show("Podaj nazwę pliku");
                    return;
                }

                var odczytane = Bilet.OdczytajXML(nazwaPliku);
                if (odczytane == null)
                {
                    MessageBox.Show("Brak pliku do odczytu");
                    return;
                }

                listViewXML.ItemsSource = null;
                listViewXML.ItemsSource = odczytane;

                OdswiezListeBiletow();

                MessageBox.Show($"Lista biletów odczytana z {nazwaPliku}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd przy odczycie: {ex.Message}");
            }
        }
        private void CokolwiekZmienione(object sender, RoutedEventArgs e)
        {
            comboTypBiletu_SelectionChanged(sender, null);
            AktualizujPodgladCeny();
        }

        private void AktualizujPodgladCeny()
        {
            try
            {
                if (comboKlasa.SelectedItem == null || comboTypBiletu.SelectedItem == null)
                    return;

                EnumKlasa klasa = (EnumKlasa)comboKlasa.SelectedItem;

                double cenaBazowa = 300;
                double cena = cenaBazowa;

                // sezon (lipiec–sierpień)
                if (dateWylotu.SelectedDate is DateTime data)
                {
                    if (data.Month == 7 || data.Month == 8)
                    {
                        cena *= 1.5;
                    }
                }

                // klasa
                cena *= klasa switch
                {
                    EnumKlasa.Ekonomiczna => 1.0,
                    EnumKlasa.Biznesowa => 1.5,
                    EnumKlasa.Pierwsza => 2.0,
                    _ => 1.0
                };

                // typ biletu
                if (comboTypBiletu.SelectedItem.ToString() == "Międzykontynentalny")
                {
                    if (chkWiza.IsChecked == true)
                        cena += 200;
                }

                // posiłki
                if (chkPosilek1.IsChecked == true) cena += 50;
                if (chkPosilek2.IsChecked == true) cena += 50;
                if (chkPosilek3.IsChecked == true) cena += 50;

                // zniżki
                if (chkZnizkaStudent.IsChecked == true)
                    cena *= 0.5;

                if (chkZnizkaSenior.IsChecked == true)
                    cena *= 0.7;

                txtPodgladCeny.Text = cena.ToString("C");
            }
            catch
            {
                txtPodgladCeny.Text = "---";
            }
        }

    }
}
