using System;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;

namespace Messenger
{
    [Activity(Label = "Messenger", MainLauncher = true)]
    public class MainActivity : Activity
    {
        RecyclerView recyclerView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            //StartupHelper.Initialize();




            //handle incoming messages with the subscribe method
            new MessageStream().Subscribe(Console.WriteLine);

            //publish messages with the send method
            new MessageStream().Send("My message");
        }
    }
}