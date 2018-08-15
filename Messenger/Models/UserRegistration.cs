using Newtonsoft.Json;

namespace Messenger.Models
{
    public class UserRegistration : IMessage
    {
        [JsonProperty]
        public string mtype { get; set; }
        public string user_id { get; set; }
        public string name { get; set; }
    }
}