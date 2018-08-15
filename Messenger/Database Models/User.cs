using SQLite;

namespace Messenger.DatabaseModels
{
    public class User
    {
        [PrimaryKey]
        public int UserId { get; set; }

        public string Name { get; set; }
    }
}
