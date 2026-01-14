using System;
using System.Collections.Generic;
using System.Windows;
using SystemBiletowLotniczych;

namespace ProjektGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Bilet> listaBiletow = new List<Bilet>();

        public MainWindow()
        {
            InitializeComponent();

            // wypełniamy ComboBox wartościami Enum
            comboKlasa.ItemsSource = Enum.GetValues(typeof(EnumKlasa));
            comboKlasa.SelectedIndex = 0;
        }

        private void btnDodajBilet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Bilet nowyBilet = new BiletKrajowy(
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
