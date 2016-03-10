using System;
using System.Collections.Generic;
using Circles.Entitites;

namespace Circles.DataService
{
    interface IUserService
    {
        User GetUser(Guid userId);
        Guid Authenticate(string userName, string passWord);

        //Send confirmation email / text
        void ForgotLogin(string email, Guid userId);

        List<User> GetCirclesByUserId(Guid id);
        List<Address> GetAddressBook(Guid userId);

        bool AddToCircle(User user);
        bool AddToCircle(Guid userId);

        bool RemoveFromCircle(Guid userId);
        bool RemoveFromCircle(User user);

        bool UpdateUser(User user);

        bool RemoveUser(User user);
        bool RemoveUser(Guid id);


        
    }

    //interface IAddressBookService
    //{

    //}
}
