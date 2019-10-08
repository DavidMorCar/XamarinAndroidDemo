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
using SQLite;
using XamarinAndroidDemo.Model;

namespace XamarinAndroidDemo.DataBase
{
    class UserDataBase
    {
        private SQLiteConnection GetDataBase()
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string routeDB = System.IO.Path.Combine(folder, "userDB.db");

            // Create the database if it does not exist and create a connection
            var db = new SQLiteConnection(routeDB);

            // Create the table if it does not exist
            db.CreateTable<User>();

            // Create the default user
            var defaultUser = new User(1, "Admin", "Admin", "1234");
            db.InsertOrReplace(defaultUser);

            return db;
        }

        public bool DataBaseInsertUser(User newUser)
        {
            var random = new Random();
            newUser.UserId = random.Next();
            try
            {
                var db = GetDataBase();
                db.InsertOrReplace(newUser);
            }
            catch (SQLiteException sqlex)
            {
                System.Diagnostics.Debug.WriteLine(sqlex.Message);
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public List<User> DataBaseGetUsers()
        {
            List<User> users = new List<User>();
            try
            {
                var db = GetDataBase();
                users = db.Table<User>().ToList();
            }
            catch (SQLiteException sqlex)
            {
                System.Diagnostics.Debug.WriteLine(sqlex.Message);
                return null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return null;
            }

            return users;
        }

        public bool DataBaseUpdateUser(User newUser)
        {
            try
            {
                var db = GetDataBase();
                db.InsertOrReplace(newUser);
            }
            catch (SQLiteException sqlex)
            {
                System.Diagnostics.Debug.WriteLine(sqlex.Message);
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public bool DataBaseDeleteUser(User newUser)
        {
            try
            {
                var db = GetDataBase();
                db.Delete(newUser);
            }
            catch (SQLiteException sqlex)
            {
                System.Diagnostics.Debug.WriteLine(sqlex.Message);
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public bool DataBaseCheckUser(string email, string password)
        {
            List<User> users = new List<User>();
            try
            {
                var db = GetDataBase();
                users = db.Table<User>().ToList();
            }
            catch (SQLiteException sqlex)
            {
                System.Diagnostics.Debug.WriteLine(sqlex.Message);
                return false;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }

            return CheckEmail(email, users) && CheckPassword(password, users);

        }

        public bool CheckEmail(string email, List<User> users)
        {
            foreach (User user in users)
            {
                if (email.Equals(user.UserEmail))
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckPassword(string password, List<User> users)
        {
            foreach (User user in users)
            {
                if (password.Equals(user.UserPassword))
                {
                    return true;
                }
            }
            return false;
        }
    }
}