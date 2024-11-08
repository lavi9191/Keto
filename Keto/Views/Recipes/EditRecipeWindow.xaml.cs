using System;
using System.Windows;
using System.Windows.Controls;
using Keto.Models;

namespace Keto.Views.Recipes
{
    public partial class EditRecipeWindow : Window
    {
        public Recipe UpdatedRecipe { get; private set; }

        public EditRecipeWindow(Recipe recipe)
        {
            InitializeComponent();

            // Wypełniamy formularz danymi istniejącego przepisu
            RecipeNameTextBox.Text = recipe.Name;
            ProteinTextBox.Text = recipe.Protein.ToString();
            FatTextBox.Text = recipe.Fat.ToString();
            CarbsTextBox.Text = recipe.Carbohydrates.ToString();
            NetCarbsTextBox.Text = recipe.NetCarbohydrates.ToString();
            CaloriesTextBox.Text = recipe.Calories.ToString();
            IngredientsTextBox.Text = recipe.Ingredients;
            ServingsTextBox.Text = recipe.Servings.ToString();
            InstructionsTextBox.Text = recipe.Instructions;

            // Ustawienie wybranej kategorii na podstawie istniejącego przepisu
            foreach (ComboBoxItem item in CategoryComboBox.Items)
            {
                if (item.Content.ToString() == recipe.Category)
                {
                    CategoryComboBox.SelectedItem = item;
                    break;
                }
            }

            // Przechowujemy Id przepisu
            UpdatedRecipe = recipe;
        }

        // Obsługa kliknięcia na przycisk "Zapisz"
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Aktualizujemy przepis na podstawie wprowadzonych danych
                UpdatedRecipe.Name = RecipeNameTextBox.Text;
                UpdatedRecipe.Protein = double.Parse(ProteinTextBox.Text);
                UpdatedRecipe.Fat = double.Parse(FatTextBox.Text);
                UpdatedRecipe.Carbohydrates = double.Parse(CarbsTextBox.Text);
                UpdatedRecipe.NetCarbohydrates = double.Parse(NetCarbsTextBox.Text);
                UpdatedRecipe.Calories = double.Parse(CaloriesTextBox.Text);
                UpdatedRecipe.Ingredients = IngredientsTextBox.Text;
                UpdatedRecipe.Servings = int.Parse(ServingsTextBox.Text);
                UpdatedRecipe.Instructions = InstructionsTextBox.Text;
                UpdatedRecipe.Category = ((ComboBoxItem)CategoryComboBox.SelectedItem).Content.ToString(); // Zapisanie wybranej kategorii

                DialogResult = true; // Sygnalizujemy, że zapisano zmiany
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas zapisywania: {ex.Message}");
            }
        }

        // Obsługa kliknięcia na przycisk "Anuluj"
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // Nie zapisano zmian
            this.Close(); // Zamykamy okno bez zapisywania zmian
        }
    }
}
