using System;
using System.Collections.Generic;
using Circles.Entities;
using Circles.ViewModels;
using Xamarin.Forms;

namespace Circles.Views
{
    public partial class AddressBookPage : ContentPage
    {
        public AddressBookPage(string userId)
        {
            InitializeComponent();

            var viewModel = ViewModelLocator.AddressBookViewModel;
            viewModel.CurrentUserId = userId;
            AddressBookListView.ItemsSource = viewModel.AllAddressBook;
            BindingContext = viewModel;
        }
        
        private async void AddAddressBook_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AddressBookCreatePage());
        }

        private async void AddressEdit_OnClicked(object sender, EventArgs e)
        {
            //get id / address
            var btn = (Button) sender;
            var id = btn.CommandParameter.ToString();
            //edit
            if (!string.IsNullOrEmpty(id))
            {
                await Navigation.PushModalAsync(new AddressBookEditPage(id));
            }

            
        }
    }

  
}
