namespace Messenger.Models
{
    public class UserRegistration : IMessage
    {
        public string mtype { get; set; }
        public string user_id { get; set; }
        public string name { get; set; }

        public MessageTypes GetViewType()
        {
            return MessageTypes.UserRegistration;
        }
    }
}