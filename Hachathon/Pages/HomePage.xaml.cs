using Microsoft.Maui.Controls;
using System;

namespace Hachathon.Pages
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        // Navigation method for Schedule button
        private async void OnScheduleClicked(object sender, EventArgs e)
        {
            // Navigate to SchedulePage (assuming you have a SchedulePage defined)
            await Navigation.PushAsync(new SchedulePage());
        }

        // Navigation method for Recipe Generator button
        private async void OnRecipeGeneratorClicked(object sender, EventArgs e)
        {
            // Navigate to RecipePage (assuming you have a RecipePage defined)
            await Navigation.PushAsync(new RecipePage());
        }

        // Navigation method for Pantry button
        private async void OnPantryClicked(object sender, EventArgs e)
        {
            // Navigate to PantryPage (assuming you have a PantryPage defined)
            await Navigation.PushAsync(new PantryPage());
        }

        // Navigation method for Nutritional Intake button
        private async void OnNutritionalIntakeClicked(object sender, EventArgs e)
        {
            // Navigate to NutritionalIntakePage (assuming you have a NutritionalIntakePage defined)
            await Navigation.PushAsync(new NutritionalIntakePage());
        }
    }
}
