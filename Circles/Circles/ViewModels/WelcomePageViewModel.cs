using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Circles.Data;
using Circles.Entities;

namespace Circles.ViewModels
{
    public class WelcomePageViewModel : BaseViewModel
    {
        private readonly IUserService _userService;

        public User CurrentUser { get; set; }

        private ObservableCollection<AddressBookPage> _addressBook;
        public ObservableCollection<AddressBookPage> AddressBook{
            get { return _addressBook;}
            set { if (value != _addressBook) _addressBook = value; RaisePropertyChanged(() => AddressBook); }
        }
        
        public WelcomePageViewModel(User user)
        {
            CurrentUser = user;
            _userService = ServiceLocator.UserService;
        }

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