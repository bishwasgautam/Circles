using System;
using Circles.Entities;
using Circles.ViewModels;
using Xamarin.Forms;

namespace Circles
{
    public partial class LoginPage : ContentPage
    {

        public LoginPageViewModel ViewModel;
        
        public LoginPage()
        {
            InitializeComponent();
            ViewModel = ViewModelLocator.LoginPageViewModel;
            this.BindingContext = ViewModel; 
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void LoginClicked(object sender, EventArgs e)
        {
            var username = UserNameEntry.Text;
            var password = PasswordEntry.Text;

            User user = null;

            if (ViewModel.IsValidUser(username, password))
            {
                user = ViewModel.GetCurrentUser(username);
            }


            if (App.Authenticator != null)
            {
                App.Authenticated = await App.Authenticator.Authenticate(username, password);
            }
            

            // Set syncItems to true in order to synchronize the data on startup when running in offline mode
            if (App.Authenticated)
                await Navigation.PushAsync(new WelcomePage(user));

            //send the new page

        }

        private void ForgotItClicked(object sender, EventArgs e)
        {
            //var username = UserNameEntry.Text;
            //var password = PasswordEntry.Text;

        }
    }
}
