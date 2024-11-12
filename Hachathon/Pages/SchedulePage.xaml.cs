using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Newtonsoft.Json.Linq;

namespace Hachathon.Pages
{
    public partial class SchedulePage : ContentPage
    {
        public SchedulePage()
        {
            InitializeComponent();
        }

        private async void OnGenerateScheduleClicked(object sender, EventArgs e)
        {
            await GenerateWeeklyMealPlan();
        }

        private async Task GenerateWeeklyMealPlan()
        {
            // Define prompt for OpenAI API
            string prompt = "Generate a meal plan for each day of the week with meals scheduled at 8am, 11am, 4pm, and 7pm. Provide a variety of nutritious options.";

            var mealPlan = await CallOpenAIMealPlanAPI(prompt);

            if (mealPlan != null)
            {
                PopulateMealPlan(mealPlan);
            }
        }

        private async Task<List<Dictionary<string, string>>> CallOpenAIMealPlanAPI(string prompt)
        {
            string apiKey = "your_openai_api_key";
            var httpClient = new HttpClient();
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

            // Parse response to obtain meal plan
            string mealPlanText = jsonResponse["choices"]?[0]?["message"]?["content"]?.ToString();
            var mealPlan = ParseMealPlan(mealPlanText);

            return mealPlan;
        }

        private List<Dictionary<string, string>> ParseMealPlan(string mealPlanText)
        {
            // Example parsing logic: Each dictionary in the list represents one day’s meals
            // with time as the key and meal as the value
            var weekPlan = new List<Dictionary<string, string>>();

            // Assuming mealPlanText is formatted in a readable, structured way
            // You may parse based on specific formatting or define your own parsing logic here

            return weekPlan;
        }

        private void PopulateMealPlan(List<Dictionary<string, string>> mealPlan)
        {
            // Clear existing content in schedule grid

            // Loop through each day and fill in meal times
            for (int day = 0; day < 7; day++)
            {
                if (day < mealPlan.Count)
                {
                    var dayPlan = mealPlan[day];

                    foreach (var timeMeal in dayPlan)
                    {
                        string time = timeMeal.Key;
                        string meal = timeMeal.Value;

                        // Calculate row based on time and add Label in the correct column
                        int row = GetRowForTime(time);  // Define this method
                        int column = day + 1;  // Day columns start at 1

                        Label mealLabel = new Label
                        {
                            Text = meal,
                            FontSize = 10,
                            BackgroundColor = Color.Orange,
                            TextColor = Color.Black,
                            HorizontalOptions = LayoutOptions.Fill,
                            VerticalOptions = LayoutOptions.Fill
                        };

                        // Add the label to the grid
                        ScheduleGrid.Children.Add(mealLabel, column, row);
                    }
                }
            }
        }

        private int GetRowForTime(string time)
        {
            // Map time strings (e.g., "8am", "11am") to row numbers in the grid
            switch (time)
            {
                case "8am": return 1;
                case "11am": return 4;
                case "4pm": return 9;
                case "7pm": return 12;
                default: return -1;
            }
        }
    }
}
