using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using XamarinAndroidDemo.ViewModel;
using XamarinAndroidDemo.View;

namespace XamarinAndroidDemo
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        UserViewModel userViewModel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            userViewModel = new UserViewModel();
            InitView();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public void InitView()
        {
            EditText emailEditText = FindViewById<EditText>(Resource.Id.mainActivityEmailET);
            EditText passwordEditText = FindViewById<EditText>(Resource.Id.mainActivityPasswordET);
            Button loginButton = FindViewById<Button>(Resource.Id.mainActivityLoginButton);
            loginButton.Click += (sender, e) =>
            {
                string userEmail = emailEditText.Text;
                string userPassword = passwordEditText.Text;

                if (ValidateInputs(userEmail) && ValidateInputs(userPassword))
                {
                    if (userViewModel.CheckUserInDataBase(userEmail, userPassword))
                        OpenListActivity();
                    else
                        ShowUserLoginErrorDialog();
                }
                else
                    ShowInvalidInputDialog();
            };
        }

        public bool ValidateInputs(string input)
        {
            return input != null && !input.Length.Equals(0);
        }

        public void ShowInvalidInputDialog()
        {
            Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alert = dialog.Create();
            alert.SetTitle("Alert!");
            alert.SetMessage("Please enter your email and password");
            alert.SetButton("OK", (c, ev) =>
            {
                alert.Dismiss();
            });
            alert.Show();
        }

        public void ShowUserLoginErrorDialog()
        {
            Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alert = dialog.Create();
            alert.SetTitle("Alert!");
            alert.SetMessage("User not found!");
            alert.SetButton("Retry", (c, ev) =>
            {
                alert.Dismiss();
            });
            alert.Show();
        }

        public void OpenListActivity()
        {
            StartActivity(typeof(UserListActivity));
        }

    }
}