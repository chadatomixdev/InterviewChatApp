using System;
using Android.Content;
using Android.OS;

namespace Messenger.Services
{
    public class MessagingServiceConnection : Java.Lang.Object, IServiceConnection
    {
        #region Properties

        public bool IsConnected { get; private set; }
        MainActivity mainActivity;

        #endregion

        public MessagingServiceConnection(MainActivity activity)
        {
            IsConnected = false;
            mainActivity = activity;
        }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            throw new NotImplementedException();
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            throw new NotImplementedException();
        }
    }
}
