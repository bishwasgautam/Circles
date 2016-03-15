using Newtonsoft.Json;

namespace Circles.Entities
{
    public class PasswordRecovery
    {
        //<TODO> Auto Generated, finite number of questions
        [JsonProperty("id")]
        public string Id { get; set; }

        //TODO Just need the id, question can be loaded lazily
        public string Question { get; set; }
        public string Answer { get; set; }

    }
}

