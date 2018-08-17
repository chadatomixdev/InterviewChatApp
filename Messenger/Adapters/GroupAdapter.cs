using System.Collections.Generic;
using Android.Views;
using Android.Widget;
using Messenger.Models;

namespace Messenger.Adapters
{
    public class GroupAdapter : BaseAdapter<Group>
    {
        List<Group> Groups;

        public GroupAdapter(List<Group> _groups)
        {
            Groups = _groups;
        }

        public override Group this[int position] 
        {
            get 
            {
                return Groups[position];
            }
        }

        public override int Count
        {
            get
            {
                return Groups.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView;

            if (view == null)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.Group, parent, false);

                var imageView = view.FindViewById<ImageView>(Resource.Id.groupImageView);
                var textView = view.FindViewById<TextView>(Resource.Id.groupTextView);

                view.Tag = new GroupViewHolder() 
                {
                    GroupImage = imageView, 
                    GroupName = textView
                };
            }

            var holder = (GroupViewHolder)view.Tag;
            holder.GroupImage.SetImageResource(Resource.Drawable.group);
            holder.GroupName.Text = Groups[position].group_name;

            return view;
        }
    }

    public class GroupViewHolder : Java.Lang.Object
    {
        public ImageView GroupImage { get; set; }
        public TextView GroupName { get; set; }
    }
}
