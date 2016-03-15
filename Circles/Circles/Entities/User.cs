//TODO : Add annotations for data validation

using System;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace Circles.Entities
{
    public class User 
    {
        

        [JsonProperty("id")]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PrimaryEmail { get; set; }
        public string SecondaryEmail { get; set; }

        public AccessLevel AccessLevel { get; set; }

        public string PhoneInfoId { get; set; }
        //[ForeignKey("PhoneInfoId")]
        public virtual PhoneInfo Phone { get; set; }

        public string AddressId { get; set; }
        public virtual  Address Address { get; set; }

        //Lazy load this
        public virtual IEnumerable<AddressBook> AddressBook { get; set; }
        
        public DateTime BirthDate { get; set; }

        [JsonIgnore]
        public int Age => (int)Math.Floor((decimal)(DateTime.Now.Year - BirthDate.Year));
        

        //<TODO> Lazy Loading 
        public virtual IEnumerable<User> Circle { get; set; }

        public override string ToString()
        {
            //TODO To QuickString ();
            return base.ToString();
        }
    }

    public enum AccessLevel
    {
        Member,
        Admin
    }
}
