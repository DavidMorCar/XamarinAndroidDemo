using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamarinAndroidDemo.Model;

namespace XamarinAndroidDemo.Adapter
{
    class UserListAdapter : BaseAdapter<User>
    {

        Activity context;
        List<User> list;

        public UserListAdapter(Activity _context, List<User> _list)
            : base()
        {
            this.context = _context;
            this.list = _list;
        }

        public override int Count
        {
            get { return list.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Android.Views.View GetView(int position, Android.Views.View convertView, ViewGroup parent)
        {
            Android.Views.View view = convertView;

            // re-use an existing view, if one is available
            // otherwise create a new one
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.item_user, parent, false);

            User item = this[position];
            view.FindViewById<TextView>(Resource.Id.userItemPassword).Text = item.UserPassword;
            view.FindViewById<TextView>(Resource.Id.userItemName).Text = item.UserName;
            view.FindViewById<TextView>(Resource.Id.userItemEmail).Text = item.UserEmail;

            return view;
        }

        public override User this[int index]
        {
            get { return list[index]; }
        }

        
    }
}