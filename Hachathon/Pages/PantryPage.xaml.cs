using System.Net.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System;
using Microsoft.Maui.Controls;

namespace Hachathon.Pages
{
    public partial class PantryPage : ContentPage
    {
        private int milkQuantity = 2;
        private int meatQuantity = 5;
        private int onionsQuantity = 1;
        private int tomatoQuantity = 2;

        private static readonly HttpClient client = new HttpClient();
        public ObservableCollection<string> Messages { get; set; } = new ObservableCollection<string>();
        public PantryPage()
        {
            InitializeComponent();
            UpdateQuantities();
            ChatMessages.ItemsSource = Messages;
        }

        private void UpdateQuantities()
        {
            MilkQuantityLabel.Text = $"{milkQuantity} Litre";
            MeatQuantityLabel.Text = $"{meatQuantity} lb";
            OnionsQuantityLabel.Text = $"{onionsQuantity} lb";
            tomatoQuantityLabel.Text = $"{tomatoQuantity} lb";
        }

        private void DecreaseMilkQuantity(object sender, EventArgs e)
        {
            if (milkQuantity > 0) milkQuantity--;
            UpdateQuantities();
        }

        private void IncreaseMilkQuantity(object sender, EventArgs e)
        {
            milkQuantity++;
            UpdateQuantities();
        }

        private void DecreaseMeatQuantity(object sender, EventArgs e)
        {
            if (meatQuantity > 0) meatQuantity--;
            UpdateQuantities();
        }

        private void IncreaseMeatQuantity(object sender, EventArgs e)
        {
            meatQuantity++;
            UpdateQuantities();
        }

        private void DecreaseOnionsQuantity(object sender, EventArgs e)
        {
            if (onionsQuantity > 0) onionsQuantity--;
            UpdateQuantities();
        }

        private void IncreaseOnionsQuantity(object sender, EventArgs e)
        {
            onionsQuantity++;
            UpdateQuantities();
        }

        private void DecreaseTomatoQuantity(object sender, EventArgs e)
        {
            if (tomatoQuantity > 0) tomatoQuantity--;
            UpdateQuantities();
        }

        private void IncreaseTomatoQuantity(object sender, EventArgs e)
        {
            tomatoQuantity++;
            UpdateQuantities();
        }

        // Event handler for the "Chat with Sarah" button
        private async void OnSendMessageClicked(object sender, EventArgs e)
        {
            string userMessage = UserMessageEntry.Text;
            if (!string.IsNullOrEmpty(userMessage))
            {
                Messages.Add("You: " + userMessage);
                UserMessageEntry.Text = "";

                string response = await GetChatbotResponse(userMessage);
                Messages.Add("Sarah: " + response);
            }
        }

        private async Task<string> GetChatbotResponse(string userMessage)
        {
            var openAiEndpoint = "https://api.openai.com/v1/chat/completions";
            var apiKey = "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";

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                new { role = "system", content = "You are Chef Sarah, an assistant for managing the pantry, setting reminders for expiry dates, and helping with grocery shopping or for any queries regarding the application. Remember information provided to you regarding any expiry dates." },
                new { role = "user", content = userMessage }
            },
                max_tokens = 50,
                temperature = 0.7
            };

            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            var json = JObject.FromObject(requestBody).ToString();
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(openAiEndpoint, content);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseObject = JObject.Parse(responseString);

            return responseObject["choices"]?[0]?["message"]?["content"]?.ToString().Trim() ?? "Sorry, I couldn't get a response.";
        }
    }
}
