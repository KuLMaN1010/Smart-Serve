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

        private async void OnScheduleTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SchedulePage());
        }

        private async void OnRecipeGeneratorTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecipePage());
        }

        private async void OnPantryTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PantryPage());
        }

        private async void OnNutritionalIntakeTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NutritionalIntakePage());
        }
    }
}
