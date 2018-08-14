using System;
using System.IO;

namespace Messenger.Helpers
{
    public static class ApplicationHelper
    {
        public static string ApplicationName
        {
            get
            {
                return "Messenger";
            }
        }

        public static string DatabasePath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), ApplicationName + ".db");
            }
        }
    }
}
