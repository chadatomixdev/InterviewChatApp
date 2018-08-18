using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Messenger.Models;
using Messenger.Repository;

namespace Messenger
{
    [Activity(Label = "ChatActivity")]
    public class ChatActivity : Activity
    {
        #region Properties 

        RecyclerView recyclerView;
        public RecyclerView.LayoutManager layoutManager;
        public MessageAdapter Adapter { get; set; }
        List<IMessage> Messages { get; set; }

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

            GetMessages(groupId);

            ////publish messages with the send method
            //new MessageStream().Send("My message");
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