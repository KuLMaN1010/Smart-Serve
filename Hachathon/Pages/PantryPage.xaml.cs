using Microsoft.Maui.Controls;

namespace Hachathon.Pages
{
    public partial class PantryPage : ContentPage
    {
        private int milkQuantity = 2; // Default quantity for milk
        private int meatQuantity = 5; // Default quantity for meat

        public PantryPage()
        {
            InitializeComponent();
            UpdateQuantityLabels();
        }

        private void UpdateQuantityLabels()
        {
            MilkQuantityLabel.Text = $"{milkQuantity} Litre";
            MeatQuantityLabel.Text = $"{meatQuantity} lb";
        }

        private void DecreaseMilkQuantity(object sender, EventArgs e)
        {
            if (milkQuantity > 0)
            {
                milkQuantity--;
                UpdateQuantityLabels();
            }
        }

        private void IncreaseMilkQuantity(object sender, EventArgs e)
        {
            milkQuantity++;
            UpdateQuantityLabels();
        }

        private void DecreaseMeatQuantity(object sender, EventArgs e)
        {
            if (meatQuantity > 0)
            {
                meatQuantity--;
                UpdateQuantityLabels();
            }
        }

        private void IncreaseMeatQuantity(object sender, EventArgs e)
        {
            meatQuantity++;
            UpdateQuantityLabels();
        }

        private async void OnChatWithSarahClicked(object sender, EventArgs e)
        {
            // Code to open chat with Sarah, potentially with AI integration
            await DisplayAlert("Chat with Sarah", "Starting chat with Chef Sarah!", "OK");
        }
    }
}
