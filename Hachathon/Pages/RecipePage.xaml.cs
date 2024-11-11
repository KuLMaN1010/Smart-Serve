using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Newtonsoft.Json.Linq;

namespace Hachathon.Pages
{
    public partial class RecipePage : ContentPage
    {
        private int userRating;

        public RecipePage()
        {
            InitializeComponent();
            SetStarGestureRecognizers(); // Initialize gestures for star rating
        }

        private async void OnGenerateButtonClicked(object sender, EventArgs e)
        {
            // Retrieve user-entered ingredients
            string ingredientsInput = IngredientsEntry.Text;
            bool usePantry = PantrySwitch.IsToggled;

            // If "Use Pantry" is toggled, append pantry ingredients to user input
            if (usePantry)
            {
                string pantryIngredients = "2 litres of milk, 5 lbs of beef, 1 onion, 2 tomatoes";
                ingredientsInput = string.IsNullOrWhiteSpace(ingredientsInput)
                    ? pantryIngredients
                    : ingredientsInput + ", " + pantryIngredients;
            }

            // Call OpenAI API to generate a recipe using the combined ingredients and pantry toggle
            string recipeResponse = await GenerateRecipeFromOpenAI(ingredientsInput, usePantry);
            DisplayRecipe(recipeResponse);
        }



        private async Task<string> GenerateRecipeFromOpenAI(string ingredients, bool usePantry)
        {
            string prompt = $"Generate a recipe using: {ingredients}.";
            if (usePantry)
                prompt += " Also include ingredients available in the pantry.";

            var apiKey = "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"; // Secure your API key
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var requestBody = new JObject
            {
                ["model"] = "gpt-3.5-turbo",
                ["messages"] = new JArray
                {
                    new JObject
                    {
                        ["role"] = "user",
                        ["content"] = prompt
                    }
                }
            };

            var response = await httpClient.PostAsync("https://api.openai.com/v1/chat/completions",
                new StringContent(requestBody.ToString(), System.Text.Encoding.UTF8, "application/json"));
            var responseContent = await response.Content.ReadAsStringAsync();

            var jsonResponse = JObject.Parse(responseContent);
            return jsonResponse["choices"]?[0]?["message"]?["content"]?.ToString() ?? "No recipe generated.";
        }

        private void DisplayRecipe(string recipeText)
        {
            var parts = recipeText.Split(new[] { "Instructions:" }, StringSplitOptions.None);
            IngredientsDisplay.Text = parts[0].Replace("Ingredients:", "").Trim();
            InstructionsDisplay.Text = parts.Length > 1 ? parts[1].Trim() : "Instructions not provided.";
        }

        private void OnStarClicked(object sender, EventArgs e)
        {
            // Determine which star was clicked by checking its position in the stars list
            var button = sender as ImageButton;
            int rating = int.Parse(button.ClassId ?? "1"); // Use ClassId or CommandParameter to differentiate stars

            // Update the rating and visual feedback
            userRating = rating;
            UpdateStarRatingDisplay();
        }

        private void UpdateStarRatingDisplay()
        {
            var stars = new List<ImageButton> { Star1, Star2, Star3, Star4, Star5 };
            for (int i = 0; i < stars.Count; i++)
            {
                stars[i].Source = i < userRating ? "star_filled.png" : "star_empty.png";
            }
        }

        private void OnAddToCalendarClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(IngredientsDisplay.Text) && !string.IsNullOrEmpty(InstructionsDisplay.Text))
            {
                CalendarManager.AddRecipeToCalendar("Generated Recipe", IngredientsDisplay.Text, InstructionsDisplay.Text);
                DisplayAlert("Success", "Recipe added to calendar!", "OK");
            }
            else
            {
                DisplayAlert("Error", "No recipe to add. Please generate a recipe first.", "OK");
            }
        }

        private void OnRegenerateClicked(object sender, EventArgs e)
        {
            OnGenerateButtonClicked(sender, e);
        }

        private void SetStarGestureRecognizers()
        {
            // Set TapGestureRecognizers directly for each star ImageButton
            Star1.ClassId = "1";
            Star2.ClassId = "2";
            Star3.ClassId = "3";
            Star4.ClassId = "4";
            Star5.ClassId = "5";
        }
    }

    public static class CalendarManager
    {
        public static void AddRecipeToCalendar(string title, string ingredients, string instructions)
        {
            // Placeholder logic for adding a calendar event.
        }
    }
}
