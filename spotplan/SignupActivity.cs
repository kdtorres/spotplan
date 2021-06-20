using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spotplan
{
    [Activity(Label = "SignupActivity")]
    public class SignupActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from layout resource
            SetContentView(Resource.Layout.activity_signup);
            
            // back 
            TextView back = FindViewById<TextView>(Resource.Id.text_back);
            back.Click += (Sender, e) =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };

            // signup button
            Button signup = FindViewById<Button>(Resource.Id.btn_signup);
            signup.Click += (Sender, e) =>
            {
                EditText fname = FindViewById<EditText>(Resource.Id.fname_signup);
                EditText lname = FindViewById<EditText>(Resource.Id.lname_signup);
                EditText email = FindViewById<EditText>(Resource.Id.email_signup);
                EditText pass = FindViewById<EditText>(Resource.Id.password_signup);
                string f = fname.Text;
                string l= lname.Text;
                string em = email.Text;
                string p = pass.Text;
                Register(f, l, em, p);
            };

            // register method
            static void Register(string f, string l, string em, string p)
            {
                // connect to firebase using API (URI)
                FirebaseClient firebase = new FirebaseClient("https://spotplan-default-rtdb.firebaseio.com/");

                // insert
                firebase.Child("Users").PutAsync(new UserSignup() { Firstname = f, Lastname = l, Email = em, Password = p });
                
            }
        }
    }
}