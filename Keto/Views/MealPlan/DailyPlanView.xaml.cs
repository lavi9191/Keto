using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Keto.Models;
using Keto.Data;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using Microsoft.Win32;
using Xceed.Document.NET;
using Xceed.Words.NET;
using PdfSharp;


namespace Keto.Views.MealPlan
{
    public partial class DailyPlanView : UserControl
    {
        public ObservableCollection<Recipe> Recipes { get; set; }
        public ObservableCollection<MealPlan> SelectedMeals { get; set; }

        public DailyPlanView()
        {
            InitializeComponent();
            RecipeDatabase recipeDb = new RecipeDatabase();
            Recipes = new ObservableCollection<Recipe>(recipeDb.GetRecipes());
            SelectedMeals = new ObservableCollection<MealPlan>();
            SelectedMealsList.ItemsSource = SelectedMeals;

            // Filtruj przepisy na podstawie kategorii
            BreakfastComboBox.ItemsSource = Recipes.Where(r => r.Category == "Śniadanie i Kolacja");
            LunchComboBox.ItemsSource = Recipes.Where(r => r.Category == "Obiad");
            DinnerComboBox.ItemsSource = Recipes.Where(r => r.Category == "Śniadanie i Kolacja");
        }

        private void AddToPlan_Click(object sender, RoutedEventArgs e)
        {
            var selectedDay = (DayOfWeekComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            var breakfast = (BreakfastComboBox.SelectedItem as Recipe)?.Name ?? "Brak";
            var lunch = (LunchComboBox.SelectedItem as Recipe)?.Name ?? "Brak";
            var dinner = (DinnerComboBox.SelectedItem as Recipe)?.Name ?? "Brak";

            SelectedMeals.Add(new MealPlan
            {
                Day = selectedDay,
                Breakfast = breakfast,
                Lunch = lunch,
                Dinner = dinner
            });
        }

        private void ClearData_Click(object sender, RoutedEventArgs e)
        {
            SelectedMeals.Clear();
        }

        private void ExportToPDF_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "PDF Files|*.pdf" };
            if (saveFileDialog.ShowDialog() == true)
            {
                PdfDocument pdf = new PdfDocument();
                pdf.Info.Title = "Jadłospis";

                PdfPage page = pdf.AddPage();
                page.Orientation = PageOrientation.Landscape; // Ustawienie strony poziomo
                page.Size = PageSize.A4; // Upewnienie się, że strona ma rozmiar A4

                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont headerFont = new XFont("Verdana", 10);
                XFont contentFont = new XFont("Verdana", 8);

                double xStart = 50;
                double yStart = 50;
                double columnWidth = 100; // Możesz dostosować szerokość kolumny, aby uzyskać odpowiedni margines
                double rowHeight = 30;
                double pageRightMargin = 50; // Dodanie prawego marginesu

                // Nagłówek
                gfx.DrawString("Jadłospis", new XFont("Verdana", 20), XBrushes.Black, new XPoint(xStart, yStart));
                yStart += 40;

                // Rysowanie nagłówków kolumn (dni tygodnia)
                string[] daysOfWeek = { "Dzień", "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela" };
                for (int i = 0; i < daysOfWeek.Length; i++)
                {
                    gfx.DrawRectangle(XPens.Black, XBrushes.White, xStart + i * columnWidth, yStart, columnWidth, rowHeight);
                    gfx.DrawString(daysOfWeek[i], headerFont, XBrushes.Black, new XRect(xStart + i * columnWidth, yStart, columnWidth, rowHeight), XStringFormats.Center);
                }
                yStart += rowHeight;

                // Rysowanie posiłków
                string[] mealTimes = { "ŚNIADANIE", "OBIAD", "KOLACJA" };
                foreach (var mealTime in mealTimes)
                {
                    gfx.DrawRectangle(XPens.Black, XBrushes.White, xStart, yStart, columnWidth, rowHeight);
                    gfx.DrawString(mealTime, headerFont, XBrushes.Black, new XRect(xStart, yStart, columnWidth, rowHeight), XStringFormats.Center);

                    for (int i = 1; i < daysOfWeek.Length; i++)
                    {
                        gfx.DrawRectangle(XPens.Black, XBrushes.White, xStart + i * columnWidth, yStart, columnWidth, rowHeight);
                        var meal = SelectedMeals.FirstOrDefault(m => m.Day == daysOfWeek[i]);
                        string mealText = mealTime switch
                        {
                            "ŚNIADANIE" => meal?.Breakfast ?? "Brak",
                            "OBIAD" => meal?.Lunch ?? "Brak",
                            "KOLACJA" => meal?.Dinner ?? "Brak",
                            _ => ""
                        };

                        // Zawijanie tekstu i wycentrowanie
                        var lines = SplitTextToFitWidth(mealText, contentFont, columnWidth - 10, gfx);
                        double lineHeight = contentFont.GetHeight();
                        double textY = yStart;
                        foreach (var line in lines)
                        {
                            gfx.DrawString(line, contentFont, XBrushes.Black, new XRect(xStart + i * columnWidth, textY, columnWidth, lineHeight), XStringFormats.Center);
                            textY += lineHeight;
                        }
                    }
                    yStart += rowHeight;
                }

                pdf.Save(saveFileDialog.FileName);
                MessageBox.Show("Eksport do PDF zakończony.");
            }
        }

