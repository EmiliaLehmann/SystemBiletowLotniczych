using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SystemBiletowLotniczych;

namespace ProjektGUI
{
    public partial class MainWindow : Window
    {
        private List<Bilet> listaBiletow = new List<Bilet>();

        public MainWindow()
        {
            InitializeComponent();

            // wypełniamy ComboBox dla klasy wartościami Enum
            comboKlasa.ItemsSource = Enum.GetValues(typeof(EnumKlasa));
            comboKlasa.SelectedIndex = 0;

            // wypełniamy ComboBox dla typu biletu
            comboTypBiletu.Items.Add("Krajowy");
            comboTypBiletu.Items.Add("Międzykrajowy");
            comboTypBiletu.Items.Add("Międzykontynentalny");
            comboTypBiletu.Items.Add("Z przesiadkami");
            comboTypBiletu.SelectedIndex = 0;

            comboTypBiletu.SelectionChanged += comboTypBiletu_SelectionChanged;
        }

        private void comboTypBiletu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboTypBiletu.SelectedItem != null)
            {
                string typ = comboTypBiletu.SelectedItem.ToString();

                // wiza tylko dla międzykontynentalnego
                chkWiza.Visibility = (typ == "Międzykontynentalny") ? Visibility.Visible : Visibility.Collapsed;

                // podatek tylko dla krajowego
                lblPodatek.Visibility = (typ == "Krajowy") ? Visibility.Visible : Visibility.Collapsed;
                txtPodatek.Visibility = (typ == "Krajowy") ? Visibility.Visible : Visibility.Collapsed;

                // posiłki tylko dla międzykontynentalnego
                lblPosilki.Visibility = (typ == "Międzykontynentalny") ? Visibility.Visible : Visibility.Collapsed;
                chkPosilek1.Visibility = (typ == "Międzykontynentalny") ? Visibility.Visible : Visibility.Collapsed;
                chkPosilek2.Visibility = (typ == "Międzykontynentalny") ? Visibility.Visible : Visibility.Collapsed;
                chkPosilek3.Visibility = (typ == "Międzykontynentalny") ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private void btnDodajBilet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Bilet nowyBilet;

                string typ = comboTypBiletu.SelectedItem?.ToString() ?? "Krajowy";

                switch (typ)
                {
                    case "Krajowy":
                        double podatek = double.TryParse(txtPodatek.Text, out double p) ? p : 0.08;
                        nowyBilet = new BiletKrajowy(
                            txtImie.Text,
                            txtNazwisko.Text,
                            100,
                            DateTime.Now.AddDays(1),
                            TimeOnly.FromDateTime(DateTime.Now),
                            (EnumKlasa)comboKlasa.SelectedItem,
                            DateTime.Now,
                            "Kraków",
                            "Warszawa"
                        );
                        ((BiletKrajowy)nowyBilet).StawkaPodatkowa = podatek;
                        break;

                    case "Międzykrajowy":
                        nowyBilet = new BiletMiedzykrajowy(
                            txtImie.Text,
                            txtNazwisko.Text,
                            150,
                            DateTime.Now.AddDays(1),
                            TimeOnly.FromDateTime(DateTime.Now),
                            (EnumKlasa)comboKlasa.SelectedItem,
                            "Warszawa",
                            "Berlin"
                        );
                        break;

                    case "Międzykontynentalny":
                        nowyBilet = new BiletMiedzykontynentalny(
                            txtImie.Text,
                            txtNazwisko.Text,
                            300,
                            DateTime.Now.AddDays(1),
                            TimeOnly.FromDateTime(DateTime.Now),
                            (EnumKlasa)comboKlasa.SelectedItem,
                            "Nowy Jork",
                            "Warszawa",
                            chkWiza.IsChecked ?? false,
                            0
                        );

                        // dodawanie posiłków
                        BiletMiedzykontynentalny bmk = (BiletMiedzykontynentalny)nowyBilet;
                        if (chkPosilek1.IsChecked == true) bmk.DodajPosilek("Posiłek 1");
                        if (chkPosilek2.IsChecked == true) bmk.DodajPosilek("Posiłek 2");
                        if (chkPosilek3.IsChecked == true) bmk.DodajPosilek("Posiłek 3");
                        break;

                    case "Z przesiadkami":
                        BiletZPrzesiadkami biletZ = new BiletZPrzesiadkami();
                        biletZ.ImiePasazera = txtImie.Text;
                        biletZ.NazwiskoPasazera = txtNazwisko.Text;
                        nowyBilet = biletZ;
                        break;

                    default:
                        MessageBox.Show("Niepoprawny typ biletu!");
                        return;
                }

                listaBiletow.Add(nowyBilet);
                listBoxBilety.Items.Add(nowyBilet.PelnyNumerBiletu + " - " + nowyBilet.ImiePasazera);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd: {ex.Message}");
            }
        }
    }
}
