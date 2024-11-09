using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Hachathon.Pages;
public partial class RecipePage : ContentPage
{
    private const string apiKey = "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"; // Replace with your actual API key
    private const string apiUrl = "https://api.openai.com/v1/completions";

    public RecipePage()
	{
		InitializeComponent();
	}
    private async void OnGenerateRecipeClicked(object sender, EventArgs e)
    {
        string ingredients = ingredientsEntry.Text;

        // Check if the ingredients entry field is empty
        if (string.IsNullOrWhiteSpace(ingredients))
        {
            // Display an alert if no ingredients are entered
            await DisplayAlert("Error", "Please enter some ingredients.", "OK");
            return;
        }

        // Show a loading message while waiting for the API response
        recipeOutput.Text = "Generating recipe...";
        recipeOutput.IsVisible = true;

        try
        {
            // Create the prompt using the user's input
            string prompt = $"Create a recipe using these ingredients: {ingredients}.";

            // Call the method to get a recipe from OpenAI
            string response = await GetRecipeFromAI(prompt);

            // Display the response (generated recipe)
            recipeOutput.Text = response;
        }
        catch (Exception ex)
        {
            // Display an error message if the request fails
            await DisplayAlert("Error", "Failed to generate recipe. Please try again.", "OK");
            recipeOutput.Text = string.Empty; // Clear the output if there’s an error
            Console.WriteLine($"Error: {ex.Message}"); // Print the error for debugging
        }
    }

    private async Task<string> GetRecipeFromAI(string prompt)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var requestBody = new
            {
                model = "text-davinci-003",
                prompt = prompt,
                max_tokens = 150,
                temperature = 0.7
            };

            try
            {
                var response = await client.PostAsJsonAsync(apiUrl, requestBody);

                // Log the status code and full response content
                Console.WriteLine($"Status Code: {response.StatusCode}");
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();
                    return result?.choices[0].text.Trim() ?? "No recipe found.";
                }
                else
                {
                    throw new Exception($"OpenAI API Error: {responseContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                throw new Exception("Failed to communicate with OpenAI API", ex);
            }
        }
    }

    // Define a helper class for the OpenAI API response
    public class OpenAIResponse
    {
        public Choice[] choices { get; set; }
    }

    public class Choice
    {
        public string text { get; set; }
    }
}
