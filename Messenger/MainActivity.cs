using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Messenger.Adapters;
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
        #region Properties

        List<Group> Groups { get; set; }

        static GenericRepository<Group> _groupRepository = new GenericRepository<Group>();
        static GenericRepository<TextMessage> _textMessageRepository = new GenericRepository<TextMessage>();
        static GenericRepository<ImageMessage> _imageRepository = new GenericRepository<ImageMessage>();
        static GenericRepository<UserRegistration> _userRepository = new GenericRepository<UserRegistration>();
        static GenericRepository<DeliveryReport> _deliverReportRepository = new GenericRepository<DeliveryReport>();

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            StartupHelper.Initialize();

            new MessageStream().Subscribe(callback);

            GetGroups();

            var groupList = FindViewById<ListView>(Resource.Id.groupListView);
            groupList.ItemClick += OnItemClick;

            groupList.Adapter = new GroupAdapter(Groups);
        }

        void GetGroups()
        {
            Groups = _groupRepository.GetAll();
        }

        void OnItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var group = Groups[e.Position];

            var intent = new Intent(this, typeof(ChatActivity));
            intent.PutExtra("GroupID", group.group_id);

            StartActivity(intent);
        }

        readonly Action<string> callback = o =>
                 {
                     string result = o;
                     var message = JsonConvert.DeserializeObject<IMessage>(result, new MessageConverter());

                     using (var sqlConnection = new SQLiteConnection(ApplicationHelper.DatabasePath))
                     {
                         switch (message.mtype)
                         {
                             case "group_created":
                                 var gm = (Group)message;

                                 var gp = _groupRepository.Where(g => g.group_id == gm.group_id);

                                 if (gp.Count == 0)
                                 {
                                     sqlConnection.Insert(message);
                                 }
                                 break;
                             case "text_message":
                                 var tmsg = (TextMessage)message;

                                 var tmr = _textMessageRepository.Where(t => t.msg_id == tmsg.msg_id);

                                 if (tmr.Count == 0)
                                 {
                                     sqlConnection.Insert(message);
                                 }
                                 break;
                             case "image_message":
                                 var imsg = (ImageMessage)message;

                                 var imr = _imageRepository.Where(i => i.msg_id == imsg.msg_id);

                                 if (imr.Count == 0)
                                 {
                                     sqlConnection.Insert(message);
                                 }
                                 break;
                             case "delivery_report":
                                 var dr = (DeliveryReport)message;

                                 var drr = _deliverReportRepository.Where(d => d.msg_id == dr.msg_id);

                                 if (drr.Count == 0)
                                 {
                                     sqlConnection.Insert(message);
                                 }
                                 break;
                             case "user_registered":
                                 var ur = (UserRegistration)message;

                                 var urr = _userRepository.Where(u => u.user_id == ur.user_id);

                                 if (urr.Count == 0)
                                 {
                                     sqlConnection.Insert(message);
                                 }
                                 break;
                         }
                     }
                 };
    }
}