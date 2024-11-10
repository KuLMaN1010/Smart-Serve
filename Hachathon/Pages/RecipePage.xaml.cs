using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace Hachathon.Pages
{
    public partial class RecipePage : ContentPage
    {
        // Use your new API key here
        private const string apiKey = "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";
        private const string apiUrl = "https://api.openai.com/v1/chat/completions";

        public RecipePage()
        {
            InitializeComponent();
        }

        private async void OnGenerateRecipeClicked(object sender, EventArgs e)
        {
            string ingredients = ingredientsEntry.Text;

            if (string.IsNullOrWhiteSpace(ingredients))
            {
                await DisplayAlert("Error", "Please enter some ingredients.", "OK");
                return;
            }

            recipeOutput.Text = "Generating recipe...";
            recipeOutput.IsVisible = true;

            try
            {
                string response = await GetRecipeFromAI(ingredients);
                recipeOutput.Text = response;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Failed to generate recipe. Please try again.", "OK");
                recipeOutput.Text = string.Empty;
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

        private async Task<string> GetRecipeFromAI(string ingredients)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "user", content = $"Create a recipe using these ingredients: {ingredients}." }
                    },
                    max_tokens = 150,
                    temperature = 0.7
                };

                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(requestBody),
                    Encoding.UTF8,
                    "application/json"
                );

                try
                {
                    Console.WriteLine("Sending request to OpenAI API...");
                    var response = await client.PostAsync(apiUrl, jsonContent);

                    Console.WriteLine($"Status Code: {response.StatusCode}");
                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response Content: {responseContent}");

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonSerializer.Deserialize<OpenAIResponse>(responseContent);
                        return result?.choices?[0]?.message?.content.Trim() ?? "No recipe found.";
                    }
                    else
                    {
                        throw new Exception($"OpenAI API Error: StatusCode={response.StatusCode}, Content={responseContent}");
                    }
                }
                catch (HttpRequestException httpEx)
                {
                    Console.WriteLine($"HTTP Request Exception: {httpEx.Message}");
                    throw new Exception("Network or connectivity issue while communicating with OpenAI API.", httpEx);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"General Exception: {ex.Message}");
                    throw new Exception("Failed to communicate with OpenAI API.", ex);
                }
            }
        }

        // Define helper classes for the OpenAI API response
        public class OpenAIResponse
        {
            public Choice[] choices { get; set; }
        }

        public class Choice
        {
            public Message message { get; set; }
        }

        public class Message
        {
            public string role { get; set; }
            public string content { get; set; }
        }
    }
}
