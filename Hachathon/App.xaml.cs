using Microsoft.Maui.Controls;
using Hachathon.Pages;

namespace Hachathon
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Set LoginPage as the initial page
            return new Window(new NavigationPage(new HomePage()));
        }
    }
}
