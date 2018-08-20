using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Widget;
using Messenger.Models;
using Messenger.Repository;

namespace Messenger
{
    [Activity(Label = "ChatActivity", Theme = "@style/Theme.Main")]
    public class ChatActivity : AppCompatActivity
    {
        #region Properties 

        RecyclerView recyclerView;
        public RecyclerView.LayoutManager layoutManager;
        public MessageAdapter Adapter { get; set; }
        List<IMessage> Messages { get; set; }
        FloatingActionButton sendButton;
        EditText messageBox;
        string groupId;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Chat);

            groupId = Intent.GetStringExtra("GroupID");

            Messages = new List<IMessage>();

            recyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            sendButton = FindViewById<FloatingActionButton>(Resource.Id.fab);
            messageBox = FindViewById<EditText>(Resource.Id.messageInput);

            layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);
            Adapter = new MessageAdapter(Messages);
            recyclerView.SetAdapter(Adapter);

            GetMessages();

            sendButton.Click += SendButton_Click;
        }

        void GetMessages()
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

        #region Events

        void SendButton_Click(object sender, EventArgs e)
        {
            try
            {
                new MessageStream().Send(messageBox.Text);

                //MOCK A SENT MESSAGE
                var textrepository = new GenericRepository<TextMessage>();
                var txtmsg = textrepository.FirstOrDefault(m => !string.IsNullOrEmpty(m.msg_id));

                var msg = new TextMessage
                {
                    group_id = groupId,
                    message = messageBox.Text,
                    sender_id = txtmsg.sender_id,
                    ts = txtmsg.ts //Would traditi
                };

                textrepository.Insert(msg);
                Messages.Add(msg);

                Adapter.NotifyDataSetChanged();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                messageBox.Text = "";
            }
        }

        #endregion
    }
}