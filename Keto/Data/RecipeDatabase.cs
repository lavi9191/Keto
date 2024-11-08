using System.Collections.Generic;
using System.Data.SQLite;
using Keto.Models;

namespace Keto.Data
{
    public class RecipeDatabase
    {
        private string _connectionString = "Data Source=D:/ProjektyVS2022/Keto/Keto/Data/recipes.db;Version=3;";

        public RecipeDatabase()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Recipes (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Protein REAL,
                        Fat REAL,
                        Carbohydrates REAL,
                        NetCarbohydrates REAL,
                        Calories REAL,
                        Ingredients TEXT,
                        Servings INTEGER,
                        Instructions TEXT,
                        Category TEXT
                    );";
                using (var cmd = new SQLiteCommand(createTableQuery, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Dodawanie nowego przepisu do bazy danych
        public void AddRecipe(Recipe recipe)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Recipes (Name, Protein, Fat, Carbohydrates, NetCarbohydrates, Calories, Ingredients, Servings, Instructions, Category) VALUES (@name, @protein, @fat, @carbs, @netcarbs, @calories, @ingredients, @servings, @instructions, @category)";
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@name", recipe.Name);
                    cmd.Parameters.AddWithValue("@protein", recipe.Protein);
                    cmd.Parameters.AddWithValue("@fat", recipe.Fat);
                    cmd.Parameters.AddWithValue("@carbs", recipe.Carbohydrates);
                    cmd.Parameters.AddWithValue("@netcarbs", recipe.NetCarbohydrates);
                    cmd.Parameters.AddWithValue("@calories", recipe.Calories);
                    cmd.Parameters.AddWithValue("@ingredients", recipe.Ingredients);
                    cmd.Parameters.AddWithValue("@servings", recipe.Servings);
                    cmd.Parameters.AddWithValue("@instructions", recipe.Instructions);
                    cmd.Parameters.AddWithValue("@category", recipe.Category);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Usuwanie przepisu z bazy danych
        public void DeleteRecipe(int recipeId)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = "DELETE FROM Recipes WHERE Id=@id";
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@id", recipeId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateRecipe(Recipe recipe)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = "UPDATE Recipes SET Name=@name, Protein=@protein, Fat=@fat, Carbohydrates=@carbs, NetCarbohydrates=@netCarbs, Calories=@calories, Ingredients=@ingredients, Servings=@servings, Instructions=@instructions, Category=@category WHERE Id=@id";
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@name", recipe.Name);
                    cmd.Parameters.AddWithValue("@protein", recipe.Protein);
                    cmd.Parameters.AddWithValue("@fat", recipe.Fat);
                    cmd.Parameters.AddWithValue("@carbs", recipe.Carbohydrates);
                    cmd.Parameters.AddWithValue("@netCarbs", recipe.NetCarbohydrates);
                    cmd.Parameters.AddWithValue("@calories", recipe.Calories);
                    cmd.Parameters.AddWithValue("@ingredients", recipe.Ingredients);
                    cmd.Parameters.AddWithValue("@servings", recipe.Servings);
                    cmd.Parameters.AddWithValue("@instructions", recipe.Instructions);
                    cmd.Parameters.AddWithValue("@category", recipe.Category); // Dodaj kategorię
                    cmd.Parameters.AddWithValue("@id", recipe.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Pobieranie listy przepisów z bazy danych
        public List<Recipe> GetRecipes()
        {
            var recipes = new List<Recipe>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Recipes";
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            recipes.Add(new Recipe
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Protein = reader.GetDouble(2),
                                Fat = reader.GetDouble(3),
                                Carbohydrates = reader.GetDouble(4),
                                NetCarbohydrates = reader.GetDouble(5),
                                Calories = reader.GetDouble(6),
                                Ingredients = reader.GetString(7),
                                Servings = reader.GetInt32(8),
                                Instructions = reader.GetString(9),
                                Category = reader.GetString(10) // Pobieramy kategorię
                            });
                        }
                    }
                }
            }
            return recipes;                   
        }
    }
}
