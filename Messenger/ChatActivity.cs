using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Widget;
using Messenger.Models;
using Messenger.Repository;
using Refractored.Fab;

namespace Messenger
{
    [Activity(Theme = "@style/AppTheme")]
    public class ChatActivity : Activity
    {
        #region Properties 

        RecyclerView recyclerView;
        public RecyclerView.LayoutManager layoutManager;
        public MessageAdapter Adapter { get; set; }
        List<IMessage> Messages { get; set; }
        FloatingActionButton fab;
        EditText messageBox;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Chat);

            string groupId = Intent.GetStringExtra("GroupID");

            Messages = new List<IMessage>();

            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);

            layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);
            Adapter = new MessageAdapter(Messages);
            recyclerView.SetAdapter(Adapter);

            //fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            messageBox = FindViewById<EditText>(Resource.Id.messageInput);

            GetMessages(groupId);

            fab.Click += PostMessage;
        }

        void PostMessage(object sender, EventArgs e)
        {
              new MessageStream().Send(messageBox.Text);
        }

        void GetMessages(string groupId)
        {
            var textrepository = new GenericRepository<TextMessage>();
            var textmsgs = textrepository.Where(tm => tm.group_id == groupId);

            foreach (var m in textmsgs)
            {
                Messages.Add(m);
            }

            var imagerepository = new GenericRepository<ImageMessage>();
            var imagemsgs = imagerepository.Where(im => im.group_id == groupId);

            foreach (var m in imagemsgs)
            {
                Messages.Add(m);
            }
        }
    }
}