        private void RemoveSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedMealsList.SelectedItem != null)
            {
                var selectedMeal = (MealPlan)SelectedMealsList.SelectedItem;
                SelectedMeals.Remove(selectedMeal);
            }
            else
            {
                MessageBox.Show("Wybierz rekord do usunięcia.", "Brak zaznaczenia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        // Funkcja do zawijania tekstu
        private List<string> SplitTextToFitWidth(string text, XFont font, double maxWidth, XGraphics gfx)
        {
            List<string> lines = new List<string>();
            string[] words = text.Split(' ');
            string currentLine = "";

            foreach (var word in words)
            {
                string testLine = currentLine.Length > 0 ? currentLine + " " + word : word;
                double width = gfx.MeasureString(testLine, font).Width;
                if (width < maxWidth)
                {
                    currentLine = testLine;
                }
                else
                {
                    lines.Add(currentLine);
                    currentLine = word;
                }
            }
            if (currentLine.Length > 0)
                lines.Add(currentLine);

            return lines;
        }

        private void ExportToWord_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog { Filter = "Word Documents|*.docx" };
            if (saveFileDialog.ShowDialog() == true)
            {
                var doc = DocX.Create(saveFileDialog.FileName);

                // Ustawienie strony poziomo
                doc.PageLayout.Orientation = Xceed.Document.NET.Orientation.Landscape;

                // Nagłówek dokumentu
                doc.InsertParagraph("Jadłospis").FontSize(20).Bold().Alignment = Alignment.center;

                // Tworzenie tabeli
                var table = doc.AddTable(4, 8); // 4 wiersze (1 nagłówek + 3 posiłki), 8 kolumn (dni tygodnia)
                table.Design = TableDesign.TableGrid;
                table.Alignment = Alignment.center;
                table.AutoFit = AutoFit.Contents;

                // Dodawanie nagłówków kolumn
                string[] daysOfWeek = { "Dzień", "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela" };
                for (int i = 0; i < daysOfWeek.Length; i++)
                {
                    table.Rows[0].Cells[i].Paragraphs[0].Append(daysOfWeek[i]).Bold().Alignment = Alignment.center;
                }

                // Dodawanie posiłków
                string[] mealTimes = { "ŚNIADANIE", "OBIAD", "KOLACJA" };
                for (int row = 1; row <= 3; row++)
                {
                    table.Rows[row].Cells[0].Paragraphs[0].Append(mealTimes[row - 1]).Bold().Alignment = Alignment.center;

                    for (int col = 1; col < daysOfWeek.Length; col++)
                    {
                        var meal = SelectedMeals.FirstOrDefault(m => m.Day == daysOfWeek[col]);
                        string mealText = mealTimes[row - 1] switch
                        {
                            "ŚNIADANIE" => meal?.Breakfast ?? "Brak",
                            "OBIAD" => meal?.Lunch ?? "Brak",
                            "KOLACJA" => meal?.Dinner ?? "Brak",
                            _ => ""
                        };

                        table.Rows[row].Cells[col].Paragraphs[0].Append(mealText).Alignment = Alignment.center;
                    }
                }

                doc.InsertTable(table);
                doc.Save();
                MessageBox.Show("Eksport do Word zakończony.");
            }
        }
    }

        public class MealPlan
    {
        public string Day { get; set; }
        public string Breakfast { get; set; }
        public string Lunch { get; set; }
        public string Dinner { get; set; }
    }
}
