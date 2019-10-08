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
using XamarinAndroidDemo.Model;
using XamarinAndroidDemo.ViewModel;

namespace XamarinAndroidDemo.View
{
    [Activity(Label = "AddUserActivity", Theme = "@style/AppTheme", MainLauncher = false)]
    public class AddUserActivity : AppCompatActivity
    {
        UserViewModel userViewModel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_user_add);
            userViewModel = new UserViewModel();
            InitView();
        }

        public void InitView()
        {
            Button addUserButton = (Button)FindViewById(Resource.Id.addUserActivityAddButton);
            EditText emailEditText = (EditText)FindViewById(Resource.Id.addUserActivityEmailET);
            EditText nameEditText = (EditText)FindViewById(Resource.Id.addUserActivityNameET);
            EditText passwordEditText = (EditText)FindViewById(Resource.Id.addUserActivityPasswordET);
            addUserButton.Click += (sender, e) =>
            {
                if (emailEditText.Text.Length > 4 && nameEditText.Text.Length > 4 && passwordEditText.Text.Length > 4)
                {
                    if (emailEditText.Text.ToLower().Contains('@') && emailEditText.Text.ToLower().Contains('.'))
                    {
                        if (!userViewModel.InsertUserToDataBase(new User(0, nameEditText.Text, emailEditText.Text, passwordEditText.Text)))
                            ShowErrorDialog();
                        else
                            ShowSuccesDialog();
                    } else
                        ShowEmailFormatErrorDialog();
                } else
                    ShowTextLenghtErrorDialog();
                
            };
        }

        public void ShowEmailFormatErrorDialog()
        {
            Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alert = dialog.Create();
            alert.SetTitle("Alert!");
            alert.SetMessage("Use a correct email format.");
            alert.SetButton("Retry", (c, ev) =>
            {
                alert.Dismiss();
            });
            alert.Show();
        }

        public void ShowTextLenghtErrorDialog()
        {
            Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alert = dialog.Create();
            alert.SetTitle("Alert!");
            alert.SetMessage("All data entered must have a minimum of 5 characters.");
            alert.SetButton("Retry", (c, ev) =>
            {
                alert.Dismiss();
            });
            alert.Show();
        }

        public void ShowErrorDialog()
        {
            Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alert = dialog.Create();
            alert.SetTitle("Alert!");
            alert.SetMessage("Error adding the new user.");
            alert.SetButton("Retry", (c, ev) =>
            {
                alert.Dismiss();
            });
            alert.Show();
        }

        public void ShowSuccesDialog()
        {
            Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alert = dialog.Create();
            alert.SetTitle("Alert!");
            alert.SetMessage("User Added!");
            alert.SetButton("Ok", (c, ev) =>
            {
                this.Finish();
            });
            alert.Show();
        }
    }
}