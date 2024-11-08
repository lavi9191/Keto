using System.Windows;
using System.Windows.Controls;
using Keto.Views.Main;

namespace Keto.Views.Calculators
{
    public partial class CalculatorsPage : Window
    {
        public CalculatorsPage()
        {
            InitializeComponent();
        }

        // Przycisk powrotu do strony głównej
        private void GoToMainPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close(); // Zamknięcie bieżącego okna
        }

        // Funkcja wyświetlająca kalkulator BMR z rozwijaną listą płci
        private void ShowBMRCalculator(object sender, RoutedEventArgs e)
        {
            CalculatorContent.Children.Clear();

            // Etykiety i pola dla BMR
            Label weightLabel = new Label { Content = "Waga (kg):", Margin = new Thickness(5) };
            TextBox weightTextBox = new TextBox { Width = 150, Margin = new Thickness(5) };

            Label heightLabel = new Label { Content = "Wzrost (cm):", Margin = new Thickness(5) };
            TextBox heightTextBox = new TextBox { Width = 150, Margin = new Thickness(5) };

            Label ageLabel = new Label { Content = "Wiek:", Margin = new Thickness(5) };
            TextBox ageTextBox = new TextBox { Width = 150, Margin = new Thickness(5) };

            Label genderLabel = new Label { Content = "Płeć:", Margin = new Thickness(5) };
            ComboBox genderComboBox = new ComboBox { Width = 150, Margin = new Thickness(5) };
            genderComboBox.Items.Add(new ComboBoxItem { Content = "Mężczyzna", Tag = "male" });
            genderComboBox.Items.Add(new ComboBoxItem { Content = "Kobieta", Tag = "female" });

            Label resultLabel = new Label { Content = "Twoje BMR:", Margin = new Thickness(5) };
            TextBox resultTextBox = new TextBox { Width = 150, Margin = new Thickness(5), IsReadOnly = true };

            // Przycisk obliczenia BMR
            Button calculateButton = new Button { Content = "Oblicz BMR", Margin = new Thickness(5) };
            calculateButton.Click += (s, eArgs) =>
            {
                if (double.TryParse(weightTextBox.Text, out double weight) &&
                    double.TryParse(heightTextBox.Text, out double height) &&
                    double.TryParse(ageTextBox.Text, out double age) &&
                    genderComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    string gender = (string)selectedItem.Tag;
                    double bmr = 0;

                    if (gender == "male")
                    {
                        bmr = (10 * weight) + (6.25 * height) - (5 * age) + 5;
                    }
                    else if (gender == "female")
                    {
                        bmr = (10 * weight) + (6.25 * height) - (5 * age) - 161;
                    }

                    resultTextBox.Text = bmr.ToString();
                }
                else
                {
                    MessageBox.Show("Proszę wprowadzić poprawne dane.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };

            // Przycisk czyszczenia danych
            Button clearButton = new Button { Content = "Wyczyść dane", Margin = new Thickness(5) };
            clearButton.Click += (s, eArgs) =>
            {
                weightTextBox.Clear();
                heightTextBox.Clear();
                ageTextBox.Clear();
                resultTextBox.Clear();
                genderComboBox.SelectedIndex = -1;
            };

            // Dodajemy elementy do panelu kalkulatora
            CalculatorContent.Children.Add(weightLabel);
            CalculatorContent.Children.Add(weightTextBox);
            CalculatorContent.Children.Add(heightLabel);
            CalculatorContent.Children.Add(heightTextBox);
            CalculatorContent.Children.Add(ageLabel);
            CalculatorContent.Children.Add(ageTextBox);
            CalculatorContent.Children.Add(genderLabel);
            CalculatorContent.Children.Add(genderComboBox);
            CalculatorContent.Children.Add(calculateButton);
            CalculatorContent.Children.Add(clearButton);
            CalculatorContent.Children.Add(resultLabel);
            CalculatorContent.Children.Add(resultTextBox);
        }

        // Funkcja wyświetlająca kalkulator zapotrzebowania
        private void ShowEnergyCalculator(object sender, RoutedEventArgs e)
        {
            CalculatorContent.Children.Clear();

            // BMR TextBox
            Label bmrLabel = new Label { Content = "Twoje BMR:", Margin = new Thickness(5) };
            TextBox bmrTextBox = new TextBox { Width = 150, Margin = new Thickness(5) };

            // Wskaźnik aktywności ComboBox
            Label activityLabel = new Label { Content = "Wskaźnik aktywności:", Margin = new Thickness(5) };
            ComboBox activityComboBox = new ComboBox { Width = 150, Margin = new Thickness(5) };
            activityComboBox.Items.Add(new ComboBoxItem { Content = "Brak aktywności - 1,2", Tag = 1.2 });
            activityComboBox.Items.Add(new ComboBoxItem { Content = "Mała aktywność - 1,37", Tag = 1.37 });
            activityComboBox.Items.Add(new ComboBoxItem { Content = "Umiarkowana aktywność - 1,55", Tag = 1.55 });
            activityComboBox.Items.Add(new ComboBoxItem { Content = "Duża aktywność - 1,75", Tag = 1.75 });
            activityComboBox.Items.Add(new ComboBoxItem { Content = "Bardzo duża aktywność - 1,9", Tag = 1.9 });

            // Wynik
            Label resultLabel = new Label { Content = "Twoje zapotrzebowanie (kcal):", Margin = new Thickness(5) };
            TextBox resultTextBox = new TextBox { Width = 150, Margin = new Thickness(5), IsReadOnly = true };

            // Oblicz Button
            Button calculateButton = new Button { Content = "Oblicz zapotrzebowanie", Margin = new Thickness(5) };
            calculateButton.Click += (s, eArgs) =>
            {
                if (double.TryParse(bmrTextBox.Text, out double bmr) && activityComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    double activityFactor = (double)selectedItem.Tag;
                    double totalCalories = bmr * activityFactor;
                    resultTextBox.Text = totalCalories.ToString();
                }
                else
                {
                    MessageBox.Show("Proszę wprowadzić poprawne dane.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };

            // Czyszczenie Button
            Button clearButton = new Button { Content = "Wyczyść dane", Margin = new Thickness(5) };
            clearButton.Click += (s, eArgs) =>
            {
                bmrTextBox.Clear();
                resultTextBox.Clear();
                activityComboBox.SelectedIndex = -1;
            };

            // Dodajemy do CalculatorContent
            CalculatorContent.Children.Add(bmrLabel);
            CalculatorContent.Children.Add(bmrTextBox);
            CalculatorContent.Children.Add(activityLabel);
            CalculatorContent.Children.Add(activityComboBox);
            CalculatorContent.Children.Add(calculateButton);
            CalculatorContent.Children.Add(clearButton);
            CalculatorContent.Children.Add(resultLabel);
            CalculatorContent.Children.Add(resultTextBox);
        }

        // Funkcja wyświetlająca kalkulator makroskładników
        private void ShowMacroCalculator(object sender, RoutedEventArgs e)
        {
            CalculatorContent.Children.Clear();

            // Zmieniamy etykietę na "Twoje BMR:"
            Label bmrLabel = new Label { Content = "Twoje BMR:", Margin = new Thickness(5) };
            TextBox bmrTextBox = new TextBox { Width = 150, Margin = new Thickness(5) };

            Label resultLabel = new Label { Content = "Twoje Makroskładniki dla różnych faz:", Margin = new Thickness(5) };

            // Etykiety i pola tekstowe dla makroskładników dla różnych faz
            TextBlock adaptacjaLabel = new TextBlock { Text = "Adaptacja:", Margin = new Thickness(5), FontWeight = FontWeights.Bold };
            TextBlock stabilizacjaLabel = new TextBlock { Text = "Stabilizacja:", Margin = new Thickness(5), FontWeight = FontWeights.Bold };
            TextBlock optymalizacjaLabel = new TextBlock { Text = "Optymalizacja:", Margin = new Thickness(5), FontWeight = FontWeights.Bold };

            // Pola tekstowe dla makroskładników
            TextBox adaptacjaFatTextBox = new TextBox { Width = 150, Margin = new Thickness(5), IsReadOnly = true };
            TextBox adaptacjaProteinTextBox = new TextBox { Width = 150, Margin = new Thickness(5), IsReadOnly = true };
            TextBox adaptacjaCarbsTextBox = new TextBox { Width = 150, Margin = new Thickness(5), IsReadOnly = true };

            TextBox stabilizacjaFatTextBox = new TextBox { Width = 150, Margin = new Thickness(5), IsReadOnly = true };
            TextBox stabilizacjaProteinTextBox = new TextBox { Width = 150, Margin = new Thickness(5), IsReadOnly = true };
            TextBox stabilizacjaCarbsTextBox = new TextBox { Width = 150, Margin = new Thickness(5), IsReadOnly = true };

            TextBox optymalizacjaFatTextBox = new TextBox { Width = 150, Margin = new Thickness(5), IsReadOnly = true };
            TextBox optymalizacjaProteinTextBox = new TextBox { Width = 150, Margin = new Thickness(5), IsReadOnly = true };
            TextBox optymalizacjaCarbsTextBox = new TextBox { Width = 150, Margin = new Thickness(5), IsReadOnly = true };

            // Przycisk obliczenia makroskładników
            Button calculateButton = new Button { Content = "Oblicz makroskładniki", Margin = new Thickness(5) };
            calculateButton.Click += (s, eArgs) =>
            {
                if (double.TryParse(bmrTextBox.Text, out double bmr))
                {
                    // Adaptacja: 80% tłuszcz, 15% białko, 5% węglowodany
                    adaptacjaFatTextBox.Text = $"{Math.Round((bmr * 0.80) / 9)}g tłuszczu";
                    adaptacjaProteinTextBox.Text = $"{Math.Round((bmr * 0.15) / 4)}g białka";
                    adaptacjaCarbsTextBox.Text = $"{Math.Round((bmr * 0.05) / 4)}g węglowodanów";

                    // Stabilizacja: 75% tłuszcz, 20% białko, 5% węglowodany
                    stabilizacjaFatTextBox.Text = $"{Math.Round((bmr * 0.75) / 9)}g tłuszczu";
                    stabilizacjaProteinTextBox.Text = $"{Math.Round((bmr * 0.20) / 4)}g białka";
                    stabilizacjaCarbsTextBox.Text = $"{Math.Round((bmr * 0.05) / 4)}g węglowodanów";

                    // Optymalizacja: 65% tłuszcz, 30% białko, 5% węglowodany
                    optymalizacjaFatTextBox.Text = $"{Math.Round((bmr * 0.65) / 9)}g tłuszczu";
                    optymalizacjaProteinTextBox.Text = $"{Math.Round((bmr * 0.30) / 4)}g białka";
                    optymalizacjaCarbsTextBox.Text = $"{Math.Round((bmr * 0.05) / 4)}g węglowodanów";
                }
                else
                {
                    MessageBox.Show("Proszę wprowadzić poprawne dane.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };

            // Przycisk czyszczenia danych
            Button clearButton = new Button { Content = "Wyczyść dane", Margin = new Thickness(5) };
            clearButton.Click += (s, eArgs) =>
            {
                bmrTextBox.Clear();
                adaptacjaFatTextBox.Clear();
                adaptacjaProteinTextBox.Clear();
                adaptacjaCarbsTextBox.Clear();
                stabilizacjaFatTextBox.Clear();
                stabilizacjaProteinTextBox.Clear();
                stabilizacjaCarbsTextBox.Clear();
                optymalizacjaFatTextBox.Clear();
                optymalizacjaProteinTextBox.Clear();
                optymalizacjaCarbsTextBox.Clear();
            };

            // Dodajemy elementy do panelu kalkulatora
            CalculatorContent.Children.Add(bmrLabel);
            CalculatorContent.Children.Add(bmrTextBox);
            CalculatorContent.Children.Add(calculateButton);
            CalculatorContent.Children.Add(clearButton);

            // Dodajemy etykiety faz makroskładników
            CalculatorContent.Children.Add(resultLabel);
            CalculatorContent.Children.Add(adaptacjaLabel);
            CalculatorContent.Children.Add(adaptacjaFatTextBox);
            CalculatorContent.Children.Add(adaptacjaProteinTextBox);
            CalculatorContent.Children.Add(adaptacjaCarbsTextBox);

            CalculatorContent.Children.Add(stabilizacjaLabel);
            CalculatorContent.Children.Add(stabilizacjaFatTextBox);
            CalculatorContent.Children.Add(stabilizacjaProteinTextBox);
            CalculatorContent.Children.Add(stabilizacjaCarbsTextBox);

            CalculatorContent.Children.Add(optymalizacjaLabel);
            CalculatorContent.Children.Add(optymalizacjaFatTextBox);
            CalculatorContent.Children.Add(optymalizacjaProteinTextBox);
            CalculatorContent.Children.Add(optymalizacjaCarbsTextBox);
        }
    }
}
