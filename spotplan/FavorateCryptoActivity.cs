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
    [Activity(Label = "My Favorate Cryptocurrecies")]
    public class FavorateCryptoActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from layout resource
            SetContentView(Resource.Layout.activity_favorate_crypto);
            // Create your application here
            Button btnadd = FindViewById<Button>(Resource.Id.btnadd);
            btnadd.Click += async (Sender, e) =>
            {
                EditText cryptoname = FindViewById<EditText>(Resource.Id.textInputEditTextCryptoName);
                if(cryptoname.Text == "")
                {
                    Toast toast = Toast.MakeText(ApplicationContext, "Fields are requied!", ToastLength.Short);
                    toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                    toast.Show();
                    return;
                } 
                else
                {
                    var crud = new Crud();
                    var crypto = await crud.GetCrypto(cryptoname.Text);

                    if (crypto != null)
                    {
                        Toast toast = Toast.MakeText(ApplicationContext, "Crypto already exist!", ToastLength.Short);
                        toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                        toast.Show();
                        return;
                    }
                    else
                    {
                        await crud.AddCrypto(cryptoname.Text);

                        cryptoname.Text = string.Empty;

                        Toast toast = Toast.MakeText(ApplicationContext, "Crypto added successfully!", ToastLength.Short);
                        toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                        toast.Show();

                        await FetchAllCrypto();
                    }  
                }
            };

        }

        public async Task FetchAllCrypto()
        {
            var crud = new Crud();
            var allPersons = await crud.GetAllCrypto();

            ListView list = FindViewById<ListView>(Resource.Id.listViewCrypto);
            list.ContentDescriptionFormatted = (Java.Lang.ICharSequence)allPersons;
        }
    }
}