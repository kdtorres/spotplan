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

namespace spotplan
{
    [Activity(Label = "@string/app_login_heading", Theme = "@style/Theme.MaterialComponents")]
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

                string em = email.Text;
                string p = pass.Text;

                if (email.Text == "" || pass.Text == "")
                {
                    Toast toast = Toast.MakeText(ApplicationContext, "All fields are requied!", ToastLength.Short);
                    toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                    toast.Show();

                    var intent = new Intent(this, typeof(LoginActivity));
                    await Task.Delay(1000);
                    StartActivity(intent);
                }

                 var user = await GetUser(em);

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
                }
               
            };


            // get user method
            static async Task<SignupModels> GetUser(string email)
            {
                // connect to firebase using API (URI)
                FirebaseClient firebase = new FirebaseClient("https://spotplan-default-rtdb.firebaseio.com/");
                
                var allUsers = await ListUser();
                await firebase.Child("Users").OnceAsync<SignupModels>();
                return allUsers.Where(a => a.Email == email).FirstOrDefault();
            }


            // retrieve all users
            static async Task<List<SignupModels>> ListUser()
            {
                // connect to firebase using API (URI)
                FirebaseClient firebase = new FirebaseClient("https://spotplan-default-rtdb.firebaseio.com/");
               
                var userlist = (await firebase.Child("Users").OnceAsync<SignupModels>()).Select(item => new SignupModels {
                    Firstname = item.Object.Firstname,
                    Lastname = item.Object.Lastname,
                    Email = item.Object.Email,
                    Password = item.Object.Password
                }).ToList();

                return userlist;
            }
            /*

            // login method
            static async Task<IEnumerable<SignupModels>> LoginUsers(string email, string password)
            {
                // connect to firebase using API (URI)
                FirebaseClient firebase = new FirebaseClient("https://spotplan-default-rtdb.firebaseio.com/");

                LoginModels l = new LoginModels();
                l.Email = email;
                l.Password = password;
                var e = l.Email;
                var p = l.Password;

                var allUsers = await ListUser();
                await firebase.Child("Users").OnceAsync<SignupModels>();
                return (IEnumerable<SignupModels>)allUsers.Where(a => a.Email == e && a.Password == p).FirstOrDefault();
            }*/
        }
    }
}