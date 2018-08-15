using Messenger.DatabaseModels;
using SQLite;

namespace Messenger.Helpers
{
    public static class StartupHelper
    {
        public static void Initialize()
        {
            using (var sqlConnection = new SQLiteConnection(ApplicationHelper.DatabasePath))
            {
                SetupDatabase(sqlConnection);
            }
        }

        static void SetupDatabase(SQLiteConnection sqlConnection)
        {
            sqlConnection.CreateTable<User>();
        }
    }
}