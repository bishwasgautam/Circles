using System;
using System.Linq;
using Circles.Entities;
using Circles.Services;
using Circles.ViewModels;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;

namespace Circles.Views
{
    public partial class WelcomePage : ContentPage
    {
        private readonly WelcomePageViewModel viewModel;

       
        public WelcomePage(User user)
        {
            InitializeComponent();

            LoadData();
            viewModel = ViewModelLocator.WelcomePageViewModel;
            viewModel.CurrentUser = user;
            this.BindingContext = viewModel;
            PopulatePageData(user);
        }

        public static async void LoadData()
        {
            await ServiceLocator.DummyDataService.LoadDummyData();
        }

        private void PopulatePageData(User user)
        {
            WelcomeTextLbl.Text = $"Welcome, {user.FirstName}";
        }

        private async void ShowAddressBook_OnClicked(object sender, EventArgs e)
        {
            //TODO Custom list view with edit and remove
            var addresses = viewModel.AddressBook.ToList();
            if(addresses != null && addresses.Any())
            await Navigation.PushAsync(new AddressBookPage(viewModel.CurrentUser.Id));
            
        }

        private async void LogOut_OnClicked(object sender, EventArgs e)
        {
            App.Authenticator.Logout();

            await DependencyService.Get<IMobileServiceClient>().LogoutAsync();

            await Navigation.PushAsync(new LoginPage());
        }
    }
}
