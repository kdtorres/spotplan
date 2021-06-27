using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spotplan
{
    [Activity(Label = "")]
    public class MainContentActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from layout resource
            SetContentView(Resource.Layout.activity_content_main);

            // favorate crypto list btn
            Button btn1 = FindViewById<Button>(Resource.Id.btn1);
            btn1.Click += async (Sender, e) =>
            {
             
                var intent = new Intent(this, typeof(FavorateCryptoActivity));
                await Task.Delay(1000);
                StartActivity(intent);
            };

            // income planner btn
            Button btn2 = FindViewById<Button>(Resource.Id.btn2);
            btn2.Click += async (Sender, e) =>
            {
                await Task.Delay(1000);
                var intent = new Intent(this, typeof(CryptoPlannerActivity));
                StartActivity(intent);
            };

            // crypto prices btn
            Button btn3 = FindViewById<Button>(Resource.Id.btn3);
            btn3.Click += async (Sender, e) =>
            {
                await Task.Delay(1000);
                var intent = new Intent(this, typeof(SignupActivity));
                StartActivity(intent);
            };
        }
    }
}