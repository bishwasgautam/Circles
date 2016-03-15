using System;

namespace Circles.Entities
{
    public class AddressBook
    {
        public string Id { get; set; }
        public string AddressName { get; set; }
        public string WPLHID{ get; set; }
        public string AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}