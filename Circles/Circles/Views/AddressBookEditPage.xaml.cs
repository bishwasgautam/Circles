﻿using System;
using Acr.UserDialogs;
using Circles.Entities;
using Circles.ViewModels;
using Xamarin.Forms;

namespace Circles.Views
{
    public partial class AddressBookEditPage : ContentPage
    {
        public AddressBook CurrentItem { get; set; }
        private readonly AddressBookViewModel _viewModel; 
        public AddressBookEditPage(string id)
        {
            InitializeComponent();
            _viewModel = ViewModelLocator.AddressBookViewModel;
            CurrentItem = _viewModel.GetItem(id);
            _viewModel.CurrentEditItem = CurrentItem;
            BindingContext = _viewModel;
        }


        private void BtnSave_OnClicked(object sender, EventArgs e)
        {
            //check if currentItem has changed
             //otherwise get individual value from each ui control

            //update
            _viewModel.UpdateItem();


            //show confirmation
            Acr.UserDialogs.UserDialogs.Instance.Toast(new ToastConfig(ToastEvent.Success, "Updated!"));

            //close view
            Exit();
        }

        private async void BtnCancel_OnClicked(object sender, EventArgs e)
        {
            Exit();
        }

        private async void Exit()
        {
            await Navigation.PopModalAsync(true);
        }

        private async void BtnDelete_OnClicked(object sender, EventArgs e)
        {
            //delete
            _viewModel.DeleteItem();

            //show confirmation
            Acr.UserDialogs.UserDialogs.Instance.Toast(new ToastConfig(ToastEvent.Success, "Deleted"));

            //close view
            Exit();
        }
    }
}
