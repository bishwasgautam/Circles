using System;
using System.Collections.Generic;
using System.Linq;
using Circles.Entities;
using Microsoft.WindowsAzure.MobileServices.Sync;

namespace Circles.Services
{
    public class UserService :  IUserService
    {
        private IMobileServiceSyncTable<User> _userTable;
        private readonly IDataService _dataService;
        public UserService()
        {
            _dataService = ServiceLocator.DataService;
            _userTable = _dataService.Table<User>();
        }
        
        public bool IsValidLogin(string username, string password)
        {
            return string.Equals(username, "admin", StringComparison.CurrentCultureIgnoreCase)
                   && string.Equals(password, "Admin2016", StringComparison.CurrentCultureIgnoreCase);

        }

        //private User GetUser(string a)
        //{
        //    var user = Builder<User>.CreateNew().With(x => x.AccessLevel = AccessLevel.Admin)
        //        .With(x => x.FirstName = Faker.Name.First())
        //        .With(x => x.LastName = Faker.Name.Last())
        //        .With(x => x.BirthDate = Faker.Date.Birthday())
        //        .With(x => x.Phone = new PhoneInfo()
        //        {
        //            CellPhone = Faker.Phone.CellNumber(),
        //            Home = Faker.Phone.Number()
        //        })
        //        .With(x => x.UserName = "admin")
        //        .With(x => x.Password = "admin2016")
        //        .With(x => x.PrimaryEmail = Faker.Internet.Email())
        //        .With(x => x.SecondaryEmail = Faker.Internet.Email())
        //        .Build();
        //    user.AddressBook = GetDummyAddressBook(user);

        //    return user;
        //}

        public string Authenticate(string userName, string passWord)
        {
            throw new NotImplementedException();
        }

        public void ForgotLogin(string email, string userId)
        {
            throw new NotImplementedException();
        }

        public List<User> GetCirclesByUserId(string id)
        {
            throw new NotImplementedException();
        }

        public List<AddressBook> GetAddressBook(string userId)
        {
            throw new NotImplementedException();
        }

        public bool AddToCircle(User user)
        {
            throw new NotImplementedException();
        }

        public bool AddToCircle(string userId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveFromCircle(string userId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveFromCircle(User user)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool RemoveUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool RemoveUser(string id)
        {
            throw new NotImplementedException();
        }

        public User GetUser(string username)
        {
            return _dataService.GetAll<User>()
                .Result.ToList()
                .First(user => string.Equals(user.UserName, username, StringComparison.CurrentCulture));
        }
    }
}