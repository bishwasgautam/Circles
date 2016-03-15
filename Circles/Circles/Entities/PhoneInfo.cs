using Newtonsoft.Json;

namespace Circles.Entities
{
    public class PhoneInfo
    {
        //TODO define phone format (data annotations)
        [JsonProperty("id")]
        public string Id { get; set; }
        public string Home { get; set; }
        public string Work { get; set; }
        public string CellPhone { get; set; }
    }
}