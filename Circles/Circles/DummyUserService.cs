using System;
using System.Collections.Generic;
using System.Linq;

using Circles.Entities;

namespace Circles
{
    public class DummyUserService: IUserService
    {
        public User GetUser(string userId)
        {
            return new User()
            {
                FirstName = "Bishwas",
                LastName = "Gautam",
                AccessLevel = AccessLevel.Admin,
                AddressBook  = GetAddressBook("random"),
                PrimaryEmail = "bgautam@dunnsolutions.com",
                Phone = new PhoneInfo() {CellPhone = "224-420-2609"},
                UserName = "bishwasgautam",
                Password = "circles101"
            };
        }

        public string Authenticate(string userName, string passWord)
        {
            return "random";
        }

        public void ForgotLogin(string email, string userId)
        {
            //send email




            //send text
        }

        public List<User> GetCirclesByUserId(string id)
        {
           return new List<User>()
           {
               new User()
               {
                   FirstName ="Bishwas", LastName ="Gautam",
                   AccessLevel = AccessLevel.Admin,
                   Address = GetAddressBook("random").First().Address,
                   PrimaryEmail = "bgautam@dunnsolutions.com",
                   Phone = new PhoneInfo() { CellPhone = "224-420-2609"},
                   UserName = "bishwasgautam", Password = "circles101"
               }
               

           };
        }

        public List<AddressBook> GetAddressBook(string userId)
        {
           return new List<AddressBook>()
           {
               new AddressBook() {AddressName = "hola nola", Address = new Address() { StreetName ="741 Mulford St", Suite = "APT GS", City = "Evanston", State = "IL", ZipCode = "60202", Country = "USA"}},
               new AddressBook() {AddressName = "porfabor ", Address = new Address() { StreetName ="751 Mulford St", Suite = "APT NS", City = "Evanston", State = "IL",  ZipCode = "60201",Country = "USA"}},
               new AddressBook() {AddressName = "cinko de mayo", Address = new Address() { StreetName ="761 Mulford St", Suite = "APT 1", City = "Evanston", State = "IL", ZipCode = "60203", Country = "USA"}}
           };
        }

        public bool AddToCircle(User user)
        {
            return true;
        }

        public bool AddToCircle(string userId)
        {
            return true;
        }

        public bool RemoveFromCircle(string userId)
        {
            return true;
        }

        public bool RemoveFromCircle(User user)
        {
            return true;
        }

        public bool UpdateUser(User user)
        {
            return true;
        }

        public bool RemoveUser(User user)
        {
            return true;
        }

        public bool RemoveUser(string id)
        {
            return true;
        }
    }
}
