using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Circles.ViewModels
{
    public static class ViewModelLocator
    {
        public static AddressBookViewModel AddressBookViewModel = new AddressBookViewModel();
        public static WelcomePageViewModel WelcomePageViewModel = new WelcomePageViewModel();
        public static LoginPageViewModel LoginPageViewModel = new LoginPageViewModel();
    }
}
