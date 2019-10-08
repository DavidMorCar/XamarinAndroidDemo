using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using XamarinAndroidDemo.Adapter;
using XamarinAndroidDemo.Model;
using XamarinAndroidDemo.ViewModel;

namespace XamarinAndroidDemo.View
{
    [Activity(Label = "UserList", Theme = "@style/AppTheme", MainLauncher = false)]
    public class UserListActivity : AppCompatActivity
    {
        ListView listView;
        List<User> users;
        UserViewModel userViewModel;

        [Obsolete]
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_list_user);
            userViewModel = new UserViewModel();
            InitView();
        }

        protected override void OnStart()
        {
            base.OnStart();
            users = userViewModel.GetUsersFromDataBase();
            if (users != null)
                listView.Adapter = new UserListAdapter(this, users);
        }

        [Obsolete]
        public void InitView()
        {
            listView = (ListView)FindViewById(Resource.Id.userListActivityListView);
            listView.Clickable = false;
            users = userViewModel.GetUsersFromDataBase();
            listView.Adapter = new UserListAdapter(this, users);
            Button addUserButton = (Button)FindViewById(Resource.Id.userListActivityAddButton);
            addUserButton.Click += (sender, e) =>
            {
                OpenAddUserActivity();
            };
        }
    

        [Obsolete]
        public void OpenAddUserActivity()
        {
            StartActivity(typeof(AddUserActivity));
        }
    }
}