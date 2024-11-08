using Keto.Data;
using Keto.Models;
using Keto.Views.MealPlan;
using Keto.Views.Recipes;
using Keto.Views.Info;
using Keto.Views.Calculators;
using System.Windows;

namespace Keto.Views.Main
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenRecipesPage_Click(object sender, RoutedEventArgs e)
        {
            RecipesPage recipesPage = new RecipesPage();
            recipesPage.Show();
            Hide();
        }

        private void OpenMealPlanPage_Click(object sender, RoutedEventArgs e)
        {
            MealPlanPage mealPlanPage = new MealPlanPage();
            mealPlanPage.Show();
            Hide();
        }

        private void OpenCalculatorsPage_Click(object sender, RoutedEventArgs e)
        {
            CalculatorsPage calculatorsPage = new CalculatorsPage();
            calculatorsPage.Show();
            Hide();
        }

        private void OpenInfoPage_Click(object sender, RoutedEventArgs e)
        {
            InfoPage infoPage = new InfoPage();
            infoPage.Show();
            Hide();
        }
    }
}
