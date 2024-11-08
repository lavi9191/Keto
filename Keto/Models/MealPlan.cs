using System;
using System.Collections.Generic;

namespace Keto.Models
{
    public class MealPlan
    {
        public string Day { get; set; }
        public string Breakfast { get; set; }
        public string Lunch { get; set; }
        public string Dinner { get; set; }

        public MealPlan(string day)
        {
            Day = day;
            Breakfast = "Brak";
            Lunch = "Brak";
            Dinner = "Brak";
        }
    }
}