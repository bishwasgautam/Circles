using System;
using System.Collections.Generic;
using System.Linq;
using Circles.Entities;
using Circles.ViewModels;
using Xamarin.Forms;

namespace Circles.Views
{
    public partial class AddressBookPage : ContentPage
    {
        private AddressBookViewModel viewmodel;
        public AddressBookPage(string userId)
        {
            InitializeComponent();

            viewmodel = ViewModelLocator.AddressBookViewModel;
            viewmodel.CurrentUserId = userId;

            BindListView();

            BindingContext = viewmodel;
        }

        private void BindListView()
        {
            if(viewmodel != null && viewmodel.AllAddressBook.Any())
            AddressBookListView.ItemsSource = viewmodel.AllAddressBook;

            AddressBookListView.IsPullToRefreshEnabled = true;
            
        }

        private async void AddAddressBook_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddressBookCreatePage());
        }

        private async void AddressEdit_OnClicked(object sender, EventArgs e)
        {
            //get id / address
            var menuItem = (MenuItem)sender;
            var id = menuItem.CommandParameter.ToString();

            //edit
            if (!string.IsNullOrEmpty(id))
            {
                await Navigation.PushModalAsync(new AddressBookEditPage(id));
            }

            
        }
        
        protected override bool OnBackButtonPressed()
        {
            InitializeComponent();

            //refresh list
            BindListView();

            return base.OnBackButtonPressed();
        }

        protected override void OnAppearing()
        {
            InitializeComponent();

            //refresh list
            BindListView();

            base.OnAppearing();
        }

        private async void AddressDetails_OnClicked(object sender, EventArgs e)
        {
            //get id / address
            var menuItem = (MenuItem)sender;
            var id = menuItem.CommandParameter.ToString();

            //edit
            if (!string.IsNullOrEmpty(id))
            {
                await Navigation.PushModalAsync(new AddressBookDetailsPage(id));
            }
        }

    }

  
}
