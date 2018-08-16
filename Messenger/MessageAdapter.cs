using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Messenger.Models;

namespace Messenger
{
    public class MessageAdapter : RecyclerView.Adapter
    {
        LayoutInflater Inflater { get; set; }
        public IList<IMessage> Items { get; set; }

        #region View Holders 

        public class TextMessageViewHolder : RecyclerView.ViewHolder
        {
            TextView _name;
            TextView _message;

            public TextView Name
            {
                get { return _name; }
            }

            public TextView Message
            {
                get { return _message; }
            }

            public TextMessageViewHolder(View v) : base(v)
            {
                _name = (TextView)v.FindViewById(Resource.Id.TextMessageNameTextView);
                _message = (TextView)v.FindViewById(Resource.Id.TextMessageMessageTextView);
            }
        }

        public class ImageMessageViewHolder : RecyclerView.ViewHolder
        {

            public ImageMessageViewHolder(View v) : base(v)
            {

            }
        }

        #endregion

        #region Constructor

        public MessageAdapter(List<IMessage> items)
        {
            Items = items;
        }

        #endregion

        #region Bind Views to View Holder

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var ItemViewType = Items[position].GetViewType();
            switch (ItemViewType)
            {
                case MessageTypes.TextMessage:
                    BindTextMessage(holder, position);
                    break;
            }
        }

        void BindTextMessage(RecyclerView.ViewHolder holder, int position)
        {
            var item = GetItem<TextMessage>(position);
            if (item == null) return;

            var TextMessageViewHolder = (TextMessageViewHolder)holder;
            TextMessageViewHolder.Message.Text = item.message;

            var tag = new Tag
            {
                Position = position,
                Id = item.msg_id
            };
        }

        #endregion

        public T GetItem<T>(int position) where T : IMessage
        {
            T result = default(T);

            if (Items.Count > position)
            {
                var item = Items[position];
                if (item != null)
                {
                    result = (T)item;
                }
            }
            return result;
        }

        public override int ItemCount => throw new NotImplementedException();

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup viewGroup, int viewType)
        {
            View itemView;
            Inflater = LayoutInflater.FromContext(viewGroup.Context);

            switch (viewType)
            {
                default:
                    itemView = LayoutInflater.From(viewGroup.Context).Inflate(Resource.Layout.TextMessage, viewGroup, false);
                    return new TextMessageViewHolder(itemView);
            }
        }
    }
}
