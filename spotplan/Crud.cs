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
    class Crud
    {
       
            // GetAllCrypto method
            public async Task<List<CryptoList>> GetAllCrypto()
            {
                // firebase initialization
                FirebaseClient firebase = new FirebaseClient("https://spotplan-default-rtdb.firebaseio.com/");

                return (await firebase
                    .Child("FavorateCrypto")
                    .OnceAsync<CryptoList>()).Select(item => new CryptoList
                    {
                        CryptoID = item.Object.CryptoID,
                        CryptoName = item.Object.CryptoName
                    }).ToList();
            }

            //  AddCrypto method
            public async Task AddCrypto(string crypto_id, string name)
            {
                // firebase initialization
                FirebaseClient firebase = new FirebaseClient("https://spotplan-default-rtdb.firebaseio.com/");

                await firebase.Child("FavorateCrypto").PostAsync(new CryptoList() { CryptoID=crypto_id, CryptoName=name});
            }

            // GetCryptoid method
            public async Task<CryptoList> GetCryptoID(string crypto_id)
            {
                // firebase initialization
                FirebaseClient firebase = new FirebaseClient("https://spotplan-default-rtdb.firebaseio.com/");

                var allcrypto = await GetAllCrypto();
                await firebase
                    .Child("FavorateCrypto")
                    .OnceAsync<CryptoList>();
                return allcrypto.FirstOrDefault(a => a.CryptoID == crypto_id);
            }

            // GetCryptoname method
            public async Task<CryptoList> GetCryptoName(string cryptoName)
            {
                // firebase initialization
                FirebaseClient firebase = new FirebaseClient("https://spotplan-default-rtdb.firebaseio.com/");

                var allcrypto = await GetAllCrypto();
                await firebase
                    .Child("FavorateCrypto")
                    .OnceAsync<Person>();
                return allcrypto.FirstOrDefault(a => a.CryptoName == cryptoName);
            }

            // UpdateCryto method 
            public async Task UpdateCryto(string crypto_id, string cryptoName)
            {
                // firebase initialization
                FirebaseClient firebase = new FirebaseClient("https://spotplan-default-rtdb.firebaseio.com/");

                var toUpdateCrypto = (await firebase
                    .Child("FavorateCrypto")
                    .OnceAsync<CryptoList>()).FirstOrDefault(a => a.Object.CryptoID == crypto_id);

                await firebase
                    .Child("FavorateCrypto")
                    .Child(toUpdateCrypto.Key)
                    .PutAsync(new CryptoList() { CryptoID = crypto_id, CryptoName = cryptoName});
            }

            // DeleteCrypto method
            public async Task DeleteCrypto(string crypto_id)
            {
                // firebase initialization
                FirebaseClient firebase = new FirebaseClient("https://spotplan-default-rtdb.firebaseio.com/");

                var toDeleteCrypto = (await firebase
                    .Child("FavorateCrypto")
                    .OnceAsync<CryptoList>()).FirstOrDefault(a => a.Object.CryptoID == crypto_id);
                await firebase.Child("FavorateCrypto").Child(toDeleteCrypto.Key).DeleteAsync();
            }
        }
    
}