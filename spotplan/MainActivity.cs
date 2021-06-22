using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using System.Threading.Tasks;

namespace spotplan
{
    [Activity(Label = "@string/app_main_heading", Theme = "@style/Theme.MaterialComponents", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);  
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from layout resource
            SetContentView(Resource.Layout.activity_main);


            // Create your application here
            // signup btn
            Button btn = FindViewById<Button>(Resource.Id.btnmain_signup);
            btn.Click += async (Sender, e) =>
            {
                var intent = new Intent(this, typeof(SignupActivity));
                await Task.Delay(1000);
                StartActivity(intent);
            };


            // login btn
            Button btn2 = FindViewById<Button>(Resource.Id.btnlogin_main);
            btn2.Click += async (Sender, e) =>
            {
                var intent = new Intent(this, typeof(LoginActivity));
                await Task.Delay(1000);
                StartActivity(intent);
            };
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}