using System;
using Circles.Entities;
using Circles.ViewModels;
using Xamarin.Forms;

namespace Circles
{
    public partial class WelcomePage : ContentPage
    {
        private readonly WelcomePageViewModel viewModel;

       
        public WelcomePage(User user)
        {
            InitializeComponent();

            App.LoadData();
            viewModel = ViewModelLocator.WelcomePageViewModel;
            viewModel.CurrentUser = user;
            this.BindingContext = viewModel;
            PopulatePageData(user);
        }

        private void PopulatePageData(User user)
        {
            WelcomeTextLbl.Text = $"Welcome, {user.FirstName}";
        }

        private void ShowAddressBook_OnClicked(object sender, EventArgs e)
        {
            //TODO Custom list view with edit and remove
            var addresses = viewModel.GetAddressBook();
            AddressBookListView.ItemsSource = addresses;

        }
    }
}
