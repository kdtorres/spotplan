using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DocumentFormat.OpenXml.Spreadsheet;
using Firebase.Database;
using Firebase.Database.Query;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spotplan
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from layout resource
            SetContentView(Resource.Layout.activity_login);

            // Create your application here
            // back
            TextView back = FindViewById<TextView>(Resource.Id.text_back);
            back.Click += (Sender, e) =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };


            // login button
            Button login = FindViewById<Button>(Resource.Id.btn_login);
            login.Click += (Sender, e) =>
            {
                EditText email = FindViewById<EditText>(Resource.Id.uname_login);
                EditText pass = FindViewById<EditText>(Resource.Id.password_login);
                string em = email.Text;
                string p = pass.Text;
                var users_login = Login(em, p);

                if(users_login != null)
                {
                    var intent = new Intent(this, typeof(CryptoActivity));
                    StartActivity(intent);
                }
                else
                {

                }

            };


            // login method
            static async Task<IEnumerable<SignupModels>> Login(string em, string p)
            {
               // connect to firebase using API (URI)
               FirebaseClient firebase = new FirebaseClient("https://spotplan-default-rtdb.firebaseio.com/");

                var users = (await firebase.Child("Users").OnceAsync<SignupModels>()).Select(item => new SignupModels {
                    Firstname = item.Object.Firstname,
                    Lastname = item.Object.Lastname,
                    Email = item.Object.Email,
                    Password = item.Object.Password
                }).Where(item => item.Email == em && item.Password == p);

                return users;
            }
        }
    }
}