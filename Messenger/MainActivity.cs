using System;
using Android.App;
using Android.OS;
using Messenger.Helpers;
using Messenger.Models;
using Newtonsoft.Json;

namespace Messenger
{
    [Activity(Label = "Messenger", MainLauncher = true)]
    public class MainActivity : Activity
    {
        //RecyclerView recyclerView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            StartupHelper.Initialize();

            new MessageStream().Subscribe(callback);

            //publish messages with the send method
            new MessageStream().Send("My message");
        }

        readonly Action<string> callback = o =>
                 {
                     string result = o;
                     var message = JsonConvert.DeserializeObject<IMessage>(result, new MessageConverter());

                    
                 };
    }
}