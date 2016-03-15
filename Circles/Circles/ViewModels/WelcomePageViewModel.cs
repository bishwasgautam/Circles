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
        private readonly IDataService _dataService;

        public User CurrentUser { get; set; }

        private ObservableCollection<AddressBookPage> _addressBook;
        public ObservableCollection<AddressBookPage> AddressBook{
            get { return _addressBook;}
            set { if (value != _addressBook) _addressBook = value; RaisePropertyChanged(() => AddressBook); }
        }



        public WelcomePageViewModel(User user)
        {
            CurrentUser = user;
            _dataService = ServiceLocator.DataService;
        }

        internal IEnumerable<AddressBook> GetAddressBook()
        {
            return CurrentUser.AddressBook;
        }

        //For dependency injection
        public WelcomePageViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        public WelcomePageViewModel()
        {
        }

        private async Task<ObservableCollection<AddressBook>> LoadAddressBook()
        {
            ObservableCollection<AddressBook> theCollection;

            try
            {
                theCollection = await _dataService.GetAll<AddressBook>();
            }
            catch (Exception ex)
            {
                theCollection = null;

                // Do something with the exception
            }

            return theCollection;
        }

        public IEnumerable GetAddressBook(object id)
        {
            throw new NotImplementedException();
        }
    }
}