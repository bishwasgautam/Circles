﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Circles.Entities;
using Circles.Services;

namespace Circles.ViewModels
{
    public class AddressBookViewModel : BaseViewModel
    {
        private IUserService _dataService;
        private IEnumerable<AddressBook> _allAddressBook;
        private IEnumerable<AddressBook> AllAddressBook {
            get
            {
                RaisePropertyChanged("AllAddressBook");
                return _allAddressBook;
            }
            set
            {
                _allAddressBook = value;
                RaisePropertyChanged("AllAddressBook");
            }
        }

      


        public AddressBookViewModel()
        {
            _dataService = ServiceLocator.UserService;
        }

       private string _currentUserId;
        

        public string CurrentUserId {
            get { return _currentUserId; }
            set
            {
                _currentUserId = value;
                PopulateAddressBook();
            }
        }

        private AddressBook _currentEditItem;
        public AddressBook CurrentEditItem {
            get
            {
                return _currentEditItem;
            }
            set
            {
                _currentEditItem = value;
                RaisePropertyChanged();
            }
        }

        private AddressBook _currentAddItem;
        public AddressBook CurrentAddItem
        {
            get
            {
                if (_currentAddItem == null)
                {
                    _currentAddItem = new AddressBook() {Address = new Address()};
                }
                return _currentAddItem;
            }
            set
            {
                _currentAddItem = value;
                RaisePropertyChanged();
            }
        }



        private void PopulateAddressBook()
        {
            AllAddressBook = ViewModelLocator.WelcomePageViewModel.GetAddressBook();
        }
        
        public AddressBook GetItem(string id)
        {
            return AllAddressBook.FirstOrDefault(x => string.Equals(x.AddressId, id));
        }

        public void UpdateItem()
        {
            var itemInCollection = AllAddressBook.First(x=> string.Equals(x.Id, CurrentEditItem.Id));

            //todo AutoMap
            if (itemInCollection != null)
            {
                itemInCollection.AddressName = CurrentEditItem.AddressName;
                itemInCollection.Address = CurrentEditItem.Address;
            }

            SaveChanges();


        }

     

        public void DeleteItem()
        {
            if(CurrentEditItem != null)
            AllAddressBook.ToList().Remove(CurrentEditItem);

            CurrentEditItem = null;

          SaveChanges();

        }

        public void SaveCurrentAddItem()
        {
            AllAddressBook.ToList().Add(CurrentAddItem);

            SaveChanges();
        }

        private void SaveChanges()
        {
            var currentUser = ViewModelLocator.WelcomePageViewModel.CurrentUser;
            currentUser.AddressBook = AllAddressBook;
            _dataService.UpdateUser(currentUser);
        }
    }
}