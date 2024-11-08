using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Keto.Models;
using Keto.Data;
using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace Keto.Views.MealPlan
{
    public partial class RandomMealPlanView : UserControl
    {
        private RecipeDatabase _recipeDatabase;
        private ObservableCollection<Recipe> _sniadanieRecipes;
        private ObservableCollection<Recipe> _obiadRecipes;
        private ObservableCollection<Recipe> _kolacjaRecipes;

        public ObservableCollection<GeneratedMeal> GeneratedMeals { get; set; }

        public RandomMealPlanView()
        {
            InitializeComponent();
            _recipeDatabase = new RecipeDatabase();
            LoadRecipes();

            GeneratedMeals = new ObservableCollection<GeneratedMeal>();
            GeneratedMealPlanList.ItemsSource = GeneratedMeals;
        }

        private void LoadRecipes()
        {
            var recipes = _recipeDatabase.GetRecipes();
            _sniadanieRecipes = new ObservableCollection<Recipe>(recipes.Where(r => r.Category == "Śniadanie i Kolacja"));
            _obiadRecipes = new ObservableCollection<Recipe>(recipes.Where(r => r.Category == "Obiad"));
            _kolacjaRecipes = new ObservableCollection<Recipe>(recipes.Where(r => r.Category == "Śniadanie i Kolacja"));
        }

        private static readonly string[] DaysOfWeekInPolish =
        {
            "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela"
        };

        private void GenerateRandomMealPlan_Click(object sender, RoutedEventArgs e)
        {
            int days = int.Parse(((ComboBoxItem)DaysComboBox.SelectedItem).Content.ToString());
            GeneratedMeals.Clear();

            for (int i = 0; i < days; i++)
            {
                var day = DaysOfWeekInPolish[i % 7]; // Pobieranie nazw dni po polsku

                var breakfast = _sniadanieRecipes[new Random().Next(_sniadanieRecipes.Count)];
                var lunch = _obiadRecipes[new Random().Next(_obiadRecipes.Count)];
                var dinner = _kolacjaRecipes[new Random().Next(_kolacjaRecipes.Count)];

                GeneratedMeals.Add(new GeneratedMeal
                {
                    Day = day,
                    Breakfast = breakfast.Name,
                    Lunch = lunch.Name,
                    Dinner = dinner.Name,
                    MacroSummary =  $"B: {Math.Round(breakfast.Protein + lunch.Protein + dinner.Protein, 2)}g, " +
                                    $"T: {Math.Round(breakfast.Fat + lunch.Fat + dinner.Fat, 2)}g, " +
                                    $"W: {Math.Round(breakfast.Carbohydrates + lunch.Carbohydrates + dinner.Carbohydrates, 2)}g"
                });
            }
        }

        private void ExportToPDF_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "PDF Files|*.pdf" };
            if (saveFileDialog.ShowDialog() == true)
            {
                PdfDocument pdf = new PdfDocument();
                pdf.Info.Title = "Jadłospis";

                PdfPage page = pdf.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont headerFont = new XFont("Verdana", 10);
                XFont contentFont = new XFont("Verdana", 8);

                double yStart = 50;
                double xStart = 50;
                double columnWidth = 105;
                double rowHeight = 60;
                double tableWidth = columnWidth * 5;

                // Nagłówek
                gfx.DrawString("Jadłospis", headerFont, XBrushes.Black, new XPoint(xStart, yStart));
                yStart += 40;

                // Rysowanie nagłówków tabeli
                string[] columns = { "Dzień", "Śniadanie", "Obiad", "Kolacja", "Makro" };
                for (int i = 0; i < columns.Length; i++)
                {
                    gfx.DrawRectangle(XPens.Black, new XRect(xStart + (i * columnWidth), yStart, columnWidth, rowHeight));
                    gfx.DrawString(columns[i], headerFont, XBrushes.Black,
                        new XRect(xStart + (i * columnWidth), yStart, columnWidth, rowHeight),
                        XStringFormats.Center);
                }
                yStart += rowHeight;

                // Wypełnianie tabeli danymi
                foreach (var meal in GeneratedMeals)
                {
                    string[] cellContents = { meal.Day, meal.Breakfast, meal.Lunch, meal.Dinner, meal.MacroSummary };
                    for (int i = 0; i < cellContents.Length; i++)
                    {
                        gfx.DrawRectangle(XPens.Black, new XRect(xStart + (i * columnWidth), yStart, columnWidth, rowHeight));
                        var rect = new XRect(xStart + (i * columnWidth), yStart, columnWidth, rowHeight);

                        // Zawijanie tekstu w komórkach
                        string[] words = cellContents[i].Split(' ');
                        string currentLine = "";
                        double lineHeight = 10; // Wysokość pojedynczej linii tekstu
                        double lineY = yStart + 5;

                        foreach (string word in words)
                        {
                            if (gfx.MeasureString(currentLine + word, contentFont).Width < columnWidth)
                            {
                                currentLine += word + " ";
                            }
                            else
                            {
                                gfx.DrawString(currentLine.Trim(), contentFont, XBrushes.Black, rect, XStringFormats.Center);
                                currentLine = word + " ";
                                lineY += lineHeight;
                                rect.Y = lineY;
                            }
                        }
                        gfx.DrawString(currentLine.Trim(), contentFont, XBrushes.Black, rect, XStringFormats.Center);
                    }
                    yStart += rowHeight;

                    // Przejście na nową stronę, jeśli kończy się miejsce
                    if (yStart > page.Height - 50)
                    {
                        page = pdf.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        yStart = 50;
                    }
                }

                pdf.Save(saveFileDialog.FileName);
                MessageBox.Show("Eksport do PDF zakończony.");
            }
        }

        private void DrawCell(XGraphics gfx, string text, double x, double y, double width, XFont font)
        {
            // Logika do zawijania tekstu
            var formattedText = gfx.MeasureString(text, font);
            if (formattedText.Width > width)
            {
                gfx.DrawString(text, font, XBrushes.Black, new XRect(x, y, width, formattedText.Height), XStringFormats.TopLeft);
            }
            else
            {
                gfx.DrawString(text, font, XBrushes.Black, new XRect(x, y, width, formattedText.Height), XStringFormats.Center);
            }
        }


        private void ExportToWord_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "Word Documents|*.docx" };
            if (saveFileDialog.ShowDialog() == true)
            {
                using (var document = DocX.Create(saveFileDialog.FileName))
                {
                    document.InsertParagraph("Jadłospis").FontSize(20).Bold().Alignment = Alignment.center;

                    // Tworzenie tabeli
                    var table = document.AddTable(GeneratedMeals.Count + 1, 5);
                    table.Design = TableDesign.TableGrid;
                    table.Alignment = Alignment.center;

                    // Nagłówki
                    table.Rows[0].Cells[0].Paragraphs[0].Append("Dzień").Bold();
                    table.Rows[0].Cells[1].Paragraphs[0].Append("Śniadanie").Bold();
                    table.Rows[0].Cells[2].Paragraphs[0].Append("Obiad").Bold();
                    table.Rows[0].Cells[3].Paragraphs[0].Append("Kolacja").Bold();
                    table.Rows[0].Cells[4].Paragraphs[0].Append("Makro").Bold();

                    // Wypełnianie tabeli danymi
                    for (int i = 0; i < GeneratedMeals.Count; i++)
                    {
                        var meal = GeneratedMeals[i];
                        table.Rows[i + 1].Cells[0].Paragraphs[0].Append(meal.Day);
                        table.Rows[i + 1].Cells[1].Paragraphs[0].Append(meal.Breakfast);
                        table.Rows[i + 1].Cells[2].Paragraphs[0].Append(meal.Lunch);
                        table.Rows[i + 1].Cells[3].Paragraphs[0].Append(meal.Dinner);
                        table.Rows[i + 1].Cells[4].Paragraphs[0].Append(meal.MacroSummary);
                    }

                    document.InsertTable(table);
                    document.Save();
                    MessageBox.Show("Eksport do Word zakończony.");
                }
            }
        }

    }

    public class GeneratedMeal
    {
        public string Day { get; set; }
        public string Breakfast { get; set; }
        public string Lunch { get; set; }
        public string Dinner { get; set; }
        public string MacroSummary { get; set; }
    }
}
