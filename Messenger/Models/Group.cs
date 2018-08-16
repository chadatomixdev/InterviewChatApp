namespace Messenger.Models
{
    public class Group : IMessage
    {
        public string mtype { get; set; }
        public string group_id { get; set; }
        public string group_name { get; set; }

        public MessageTypes GetViewType()
        {
            return MessageTypes.Group;
        }
    }
}
