using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Keto.Models;
using Keto.Data;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using Xceed.Words.NET; // Dodaj, jeśli korzystasz z DocX
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Diagnostics; // Do otwierania pliku po wygenerowaniu
using Microsoft.Win32;
using PdfSharp.Drawing.Layout;
using Xceed.Document.NET;
using Keto.Views.Main;


namespace Keto.Views.Recipes
{
    public partial class RecipesPage : Window
    {
        private RecipeDatabase _recipeDatabase;

        // Kolekcje ObservableCollection do zarządzania przepisami
        private ObservableCollection<Recipe> _sniadanieKolacjaRecipes;
        private ObservableCollection<Recipe> _obiadRecipes;
        private ObservableCollection<Recipe> _deseryRecipes;

        public RecipesPage()
        {
            InitializeComponent();
            _recipeDatabase = new RecipeDatabase();

            // Inicjalizacja ObservableCollection
            _sniadanieKolacjaRecipes = new ObservableCollection<Recipe>();
            _obiadRecipes = new ObservableCollection<Recipe>();
            _deseryRecipes = new ObservableCollection<Recipe>();

            // Powiązanie kolekcji z ListBoxami
            SniadanieKolacjaList.ItemsSource = _sniadanieKolacjaRecipes;
            ObiadList.ItemsSource = _obiadRecipes;
            DeseryList.ItemsSource = _deseryRecipes;

            // Załaduj przepisy dla każdej kategorii na starcie
            LoadRecipes();
        }

        // Przycisk powrotu do strony głównej
        private void GoToMainPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close(); // Zamknięcie bieżącego okna
        }

        // Przycisk dodawania nowego przepisu
        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            AddRecipeWindow addRecipeWindow = new AddRecipeWindow();

