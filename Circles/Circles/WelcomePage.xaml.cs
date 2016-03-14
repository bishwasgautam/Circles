using System;
using Circles.Entities;
using Circles.ViewModels;
using Xamarin.Forms;

namespace Circles
{
    public partial class WelcomePage : ContentPage
    {
        public User CurrentUser { get; set; }
        private readonly IUserService _userService = new DummyUserService();

        public WelcomePage(User user)
        {
            InitializeComponent();
            var viewModel = ViewModelLocator.WelcomePageViewModel;
            viewModel.CurrentUser = user;
            this.BindingContext = viewModel;
            CurrentUser = user;
            PopulatePageData(user);
        }

        private void PopulatePageData(User user)
        {
            WelcomeTextLbl.Text = $"Welcome, {user.FirstName}";
        }

        private void ShowAddressBook_OnClicked(object sender, EventArgs e)
        {
            //TODO Custom list view with edit and remove
            var addresses = _userService.GetAddressBook(CurrentUser.Id);
            AddressBookListView.ItemsSource = addresses;

        }
    }
}
