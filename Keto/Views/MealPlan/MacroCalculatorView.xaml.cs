using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Keto.Models;
using Keto.Data;

namespace Keto.Views.MealPlan
{
    public partial class MacroCalculatorView : UserControl
    {
        public ObservableCollection<Recipe> Recipes { get; set; }
        public ObservableCollection<Recipe> SuggestedMeals { get; set; }

        public MacroCalculatorView()
        {
            InitializeComponent();
            RecipeDatabase recipeDb = new RecipeDatabase();
            Recipes = new ObservableCollection<Recipe>(recipeDb.GetRecipes());
            SuggestedMeals = new ObservableCollection<Recipe>();
            SuggestedMealPlanList.ItemsSource = SuggestedMeals;
        }

        private void SearchMealPlan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double targetProtein = double.Parse(ProteinInput.Text);
                double targetFat = double.Parse(FatInput.Text);
                double targetCarbs = double.Parse(CarbsInput.Text);

                SuggestedMeals.Clear();

                // Filtracja przepisów na podstawie makroskładników
                var matchingRecipes = Recipes
                    .Where(r =>
                        Math.Abs(r.Protein - targetProtein) <= 5 &&
                        Math.Abs(r.Fat - targetFat) <= 5 &&
                        Math.Abs(r.Carbohydrates - targetCarbs) <= 5)
                    .ToList();

                foreach (var recipe in matchingRecipes)
                {
                    SuggestedMeals.Add(recipe);
                }

                if (SuggestedMeals.Count == 0)
                {
                    MessageBox.Show("Brak przepisów spełniających podane makroskładniki.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Proszę wprowadzić poprawne wartości liczbowe dla makroskładników.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
