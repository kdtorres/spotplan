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
    [Activity(Label = "Crypto Income Planner")]
    public class CryptoPlannerActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from layout resource
            SetContentView(Resource.Layout.activity_crypto_income_planner);
            // Create your application here
            Button btn1 = FindViewById<Button>(Resource.Id.btncalcu);
            btn1.Click += async (Sender, e) =>
            {
                EditText ep = FindViewById<EditText>(Resource.Id.textInputEditTextentry);
                EditText exitp = FindViewById<EditText>(Resource.Id.textInputEditTextexit);
                EditText amount = FindViewById<EditText>(Resource.Id.textInputEditTextbid);
                Button pnl = FindViewById<Button>(Resource.Id.btnamount);
                if (ep.Text == "" || exitp.Text == "" || amount.Text == "")
                {
                    Toast toast = Toast.MakeText(ApplicationContext, "All fields are required!", ToastLength.Long);
                    toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                    toast.Show();
                    return;
                }
                else
                {
                    var EN_EXP = (float.Parse(exitp.Text) / float.Parse(ep.Text)) - 1;
                    var INITIAL = EN_EXP * float.Parse(amount.Text);
                    var TOTAL = INITIAL + float.Parse(amount.Text);

                    pnl.Text = "₱ " + TOTAL.ToString();

                    await Task.Delay(9000);
                }
            };

            TextView btnback = FindViewById<TextView>(Resource.Id.textViewback);
            btnback.Click += (Sender, e) =>
            {
                var intent = new Intent(this, typeof(MainContentActivity));
                StartActivity(intent);
            };
        }
    }
}