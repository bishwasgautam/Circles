using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Circles.Data;
using Circles.Entities;

namespace Circles.ViewModels
{
    public static class ViewModelLocator
    {
        public static AddressBookViewModel AddressBookViewModel => new AddressBookViewModel();
        public static WelcomePageViewModel WelcomePageViewModel => new WelcomePageViewModel();
        public static LoginPageViewModel LoginPageViewModel => new LoginPageViewModel();
    }

    public class LoginPageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;
        
        public User CurrentUser { get; set; }

        public LoginPageViewModel(IUserService userService)
        {
            this._userService = userService;
        }

        public LoginPageViewModel()
        {
            _userService = ServiceLocator.UserService;
        }

        //IsValidUser
        public  bool IsValidUser(string username, string password)
        {
            return  _userService.IsValidLogin(username, password);
        }

        public User GetCurrentUser(string username)
        {
            if(CurrentUser == null || !string.Equals(CurrentUser.UserName, username, StringComparison.CurrentCulture))
            {
                CurrentUser = _userService.GetUser(username);
            }

            return CurrentUser;
        }
    }
}
