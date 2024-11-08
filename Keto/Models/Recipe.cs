namespace Keto.Models
{
    public class Recipe
    {
        public int Id { get; set; }                    // Id przepisu
        public string Name { get; set; }               // Nazwa przepisu
        public double Protein { get; set; }            // Ilość białka
        public double Fat { get; set; }                // Ilość tłuszczu
        public double Carbohydrates { get; set; }      // Ilość węglowodanów
        public double NetCarbohydrates { get; set; }   // Ilość węglowodanów netto
        public string Ingredients { get; set; }        // Składniki
        public int Servings { get; set; }              // Ilość porcji
        public string Instructions { get; set; }       // Instrukcja przygotowania
        public double Calories { get; set; }           // Kalorie
        public string Category { get; set; }  // "Śniadanie i Kolacja", "Obiad", "Desery"
    }
}
