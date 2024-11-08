using System;
using Microsoft.Maui.Controls;

namespace Hachathon.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = usernameEntry.Text;
        string password = passwordEntry.Text;

        // Simple validation
        if (username == "admin" && password == "password")
        {
            await Navigation.PushAsync(new MainPage());
        }
        else
        {
            await DisplayAlert("Error", "Invalid credentials", "OK");
        }
    }
    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        // Navigate to the sign-up page (replace `SignUpPage` with your actual sign-up page class if you have one)
        await Navigation.PushAsync(new SignUpPage());
    }
}