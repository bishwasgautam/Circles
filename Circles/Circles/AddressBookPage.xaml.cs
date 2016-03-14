using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Circles.Data;
using Circles.ViewModels;
using Xamarin.Forms;

namespace Circles
{
    public partial class AddressBookPage : ContentPage
    {
        private IDataService _dataService;
        public AddressBookPage()
        {
            InitializeComponent();
            _dataService = ServiceLocator.DataService;
            this.BindingContext = new AddressBookViewModel();
        }

        public AddressBookPage(IDataService ds)
        {
            _dataService = ds;
        }
    }
}
