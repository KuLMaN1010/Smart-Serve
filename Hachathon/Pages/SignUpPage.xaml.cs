namespace Hachathon.Pages;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
		InitializeComponent();
	}
    private async void OnSignUpClicked(object sender, EventArgs e)
    {
        string username = usernameEntry.Text;
        string email = emailEntry.Text;
        string password = passwordEntry.Text;
        string confirmPassword = confirmPasswordEntry.Text;

        // Basic validation
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            await DisplayAlert("Error", "Please fill in all fields.", "OK");
            return;
        }

        if (password != confirmPassword)
        {
            await DisplayAlert("Error", "Passwords do not match.", "OK");
            return;
        }

        // Success message (you can add more logic here, like saving the user data)
        await DisplayAlert("Success", "Account created successfully!", "OK");

        // Navigate to login page after successful signup
        await Navigation.PushAsync(new LoginPage());
    }
    private async void OnLoginClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new LoginPage());
    }
}