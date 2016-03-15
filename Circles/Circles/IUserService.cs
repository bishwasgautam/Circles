using System;
using System.Collections.Generic;
using Circles.Entities;

namespace Circles
{
    interface IUserService
    {
        User GetUser(string userId);
        string Authenticate(string userName, string passWord);

        //Send confirmation email / text
        void ForgotLogin(string email, string userId);

        List<User> GetCirclesByUserId(string id);
        List<AddressBook> GetAddressBook(string userId);

        bool AddToCircle(User user);
        bool AddToCircle(string userId);

        bool RemoveFromCircle(string userId);
        bool RemoveFromCircle(User user);

        bool UpdateUser(User user);

        bool RemoveUser(User user);
        bool RemoveUser(string id);


        
    }

    //interface IAddressBookService
    //{

    //}
}
