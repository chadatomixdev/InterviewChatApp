using System;
using Android.App;
using Android.Widget;
using Android.OS;

namespace Messenger
{
    [Activity(Label = "Messenger", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //handle incoming messages with the subscribe method
            new MessageStream().Subscribe(Console.WriteLine);

            //publish messages with the send method
            new MessageStream().Send("My message");  

            
        }
    }
}

