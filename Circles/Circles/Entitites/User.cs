using System;
using System.Collections.Generic;

//TODO : Add annotations for data validation
namespace Circles.Entitites
{
    public class User
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public PhoneInfo Phone { get; set; }

        public Address Address { get; set; }
        //Lazy load this
        public List<AddressBook> AddressBook { get; set; }
        
        public DateTime BirthDate { get; set; }
        public int Age => (int)Math.Floor((decimal)(DateTime.Now.Year - BirthDate.Year));
        

        //<TODO> Lazy Loading 
        public IList<User> Circle { get; set; }

        public override string ToString()
        {
            //TODO To QuickString ();
            return base.ToString();
        }
    }

    public class AddressBook
    {
        public string AddressName { get; set; }
        public virtual User User { get; set; }
        public Address Address { get; set; }
    }

    public enum AccessLevel
    {
        Member,
        Admin
    }
}
