using System;

using Xamarin.Forms;

namespace Circles
{
    public partial class LoginPage : ContentPage
    {
        private readonly IUserService _userService = new DummyUserService();
        
        public LoginPage()
        {
            InitializeComponent();
        }


        private async void LoginClicked(object sender, EventArgs e)
        {
            var username = UserNameEntry.Text;
            var password = PasswordEntry.Text;

            var userId = _userService.Authenticate(username, password);
            if (!string.IsNullOrEmpty(userId))
            {
                var user = _userService.GetUser(userId);
                if (user != null)
                {
                    //Add user to azure tables
                    var aus = new AzureUserService();
                    //await aus.AddUser(user);
                }
                

                //Save session / forms authentication

                //Forward to welcome page
                await Navigation.PushAsync(new WelcomePage(user));
            }
        }

        private void ForgotItClicked(object sender, EventArgs e)
        {
            //var username = UserNameEntry.Text;
            //var password = PasswordEntry.Text;

        }
    }
}
