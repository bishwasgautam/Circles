using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Circles.Entities;
using Circles.ViewModels;
using Xamarin.Forms;

namespace Circles.Views
{
    public partial class AddressBookCreatePage : ContentPage
    {

        public AddressBook CurrentItem { get; set; }
        private readonly AddressBookViewModel _viewModel;
       
        public AddressBookCreatePage()
        {
            InitializeComponent();
            _viewModel = ViewModelLocator.AddressBookViewModel;
            BindingContext = _viewModel;
        }

        private void BtnSave_OnClicked(object sender, EventArgs e)
        {
            _viewModel.SaveCurrentAddItem();

            //show confirmation
            Acr.UserDialogs.UserDialogs.Instance.Toast(new ToastConfig(ToastEvent.Success, "Saved"));

            Exit();
        }

        private void BtnCancel_OnClicked(object sender, EventArgs e)
        {
            Exit();
        }

        private async void Exit()
        {
            await Navigation.PopModalAsync(true);
        }
    }
}
