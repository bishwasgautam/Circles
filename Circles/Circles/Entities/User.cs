//TODO : Add annotations for data validation

using System;
using System.Collections.Generic;
using Circles.Common.Extensions;
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


        public string ExtractedTypesFlat { get; set; } //for use by the Azure client libs only
        
        [JsonProperty("AccessLevel")]
        public string AccessLevelFlat { get; set; }

        [JsonIgnore]
        public AccessLevel AccessLevel {
            get { return (AccessLevel) Enum.Parse(typeof (AccessLevel), AccessLevelFlat); }
            set { AccessLevelFlat = value.ToEnumString(); }
        }

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
