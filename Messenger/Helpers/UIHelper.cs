using Android.Content;
using Android.Widget;

namespace Messenger.Helpers
{
    public static class UIHelper
    {
        public static void ShowToastMethod(Context context, string message)
        {
            Toast.MakeText(context, message, ToastLength.Long).Show();
        }
    }
}
