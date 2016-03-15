using System;
using Newtonsoft.Json;

namespace Circles.Entities
{
    public class Address
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("streetname")]
        public string StreetName { get; set; }
        [JsonProperty("suite")]
        public string Suite { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode{ get; set; }
    }
}