using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Circles.ViewModels
{
    public static class ViewModelLocator
    {
        public static AddressBookViewModel AddressBookViewModel = App.Container.Resolve<AddressBookViewModel>();
        public static WelcomePageViewModel WelcomePageViewModel = App.Container.Resolve<WelcomePageViewModel>();
        public static LoginPageViewModel LoginPageViewModel = App.Container.Resolve<LoginPageViewModel>();
    }
}
