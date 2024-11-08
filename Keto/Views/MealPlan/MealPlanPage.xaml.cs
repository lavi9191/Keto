using System.Windows;
using System.Windows.Controls;
using Keto.Views.Main;

namespace Keto.Views.MealPlan
{
    public partial class MealPlanPage : Window
    {
        public MealPlanPage()
        {
            InitializeComponent();
        }

        private void GoToMainPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void ShowDailyPlan_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new DailyPlanView(); // Załaduj widok dziennego planu
        }

        
        private void ShowMacroCalculator_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new MacroCalculatorView(); // Załaduj widok kalkulatora makroskładników
        }

        private void ShowRandomMealPlan_Click(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new RandomMealPlanView(); // Załaduj widok generatora losowych jadłospisów
        }
    }
}
