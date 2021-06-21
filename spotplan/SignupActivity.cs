using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Firebase.Database;
using Firebase.Database.Query;
using System.Threading.Tasks;

namespace spotplan
{
    [Activity(Label = "SignupActivity")]
    public class SignupActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from layout resource
            SetContentView(Resource.Layout.activity_signup);


            // Create your application here
            // back 
            TextView back = FindViewById<TextView>(Resource.Id.text_back);
            back.Click += async (Sender, e) =>
            {
                var intent = new Intent(this, typeof(MainActivity));
                await Task.Delay(1000);
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
                string l = lname.Text;
                string em = email.Text;
                string p = pass.Text;

                if (fname.Text == "" || lname.Text == "" || email.Text == "" || pass.Text == "")
                {
                    Toast toast = Toast.MakeText(ApplicationContext, "All fields are requied!", ToastLength.Short);
                    toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                    toast.Show();
                } 
                else
                {
                    Register(f, l, em, p);
                }
            };

         
            // register method
            void Register(string f, string l, string em, string p)
            {
                // connect to firebase using API (URI)
                FirebaseClient firebase = new FirebaseClient("https://spotplan-default-rtdb.firebaseio.com/");

                // insert to firebase realtime database
                firebase.Child("Users").PutAsync(new SignupModels() { Firstname = f, Lastname = l, Email = em, Password = p });

                Toast toast = Toast.MakeText(ApplicationContext, "Successfully signup!", ToastLength.Short);
                toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                toast.Show();
            }
        }
    }
}