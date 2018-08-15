namespace Messenger.Models
{
    public class ImageMessage : IMessage
    {
        public string mtype { get; set; }
        public string msg_id { get; set; }
        public string sender_id { get; set; }
        public string group_id { get; set; }
        public string url { get; set; }
    }
}