            if (addRecipeWindow.ShowDialog() == true)
            {
                _recipeDatabase.AddRecipe(addRecipeWindow.NewRecipe); // Dodanie nowego przepisu
                LoadRecipes(); // Odświeżenie listy przepisów
            }
        }

        // Przycisk edytowania przepisu
        private void EditRecipe_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecipe = GetSelectedRecipe();
            if (selectedRecipe != null)
            {
                EditRecipeWindow editRecipeWindow = new EditRecipeWindow(selectedRecipe);
                if (editRecipeWindow.ShowDialog() == true)
                {
                    _recipeDatabase.UpdateRecipe(editRecipeWindow.UpdatedRecipe); // Aktualizacja przepisu
                    LoadRecipes(); // Odświeżenie listy przepisów
                }
            }
            else
            {
                MessageBox.Show("Wybierz przepis do edycji.");
            }
        }

        // Załaduj przepisy do odpowiednich kategorii
        private void LoadRecipes()
        {
            var recipes = _recipeDatabase.GetRecipes();

            // Wyczyść aktualne kolekcje
            _sniadanieKolacjaRecipes.Clear();
            _obiadRecipes.Clear();
            _deseryRecipes.Clear();

            // Dodaj przepisy do odpowiednich kolekcji
            foreach (var recipe in recipes)
            {
                if (recipe.Category == "Śniadanie i Kolacja")
                {
                    _sniadanieKolacjaRecipes.Add(recipe);
                }
                else if (recipe.Category == "Obiad")
                {
                    _obiadRecipes.Add(recipe);
                }
                else if (recipe.Category == "Desery")
                {
                    _deseryRecipes.Add(recipe);
                }
            }
        }

        // Obsługa zmiany zaznaczenia na liście przepisów
        private void RecipesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListBox listBox && listBox.SelectedItem is Recipe selectedRecipe)
            {
                DeleteButton.IsEnabled = true;
                DetailsButton.IsEnabled = true;
                EditButton.IsEnabled = true;
            }
            else
            {
                DeleteButton.IsEnabled = false;
                DetailsButton.IsEnabled = false;
                EditButton.IsEnabled = false;
            }
        }

        // Obsługa podwójnego kliknięcia na przepisie
        private void RecipesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listBox = (ListBox)sender;
            var selectedRecipe = listBox.SelectedItem as Recipe;

            if (selectedRecipe != null)
            {
                RecipeDetailsWindow detailsWindow = new RecipeDetailsWindow(selectedRecipe);
                detailsWindow.ShowDialog();
            }
        }

        // Obsługa zmiany zakładki
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SniadanieKolacjaTab.IsSelected)
            {
                SniadanieKolacjaList.ItemsSource = _sniadanieKolacjaRecipes;
            }
            else if (ObiadTab.IsSelected)
            {
                ObiadList.ItemsSource = _obiadRecipes;
            }
            else if (DeseryTab.IsSelected)
            {
                DeseryList.ItemsSource = _deseryRecipes;
            }
        }

        // Przycisk usuwania przepisu
        private void DeleteRecipe_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecipe = GetSelectedRecipe();
            if (selectedRecipe != null)
            {
                _recipeDatabase.DeleteRecipe(selectedRecipe.Id); // Usunięcie przepisu z bazy danych
                LoadRecipes(); // Odświeżenie listy przepisów
            }
        }

        // Przycisk wyświetlania szczegółów przepisu
        private void ShowRecipeDetails_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecipe = GetSelectedRecipe();
            if (selectedRecipe != null)
            {
                RecipeDetailsWindow detailsWindow = new RecipeDetailsWindow(selectedRecipe);
                detailsWindow.ShowDialog();
            }
        }

        // Pobieranie wybranego przepisu na podstawie aktualnie zaznaczonej zakładki
        private Recipe GetSelectedRecipe()
        {
            if (SniadanieKolacjaTab.IsSelected)
            {
                return SniadanieKolacjaList.SelectedItem as Recipe;
            }
            else if (ObiadTab.IsSelected)
            {
                return ObiadList.SelectedItem as Recipe;
            }
            else if (DeseryTab.IsSelected)
            {
                return DeseryList.SelectedItem as Recipe;
            }
            return null;
        }

        private void PrintRecipeToPDF_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecipe = GetSelectedRecipe();
            if (selectedRecipe != null)
            {
                // Okno dialogowe wyboru lokalizacji zapisu pliku PDF
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PDF Files|*.pdf";
                saveFileDialog.Title = "Zapisz przepis jako PDF";
                saveFileDialog.FileName = $"{selectedRecipe.Name}.pdf";

                if (saveFileDialog.ShowDialog() == true)
                {
                    // Ścieżka zapisu wybrana przez użytkownika
                    string filePath = saveFileDialog.FileName;

                    // Tworzenie dokumentu PDF
                    PdfDocument pdf = new PdfDocument();
                    pdf.Info.Title = "Szczegóły przepisu";

                    PdfPage page = pdf.AddPage();
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    XFont fontNormal = new XFont("Verdana", 12);
                    XFont fontBold = new XFont("Verdana", 14);

                    // Ustawianie treści przepisu do PDF
                    int yPoint = 40;
                    gfx.DrawString($"{selectedRecipe.Name.ToUpper()} ",
                                   fontBold, XBrushes.Black, new XRect(20, yPoint, page.Width - 40, page.Height),
                                   XStringFormats.TopLeft);
                    yPoint += 40;
                    gfx.DrawString($"| KALORIE: {selectedRecipe.Calories} | B: {selectedRecipe.Protein}g | T: {selectedRecipe.Fat}g | W NETTO: {selectedRecipe.NetCarbohydrates}g |",
                                   fontBold, XBrushes.Black, new XRect(20, yPoint, page.Width - 40, page.Height),
                                   XStringFormats.TopLeft);
                    yPoint += 40;
                    gfx.DrawString("Składniki:", fontBold, XBrushes.Black, new XRect(20, yPoint, page.Width - 40, page.Height),
                                   XStringFormats.TopLeft);
                    yPoint += 20;

                    // Wyświetlanie składników w nowej linii, bez punktowania
                    foreach (var ingredient in selectedRecipe.Ingredients.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        gfx.DrawString(ingredient.Trim(), fontNormal, XBrushes.Black, new XRect(40, yPoint, page.Width - 60, page.Height),
                                       XStringFormats.TopLeft);
                        yPoint += 20;
                    }

                    yPoint += 20;
                    gfx.DrawString("Przygotowanie:", fontBold, XBrushes.Black, new XRect(20, yPoint, page.Width - 40, page.Height),
                                   XStringFormats.TopLeft);
                    yPoint += 20;

                    // Automatyczne zawijanie tekstu w sekcji przygotowania
                    XTextFormatter tf = new XTextFormatter(gfx);
                    tf.Alignment = XParagraphAlignment.Left;
                    tf.DrawString(selectedRecipe.Instructions, fontNormal, XBrushes.Black, new XRect(20, yPoint, page.Width - 40, page.Height - yPoint));

                    // Zapis do PDF
                    pdf.Save(filePath);
                    MessageBox.Show("Plik PDF zapisano pomyślnie!", "Sukces", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void PrintRecipeToWord_Click(object sender, RoutedEventArgs e)
        {
            var selectedRecipe = GetSelectedRecipe();
            if (selectedRecipe != null)
            {
                // Wybierz lokalizację zapisu pliku Word
                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                saveFileDialog.Filter = "Word Documents|*.docx";
                saveFileDialog.FileName = $"{selectedRecipe.Name}.docx";

                if (saveFileDialog.ShowDialog() == true)
                {
                    // Ścieżka zapisu pliku Word
                    string filePath = saveFileDialog.FileName;

                    // Tworzenie dokumentu Word
                    using (var doc = DocX.Create(filePath))
                    {
                        // Dodanie tytułu przepisu
                        doc.InsertParagraph(selectedRecipe.Name.ToUpper())
                            .FontSize(18)
                            .Bold()
                            .SpacingAfter(10);

                        // Dodanie informacji o kaloriach i makroskładnikach
                        doc.InsertParagraph($"| KALORIE: {selectedRecipe.Calories} | B: {selectedRecipe.Protein}g | T: {selectedRecipe.Fat}g | W NETTO: {selectedRecipe.NetCarbohydrates}g |")
                            .FontSize(14)
                            .SpacingAfter(10);

                        // Dodanie nagłówka składników
                        doc.InsertParagraph("Składniki:")
                            .FontSize(16)
                            .Bold()
                            .SpacingAfter(10);

                        // Dodanie składników bez punktowania
                        foreach (var ingredient in selectedRecipe.Ingredients.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            doc.InsertParagraph(ingredient)
                                .FontSize(12)
                                .SpacingAfter(5); // Zachowanie odstępu między składnikami
                        }

                        // Dodanie nagłówka instrukcji
                        doc.InsertParagraph("Przygotowanie:")
                            .FontSize(16)
                            .Bold()
                            .SpacingAfter(10);

                        // Dodanie instrukcji
                        doc.InsertParagraph(selectedRecipe.Instructions)
                            .FontSize(12)
                            .SpacingAfter(20);

                        // Zapisz dokument
                        doc.Save();
                        MessageBox.Show("Plik Word został zapisany!");
                    }
                }
            }
        }
    }
}
