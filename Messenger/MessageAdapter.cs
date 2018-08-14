using System;
using Android.Support.V7.Widget;
using Android.Views;

namespace Messenger
{
    public class MessageAdapter : RecyclerView.Adapter
    {
        LayoutInflater Inflater { get; set; }

        #region View Holders 

        public class TextMessageViewHolder : RecyclerView.ViewHolder
        {

            public TextMessageViewHolder(View v) : base(v)
            {
                
            }
        }

        #endregion

        #region Constructor

        public MessageAdapter()
        {

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
                    itemView = LayoutInflater.From(viewGroup.Context).Inflate(Resource.Layout.ControlText, viewGroup, false);
                    return new TextMessageViewHolder(itemView);
            }
        }

    }
}
