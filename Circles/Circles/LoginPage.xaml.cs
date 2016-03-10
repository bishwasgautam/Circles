using System;
using Circles.DataService;
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
            var username = UserNameTextCell.Text;
            var password = PasswordTextCell.Text;

            var userId = _userService.Authenticate(username, password);
            if (!userId.Equals(new Guid(string.Empty)))
            {
                var user = _userService.GetUser(userId);

                //Save session / forms authentication
                
                //Forward to welcome page
                await Navigation.PushAsync(new WelcomePage(user));




            }
        }

        private void ForgotItClicked(object sender, EventArgs e)
        {
            //var username = UserNameTextCell.Text;
            //var pasword = PasswordTextCell.Text;
        }
    }
}
