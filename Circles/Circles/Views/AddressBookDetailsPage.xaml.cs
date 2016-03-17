using System;
using Acr.UserDialogs;
using Circles.Entities;
using Circles.ViewModels;
using Xamarin.Forms;

namespace Circles.Views
{
    public partial class AddressBookDetailsPage : ContentPage
    {
        private AddressBookViewModel _viewModel;

        public AddressBookDetailsPage(string id)
        {
            
            InitializeComponent();
            _viewModel = ViewModelLocator.AddressBookViewModel;
            CurrentItem = _viewModel.GetItem(id);
            _viewModel.CurrentEditItem = CurrentItem;
            BindingContext = _viewModel;
        }

        public AddressBook CurrentItem { get; set; }

        private async void BtnEdit_OnClicked(object sender, EventArgs e)
        {
           
            if (CurrentItem != null && !string.IsNullOrEmpty(CurrentItem.Id))
            {
                await Navigation.PushModalAsync(new AddressBookEditPage(CurrentItem.Id));
            }
        }

        private void BtnDelete_OnClicked(object sender, EventArgs e)
        {
            //delete
            _viewModel.DeleteItem();

            //show confirmation
            Acr.UserDialogs.UserDialogs.Instance.Toast(new ToastConfig(ToastEvent.Success, "Deleted"));


            //close view
            Exit();
        }

        private async void Exit()
        {
            await Navigation.PopModalAsync(true);
        }
    }
}
