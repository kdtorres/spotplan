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

            // add btn
            Button btnadd = FindViewById<Button>(Resource.Id.btnadd);
            btnadd.Click += async (Sender, e) =>
            {
                EditText cryptoid = FindViewById<EditText>(Resource.Id.textInputEditTextID);
                EditText cryptoname = FindViewById<EditText>(Resource.Id.textInputEditTextCryptoName);
                if (cryptoname.Text == "" || cryptoid.Text == "")
                {
                    Toast toast = Toast.MakeText(ApplicationContext, "All fields are requied!", ToastLength.Long);
                    toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                    toast.Show();
                    return;
                }
                else
                {
                    var crud = new Crud();
                    string cid = cryptoid.Text;
                    var crypto_id = await crud.GetCryptoID(cid);
                    var crypto_name = await crud.GetCryptoName(cryptoname.Text);

                    if (crypto_id != null && crypto_name != null)
                    {
                        Toast toast = Toast.MakeText(ApplicationContext, "Crypto already exist!", ToastLength.Long);
                        toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                        toast.Show();
                        return;
                    }
                    else
                    {
                        await crud.AddCrypto(cid, cryptoname.Text);

                        cryptoname.Text = string.Empty;
                        cryptoid.Text = string.Empty;

                        Toast toast = Toast.MakeText(ApplicationContext, "Crypto added successfully!", ToastLength.Long);
                        toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                        toast.Show();

                        //await FetchAllCrypto();
                    }
                }
            };

            // update btn
            Button btnupdate = FindViewById<Button>(Resource.Id.btnupdate);
            btnupdate.Click += async (Sender, e) =>
            {
                EditText cryptoid = FindViewById<EditText>(Resource.Id.textInputEditTextID);
                EditText cryptoname = FindViewById<EditText>(Resource.Id.textInputEditTextCryptoName);
                if (cryptoid.Text == null || cryptoname.Text == null)
                {
                    Toast toast = Toast.MakeText(ApplicationContext, "All fields are requied!", ToastLength.Long);
                    toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                    toast.Show();
                    return;
                }
                else
                {
                    string cid = cryptoid.Text;
                    var crud = new Crud();
                    var crypto_id = await crud.GetCryptoID(cid);
                    var crypto_name = await crud.GetCryptoName(cryptoname.Text);

                    if (crypto_id != null && crypto_name != null)
                    {
                        Toast toast = Toast.MakeText(ApplicationContext, "Crypto with that info is already exist!", ToastLength.Long);
                        toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                        toast.Show();
                        return;
                    }
                    else
                    {
                        await crud.UpdateCryto(cid, cryptoname.Text);

                        cryptoname.Text = string.Empty;
                        cryptoid.Text = string.Empty;

                        Toast toast = Toast.MakeText(ApplicationContext, "Crypto updated successfully!", ToastLength.Long);
                        toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                        toast.Show();

                        await FetchAllCrypto();
                    }
                }
            };

            // btn delete 
            Button btndelete = FindViewById<Button>(Resource.Id.btndelete);
            btndelete.Click += async (Sender, e) =>
            {
                EditText cryptoid = FindViewById<EditText>(Resource.Id.textInputEditTextID);
                EditText cryptoname = FindViewById<EditText>(Resource.Id.textInputEditTextCryptoName);

                if (cryptoid.Text != null)
                {
                    Toast toast = Toast.MakeText(ApplicationContext, "Only Crypto ID required!", ToastLength.Long);
                    toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                    toast.Show();
                    return;
                }
                
                if (cryptoid.Text == null)
                {
                    Toast toast = Toast.MakeText(ApplicationContext, "Required crypto ID!", ToastLength.Long);
                    toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                    toast.Show();
                    return;
                }
                else
                {
                    var crud = new Crud();
                    string cid = cryptoid.Text;
                    await crud.DeleteCrypto(cid);

                    cryptoname.Text = string.Empty;
                    cryptoid.Text = string.Empty;

                    Toast toast = Toast.MakeText(ApplicationContext, "Deleted successfully!", ToastLength.Long);
                    toast.SetGravity(GravityFlags.CenterHorizontal, 0, 0);
                    toast.Show();

                    await FetchAllCrypto();
                }
            };

             // btn back 
            TextView btnstrback = FindViewById<TextView>(Resource.Id.textViewbackfca);
            btnstrback.Click += (Sender, e) =>
            {
                var intent = new Intent(this, typeof(MainContentActivity));
                StartActivity(intent);
            };


        } // end onCreate

        // list all crypto favorate
        public async Task FetchAllCrypto()
        {
            var crud = new Crud();
            var allPersons = await crud.GetAllCrypto();

            ListView list = FindViewById<ListView>(Resource.Id.listView1);
            //ListView list = FindViewById<ListView>(Resource.Id.listVie);
            list.ContentDescriptionFormatted = (Java.Lang.ICharSequence)allPersons;
        }

    }
}