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


            var repository = new GenericRepository<TextMessage>();
            var mg = repository.GetAll();

            foreach (var m in mg)
            {
                Messages.Add(m);
            }

            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);
            Adapter = new MessageAdapter(Messages);
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