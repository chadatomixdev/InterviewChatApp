using System;
using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
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

            public TextMessageViewHolder(View v) : base(v)
            {
                
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
            throw new NotImplementedException();
        }

        #endregion

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
