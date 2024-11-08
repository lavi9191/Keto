using System.Windows;
using System.Windows.Controls;
using Keto.Models;

namespace Keto.Views.Recipes
{
    public partial class AddRecipeWindow : Window
    {
        public Recipe NewRecipe { get; private set; }

        public AddRecipeWindow()
        {
            InitializeComponent();
        }

        // Obsługa kliknięcia na przycisk "Zapisz"
        private void SaveRecipe_Click(object sender, RoutedEventArgs e)
        {
            // Sprawdzanie czy wszystkie wymagane pola są wypełnione
            if (string.IsNullOrWhiteSpace(RecipeNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(ProteinTextBox.Text) ||
                string.IsNullOrWhiteSpace(FatTextBox.Text) ||
                string.IsNullOrWhiteSpace(CarbsTextBox.Text) ||
                string.IsNullOrWhiteSpace(NetCarbsTextBox.Text) ||
                string.IsNullOrWhiteSpace(CaloriesTextBox.Text) ||
                string.IsNullOrWhiteSpace(IngredientsTextBox.Text) ||
                string.IsNullOrWhiteSpace(ServingsTextBox.Text) ||
                string.IsNullOrWhiteSpace(InstructionsTextBox.Text) ||
                CategoryComboBox.SelectedItem == null)
            {
                MessageBox.Show("Proszę wypełnić wszystkie pola.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Tworzenie nowego przepisu na podstawie wprowadzonych danych
            try
            {
                NewRecipe = new Recipe
                {
                    Name = RecipeNameTextBox.Text,
                    Protein = double.Parse(ProteinTextBox.Text),
                    Fat = double.Parse(FatTextBox.Text),
                    Carbohydrates = double.Parse(CarbsTextBox.Text),
                    NetCarbohydrates = double.Parse(NetCarbsTextBox.Text),
                    Calories = double.Parse(CaloriesTextBox.Text),
                    Ingredients = IngredientsTextBox.Text,
                    Servings = int.Parse(ServingsTextBox.Text),
                    Instructions = InstructionsTextBox.Text,
                    Category = ((ComboBoxItem)CategoryComboBox.SelectedItem).Content.ToString() // Pobranie kategorii
                };

                DialogResult = true; // Zamknięcie okna i sygnalizowanie zapisania przepisu
                this.Close();
            }
            catch (FormatException)
            {
                MessageBox.Show("Proszę wprowadzić prawidłowe wartości liczbowe dla składników odżywczych.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Obsługa kliknięcia na przycisk "Anuluj"
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Zamknięcie okna bez zapisania zmian
        }
    }
}
