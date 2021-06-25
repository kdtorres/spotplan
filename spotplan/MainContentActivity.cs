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
    [Activity(Label = "MainContentActivity")]
    public class MainContentActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from layout resource
            SetContentView(Resource.Layout.activity_content_main);

              
            
            Button btn = FindViewById<Button>(Resource.Id.btn1);
            btn.Click += async (Sender, e) =>
            {
                await Task.Delay(1000);
                var intent = new Intent(this, typeof(SignupActivity));
                StartActivity(intent);
            };
        }
    }
}