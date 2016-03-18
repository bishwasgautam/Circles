using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Circles.Entities;
using Circles.Services;

namespace Circles.ViewModels
{
    public class WelcomePageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        private User _currentUser;
        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                RaisePropertyChanged(() => CurrentUser);
            }
        }

        
        public IEnumerable<AddressBook> AddressBook{
            get { return CurrentUser.AddressBook;}
            set {
                if (value != CurrentUser.AddressBook)
                    CurrentUser.AddressBook = value;
                RaisePropertyChanged(() => AddressBook); }
        }

       
        //public WelcomePageViewModel(User user)
        //{
        //    CurrentUser = user;
        //    _userService = ServiceLocator.UserService;
        //}

        public IEnumerable<AddressBook> GetAddressBook()
        {
            return CurrentUser.AddressBook;
        }

        //For dependency injection
        public WelcomePageViewModel(IUserService userService)
        {
            _userService = userService;
        }

        public WelcomePageViewModel()
        {
        }

        internal IEnumerable<AddressBook> GetAddressBook(string Id)
        {
            return string.Equals(CurrentUser.Id, Id, StringComparison.CurrentCulture) ? AddressBook.ToList() : new List<AddressBook>();
        }

        //private async Task<ObservableCollection<AddressBook>> LoadAddressBook()
        //{
        //    ObservableCollection<AddressBook> theCollection;

        //    CurrentUser.AddressBook;
        //    try
        //    {
        //        theCollection = await _userService.GetAll<AddressBook>();
        //    }
        //    catch (Exception ex)
        //    {
        //        theCollection = null;

        //        // Do something with the exception
        //    }

        //    return theCollection;
        //}


    }
}