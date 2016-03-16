using System.Collections.Generic;
using Circles.Entities;

namespace Circles.Services
{
    public interface IUserService
    {
        User GetUser(string userName);
        bool IsValidLogin(string username, string password);

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
