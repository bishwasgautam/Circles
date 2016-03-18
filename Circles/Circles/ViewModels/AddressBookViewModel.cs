using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Circles.Entities;
using Circles.Services;
using Xamarin.Forms;

namespace Circles.ViewModels
{
    public class AddressBookViewModel : BaseViewModel
    {
        private IUserService _dataService;

        private ObservableCollection<AddressBook> _allAddressBook;
        public ObservableCollection<AddressBook> AllAddressBook
        {
            get
            {
                RaisePropertyChanged(() => AllAddressBook);
                return _allAddressBook;
            }
            set
            {
                _allAddressBook = value;
                RaisePropertyChanged(() => AllAddressBook);
            }
        }

        private bool _isListRefreshing;

        public bool IsListRefreshing
        {
            get { return _isListRefreshing; }
            set { if (_isListRefreshing == value) return;
                _isListRefreshing = value;
                RaisePropertyChanged(() => IsListRefreshing);
            }
        }

        private Command loadTweetsCommand;

        public Command LoadAddressBook
        {
            get { return loadTweetsCommand ??
                    (loadTweetsCommand = new Command(ExecuteLoadAddressBooksCommand, () => { return !IsListRefreshing; })); }
        }
        private async void ExecuteLoadAddressBooksCommand()
        {
            if (IsListRefreshing) return;
            IsListRefreshing = true;
            LoadAddressBook.ChangeCanExecute();

            AllAddressBook.Add(ServiceLocator.DummyDataService.GetRandomAddressBook());
            AllAddressBook.Move(AllAddressBook.Count-1, 0);

            IsListRefreshing = false;
            LoadAddressBook.ChangeCanExecute();
        } 

        public AddressBookViewModel()
        {
            _dataService = ServiceLocator.UserService;
        }


        private string _currentUserId;
        public string CurrentUserId
        {
            get { return _currentUserId; }
            set
            {
                _currentUserId = value;
                PopulateAddressBook();
            }
        }

        private AddressBook _currentEditItem;
        public AddressBook CurrentEditItem
        {
            get
            {
                return _currentEditItem;
            }
            set
            {
                _currentEditItem = value;
                RaisePropertyChanged(() => CurrentEditItem);
            }
        }

        private AddressBook _currentAddItem;
        public AddressBook CurrentAddItem
        {
            get
            {
                if (_currentAddItem == null)
                {
                    _currentAddItem = new AddressBook() { Address = new Address() };
                }
                return _currentAddItem;
            }
            set
            {
                _currentAddItem = value;
                RaisePropertyChanged(() => CurrentAddItem);
            }
        }

        public void Refresh()
        {
            PopulateAddressBook();
        }

        private void PopulateAddressBook()
        {
            AllAddressBook = new ObservableCollection<AddressBook>(ViewModelLocator.WelcomePageViewModel.GetAddressBook(CurrentUserId));
        }

        public AddressBook GetItem(string id)
        {
            return AllAddressBook.FirstOrDefault(x => string.Equals(x.Id, id));
        }

        public void UpdateItem()
        {
            var itemInCollection = AllAddressBook.First(x => string.Equals(x.Id, CurrentEditItem.Id));
            var index = AllAddressBook.IndexOf(itemInCollection);
            //todo AutoMap
            if (itemInCollection != null)
            {
                itemInCollection.AddressName = CurrentEditItem.AddressName;
                itemInCollection.Address = CurrentEditItem.Address;
            }

            AllAddressBook[index] = itemInCollection;
            

            SaveChanges();


        }



        public void DeleteItem()
        {
            if (CurrentEditItem != null)
                AllAddressBook.Remove(CurrentEditItem);

            CurrentEditItem = null;

            SaveChanges();

        }

        public void SaveCurrentAddItem()
        {
            CurrentAddItem.Id = "newId";
            AllAddressBook.Add(CurrentAddItem);
            AllAddressBook.Move(AllAddressBook.Count -1 , 0);
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