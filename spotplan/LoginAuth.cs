using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Firebase.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AndroidX.AppCompat.App;
using Android.Views;
using Xamarin.Essentials;
using System;

namespace spotplan
{
    class LoginAuth : LoginActivity
    {
        // user list method
        public static async Task<List<Users>> UserList()
        {
            // firebase initialization
            FirebaseClient firebase = new FirebaseClient("https://spotplan-default-rtdb.firebaseio.com/");

            var userlist = (await firebase.Child("Users").OnceAsync<Users>()).Select(item =>
               new Users
               {
                   UserID = item.Object.UserID,
                   Firstname = item.Object.Firstname,
                   Lastname = item.Object.Lastname,
                   Email = item.Object.Email,
                   Password = item.Object.Password
               }).ToList();
            return userlist;
        }


        //// get especific users
        //public static async Task<Users> EspecificUser(Guid userid, string email, string password)
        //{
        //    // firebase initialization
        //    FirebaseClient firebase = new FirebaseClient("https://spotplan-default-rtdb.firebaseio.com/");

        //    var user = await UserList();
        //    await firebase.Child("Users").OnceAsync<Users>();
        //    return user.Where(a => a.UserID == userid && a.Email == email && a.Password == password).FirstOrDefault();
        //}
    }
}