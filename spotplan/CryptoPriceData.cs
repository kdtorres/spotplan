using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spotplan
{
    class CryptoPriceData
    {
        public int id { get; set; }
        public string token { get; set; }
        public double price { get; set; }
        public int _change { get; set; }
        public string currency { get; set; }
        public DateTime created_at { get; set; }
    }
}