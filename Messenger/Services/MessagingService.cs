using System;
using Android.App;
using Android.Content;
using Android.OS;

namespace Messenger.Services
{
    [Service]
    [IntentFilter(new String[] { "com.xamarin.MessagingService" })]
    public class MessagingService : Service
    {
        #region Properties

        public IBinder Binder { get; private set; }

        #endregion

        public MessagingService()
        {
        }

        public override IBinder OnBind(Intent intent)
        {
            //this.Binder = new  TODO Create Binder
            return Binder;
        }

        public override bool OnUnbind(Intent intent)
        {
            return base.OnUnbind(intent);
        }

        public override void OnDestroy()
        {
            Binder = null;
            base.OnDestroy();
        }

    }
}
