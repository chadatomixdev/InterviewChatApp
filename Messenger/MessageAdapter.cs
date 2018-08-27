using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Messenger.Helpers;
using Messenger.Models;

namespace Messenger
{
    public class MessageAdapter : RecyclerView.Adapter
    {
        #region Properties

        LayoutInflater Inflater { get; set; }
        public IList<IMessage> Items { get; set; }

        #endregion

        #region View Holders 

        public class TextMessageViewHolder : RecyclerView.ViewHolder
        {
            public TextView Name { get; }

            public TextView Message { get; }

            public TextMessageViewHolder(View v) : base(v)
            {
                Name = (TextView)v.FindViewById(Resource.Id.TextMessageNameTextView);
                Message = (TextView)v.FindViewById(Resource.Id.TextMessageMessageTextView);
            }
        }

        public class ImageMessageViewHolder : RecyclerView.ViewHolder
        {
            public TextView Name { get; }

            public ImageView Image { get; }

            public ImageMessageViewHolder(View v) : base(v)
            {
                Name = (TextView)v.FindViewById(Resource.Id.ImageMessageNameTextView);
                Image = (ImageView)v.FindViewById(Resource.Id.ImageMessagePhotoImageView);
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
                case MessageTypes.ImageMessage:
                    BindImageMessage(holder, position);
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

        void BindImageMessage(RecyclerView.ViewHolder holder, int position)
        {
            var item = GetItem<ImageMessage>(position);
            if (item == null) return;

            var ImageMessageViewHolder = (ImageMessageViewHolder)holder;

            var imageBitmap = ImageHelper.GetImageBitmapFromUrl(item.url);
            ImageMessageViewHolder.Image.SetImageBitmap(imageBitmap);

            var tag = new Tag
            {
                Position = position,
                Id = item.msg_id
            };
        }

        #endregion

        public override int GetItemViewType(int position)
        {
            var ItemViewType = Items[position].GetViewType();

            switch (ItemViewType)
            {
                case MessageTypes.TextMessage:
                    return 1;
                case MessageTypes.ImageMessage:
                    return 2;
                default:
                    return 0;
            }
        }

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

        public override int ItemCount
        {
            get { return Items.Count; }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup viewGroup, int viewType)
        {
            View itemView;
            Inflater = LayoutInflater.FromContext(viewGroup.Context);

            switch (viewType)
            {
                case 1:
                    itemView = LayoutInflater.From(viewGroup.Context).Inflate(Resource.Layout.TextMessage, viewGroup, false);
                    return new TextMessageViewHolder(itemView);
                case 2:
                    itemView = LayoutInflater.From(viewGroup.Context).Inflate(Resource.Layout.ImageMessage, viewGroup, false);
                    return new ImageMessageViewHolder(itemView);
                default:
                    itemView = LayoutInflater.From(viewGroup.Context).Inflate(Resource.Layout.TextMessage, viewGroup, false);
                    return new TextMessageViewHolder(itemView);
            }
        }
    }
}
