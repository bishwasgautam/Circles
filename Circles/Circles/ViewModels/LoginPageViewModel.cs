using Circles.Entities;
using Circles.Services;

namespace Circles.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;
        private readonly IDummyDataService _dummyDataService;


        public LoginPageViewModel()
        {
            _userService = ServiceLocator.UserService;
            _dummyDataService = ServiceLocator.DummyDataService;
        }


        public LoginPageViewModel(IUserService us, IDummyDataService dds)
        {
            _userService = us;
            _dummyDataService = dds;
        }


        private User _currentUser;
        public User CurrentUser {
            get {
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