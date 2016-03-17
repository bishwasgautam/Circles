using Circles.Entities;
using Circles.Services;

namespace Circles.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;
        private static readonly IDummyDataService _dummyDataService = ServiceLocator.DummyDataService;

        private User _currentUser;
        public User CurrentUser {
            get {
                RaisePropertyChanged();
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                RaisePropertyChanged(() => CurrentUser);
            }
        }

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
            //if(CurrentUser == null || !string.Equals(CurrentUser.UserName, username, StringComparison.CurrentCulture))
            //{
            //    CurrentUser = _userService.GetUser(username);
            //}

            return _dummyDataService.LoadDummyAdmin();
        }
    }
}