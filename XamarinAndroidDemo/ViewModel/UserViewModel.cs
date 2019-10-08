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
using XamarinAndroidDemo.DataBase;
using XamarinAndroidDemo.Model;

namespace XamarinAndroidDemo.ViewModel
{
    class UserViewModel
    {
        UserDataBase userDataBase;

        public UserViewModel()
        {
            userDataBase = new UserDataBase();
        }

        public bool InsertUserToDataBase(User newUser)
        {
            return userDataBase.DataBaseInsertUser(newUser);
        }

        public bool DeleteUserIDataBase(User newUser)
        {
            return userDataBase.DataBaseDeleteUser(newUser);
        }

        public bool UpadateUserInDataBase(User newUser)
        {
            return userDataBase.DataBaseUpdateUser(newUser);
        }

        public List<User> GetUsersFromDataBase()
        {
            return userDataBase.DataBaseGetUsers();
        }

        public bool CheckUserInDataBase(string email, string password)
        {
            return userDataBase.DataBaseCheckUser(email, password);
        }
    }
}