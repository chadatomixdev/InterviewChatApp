using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Messenger.Helpers;
using Messenger.Models;
using Messenger.Repository;
using Newtonsoft.Json;
using SQLite;

namespace Messenger
{
    [Activity(Label = "Messenger", MainLauncher = true)]
    public class MainActivity : Activity
    {
        RecyclerView recyclerView;
        public RecyclerView.LayoutManager layoutManager;
        public MessageAdapter Adapter { get; set; }
        List<IMessage> Messages { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            Messages = new List<IMessage>();

            StartupHelper.Initialize();

            //new MessageStream().Subscribe(callback);

            ////publish messages with the send method
            //new MessageStream().Send("My message");

            GetMessages();

            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

            layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);
            Adapter = new MessageAdapter(Messages);
            recyclerView.SetAdapter(Adapter);
        }

        void GetMessages()
        {
            var textrepository = new GenericRepository<TextMessage>();
            var textmsgs = textrepository.GetAll();

            foreach (var m in textmsgs)
            {
                Messages.Add(m);
            }

            var imagerepository = new GenericRepository<ImageMessage>();
            var imagemsgs = imagerepository.GetAll();

            foreach (var m in imagemsgs)
            {
                Messages.Add(m);
            }
        }

        readonly Action<string> callback = o =>
                 {
                     string result = o;
                     var message = JsonConvert.DeserializeObject<IMessage>(result, new MessageConverter());

                     using (var sqlConnection = new SQLiteConnection(ApplicationHelper.DatabasePath))
                     {
                         sqlConnection.Insert(message);
                     }
                 };
    }
}