using System.Windows;
using Keto.Models;

namespace Keto.Views.Recipes
{
    public partial class RecipeDetailsWindow : Window
    {
        public RecipeDetailsWindow(Recipe recipe)
        {
            InitializeComponent();
            DisplayRecipeDetails(recipe);
        }

        private void DisplayRecipeDetails(Recipe recipe)
        {
            RecipeNameTextBlock.Text = recipe.Name;
            RecipeCategoryTextBlock.Text = recipe.Category;  // Wyświetlanie kategorii
            RecipeIngredientsTextBlock.Text = recipe.Ingredients;
            RecipeProteinTextBlock.Text = recipe.Protein.ToString();
            RecipeFatTextBlock.Text = recipe.Fat.ToString();
            RecipeCarbsTextBlock.Text = recipe.Carbohydrates.ToString();
            RecipeNetCarbsTextBlock.Text = recipe.NetCarbohydrates.ToString();
            RecipeCaloriesTextBlock.Text = recipe.Calories.ToString();
            RecipeServingsTextBlock.Text = recipe.Servings.ToString();
            RecipeInstructionsTextBlock.Text = recipe.Instructions;
        }
    }
}
