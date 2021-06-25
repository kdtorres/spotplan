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
    [Activity(Label = "@string/app_login_heading", Theme = "@style/AppTheme")]
    public class LoginActivity : AppCompatActivity
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
            back.Click += async (Sender, e) =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                await Task.Delay(1000);
                StartActivity(intent);
            };


            // login button
            Button login = FindViewById<Button>(Resource.Id.btn_login);
            login.Click += async (Sender, e) =>
            {
                EditText email = FindViewById<EditText>(Resource.Id.uname_login);
                EditText pass = FindViewById<EditText>(Resource.Id.password_login);

                LoginModel m = new LoginModel();
                m.Email = email.Text;
                m.Password = pass.Text;

                if (email.Text == "" || pass.Text == "")
                {
                    Toast toast = Toast.MakeText(ApplicationContext, "All fields are requied!", ToastLength.Short);
                    toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                    toast.Show();

                    var intent = new Intent(this, typeof(LoginActivity));
                    await Task.Delay(1000);
                    StartActivity(intent);
                } 
                else
                {
                    Login l = new Login();
                    l.LoginUser();

                    LoginModel lm = new LoginModel();
                    if(lm.flag == false)
                    {
                        Toast toast = Toast.MakeText(ApplicationContext, "User doest not exist!", ToastLength.Short);
                        toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                        toast.Show();

                        await Task.Delay(1000);
                        var intent = new Intent(this, typeof(LoginActivity));
                        StartActivity(intent);
                    }else
                    {
                        Toast toast = Toast.MakeText(ApplicationContext, "Login successfully!", ToastLength.Short);
                        toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                        toast.Show();

                        await Task.Delay(1000);
                        var intent = new Intent(this, typeof(CryptoActivity));
                        StartActivity(intent);
                    }
                }
                /* trial code
                if (user != null)
                {
                    if (em == user.Email && p == user.Password)
                    {
                        Toast toast = Toast.MakeText(ApplicationContext, "Login Sucess.", ToastLength.Short);
                        toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                        toast.Show();

                        var intent = new Intent(this, typeof(CryptoActivity));
                        await Task.Delay(1000);
                        StartActivity(intent);
                    }
                    else
                    {
                        Toast toast = Toast.MakeText(ApplicationContext, "Please enter correct email and password!.", ToastLength.Short);
                        toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                        toast.Show();

                        var intent = new Intent(this, typeof(LoginActivity));
                        await Task.Delay(1000);
                        StartActivity(intent);
                    }
                   
                }
                else
                {
                    Toast toast = Toast.MakeText(ApplicationContext, "Sorry, login failed!", ToastLength.Short);
                    toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                    toast.Show();

                    var intent = new Intent(this, typeof(LoginActivity));
                    await Task.Delay(1000);
                    StartActivity(intent);
                }*/

            };

            /* trial code
            // get user method
            static async Task<Users> GetUser(string email)
            {
                // connect to firebase using API (URI)
                FirebaseClient firebase = new FirebaseClient("https://spotplan-default-rtdb.firebaseio.com/");

                var allUsers = await ListUser();
                await firebase.Child("Users").OnceAsync<Users>();
                return allUsers.Where(a => a.Email == email).FirstOrDefault();
            }


            // retrieve all users
            static async Task<List<Users>> ListUser()
            {
                // connect to firebase using API (URI)
                FirebaseClient firebase = new FirebaseClient("https://spotplan-default-rtdb.firebaseio.com/");

                var userlist = (await firebase.Child("Users").OnceAsync<Users>()).Select(item => new Users
                {
                    Firstname = item.Object.Firstname,
                    Lastname = item.Object.Lastname,
                    Email = item.Object.Email,
                    Password = item.Object.Password
                }).ToList();

                return userlist;
            }*/

        
        // get especific users
        //public async void Login(string email, string password)
        //{
        
            //try
            //{
            //    // to firebase realtime database
            //    FirebaseClient firebase = new FirebaseClient("https://spotplan-default-rtdb.firebaseio.com/");
            //    var eu = await LoginAuth.ge();
            //    await firebase.Child("Users").OnceAsync<Users>();
            //    eu.Where(a => a.Email == email && a.Password == password).FirstOrDefault();

            //    if(eu != null)
            //    {
            //        Toast toast = Toast.MakeText(ApplicationContext, "Login successfully!", ToastLength.Short);
            //        toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
            //        toast.Show();

            //        await Task.Delay(1000);
            //        var intent = new Intent(this, typeof(CryptoActivity));
            //        StartActivity(intent);
            //    }

            //}
            //catch
            //{
            //    Toast toast = Toast.MakeText(ApplicationContext, "User does not exist!", ToastLength.Short);
            //    toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
            //    toast.Show();

            //    await Task.Delay(1000);
            //    var intent = new Intent(this, typeof(LoginActivity));
            //    StartActivity(intent);
            //}


            //if (user != null)
            //{
            //    Toast toast = Toast.MakeText(ApplicationContext, "Login successfully!", ToastLength.Short);
            //    toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
            //    toast.Show();

            //    await Task.Delay(1000);
            //    var intent = new Intent(this, typeof(CryptoActivity));
            //    StartActivity(intent);
            //} 
            //else
            //{
            //    Toast toast = Toast.MakeText(ApplicationContext, "User doest not exist!", ToastLength.Short);
            //    toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
            //    toast.Show();

            //    await Task.Delay(1000);
            //    var intent = new Intent(this, typeof(LoginActivity));
            //    StartActivity(intent);
            //}
        }

    }
}