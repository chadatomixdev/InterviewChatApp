using Newtonsoft.Json;

namespace Messenger.Models
{
    public class TextMessage : IMessage
    {
        [JsonProperty]
        public string mtype { get; set; }
        public string msg_id { get; set; }
        public string sender_id { get; set; }
        public string group_id { get; set; }
        public string message { get; set; }
        public string ts { get; set; }
    }
}
