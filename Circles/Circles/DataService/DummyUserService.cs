﻿using System;
using System.Collections.Generic;
using System.Linq;
using Circles.Entitites;

namespace Circles.DataService
{
    public class DummyUserService: IUserService
    {
        public User GetUser(Guid userId)
        {
            return new User()
            {
                FirstName = "Bishwas",
                LastName = "Gautam",
                AccessLevel = AccessLevel.Admin,
                Address = GetAddressBook(new Guid()).First(),
                PrimaryEmail = "bgautam@dunnsolutions.com",
                Phone = new PhoneInfo() {CellPhone = "224-420-2609"},
                UserName = "bishwasgautam",
                Password = "circles101"
            };
        }

        public Guid Authenticate(string userName, string passWord)
        {
           return new Guid("circles__userGuid*");
        }

        public void ForgotLogin(string email, Guid userId)
        {
            //send email




            //send text
        }

        public List<User> GetCirclesByUserId(Guid id)
        {
           return new List<User>()
           {
               new User()
               {
                   FirstName ="Bishwas", LastName ="Gautam",
                   AccessLevel = AccessLevel.Admin,
                   Address = GetAddressBook(new Guid()).First(),
                   PrimaryEmail = "bgautam@dunnsolutions.com",
                   Phone = new PhoneInfo() { CellPhone = "224-420-2609"},
                   UserName = "bishwasgautam", Password = "circles101"
               }
               

           };
        }

        public List<Address> GetAddressBook(Guid userId)
        {
           return new List<Address>()
           {
               new Address() { StreetName ="741 Mulford St", Suite = "APT GS", City = "Evanston", State = "IL", ZipCode = "60202", Country = "USA"},
               new Address() { StreetName ="751 Mulford St", Suite = "APT NS", City = "Evanston", State = "IL",  ZipCode = "60201",Country = "USA"},
               new Address() { StreetName ="761 Mulford St", Suite = "APT 1", City = "Evanston", State = "IL", ZipCode = "60203", Country = "USA"},
           };
        }

        public bool AddToCircle(User user)
        {
            return true;
        }

        public bool AddToCircle(Guid userId)
        {
            return true;
        }

        public bool RemoveFromCircle(Guid userId)
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

        public bool RemoveUser(Guid id)
        {
            return true;
        }
    }
}