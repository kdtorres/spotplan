using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Microcharts.Droid;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Microcharts;
using SkiaSharp;
using System.Globalization;
using Android.Views;
using AndroidX.AppCompat.App;
using System;
using static Android.OS.DropBoxManager;

namespace spotplan
{
    [Activity(Label = "Current Prices")]
    public class CryptoPricesActivity : AppCompatActivity
    {
        ChartView chartView;
        TextView priceChartPriceTextView;
        ImageView priceChartLogo;
        private string btcPriceResponse;
        private string ethPriceResponse;
        private string eosPriceResponse;
        private string steemPriceResponse;

        LinearLayout priceHistoryLayout;
        RelativeLayout priceProgressLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_crypto_prices);

            chartView = (ChartView)FindViewById(Resource.Id.chartView);
            priceChartPriceTextView = (TextView)FindViewById(Resource.Id.priceChartPriceTextView);
            priceChartPriceTextView.Click += PriceChartPriceTextView_Click;
            priceChartLogo = (ImageView)FindViewById(Resource.Id.priceChartLogo);

            priceHistoryLayout = (LinearLayout)FindViewById(Resource.Id.priceHistoryLayout);
            priceProgressLayout = (RelativeLayout)FindViewById(Resource.Id.priceProgressLayout);

            FetchChart();
            FetchData();
        }

        private void PriceChartPriceTextView_Click(object sender, System.EventArgs e)
        {
            PopupMenu popupMenu = new PopupMenu(this, priceChartPriceTextView);
            popupMenu.MenuItemClick += PopupMenu_MenuItemClick;

            popupMenu.Menu.Add(IMenu.None, 1, 1, "Bitcoin");
            popupMenu.Menu.Add(IMenu.None, 2, 2, "Ethereum");
            popupMenu.Menu.Add(IMenu.None, 3, 3, "EOS");
            popupMenu.Menu.Add(IMenu.None, 4, 4, "STEEM");

            popupMenu.Show();
        }

        private void PopupMenu_MenuItemClick(object sender, PopupMenu.MenuItemClickEventArgs e)
        {
            if (e.Item.ItemId == 1)
            {
                RedrawChart(btcPriceResponse, "#f7931a");
                priceChartLogo.SetImageResource(Resource.Drawable.btc);
            }
            else if (e.Item.ItemId == 2)
            {
                RedrawChart(ethPriceResponse, "#0f0f0f");
                priceChartLogo.SetImageResource(Resource.Drawable.bnb);
            }
            else if (e.Item.ItemId == 3)
            {
                RedrawChart(eosPriceResponse, "#00a859");
                priceChartLogo.SetImageResource(Resource.Drawable.eth);

            }
            else if (e.Item.ItemId == 4)
            {
                RedrawChart(steemPriceResponse, "#2a56c6");
                priceChartLogo.SetImageResource(Resource.Drawable.xrp);
            }
            else if (e.Item.ItemId == 5)
            {
                RedrawChart(steemPriceResponse, "#2a56c6");
                priceChartLogo.SetImageResource(Resource.Drawable.bch);
            }
            else if (e.Item.ItemId == 6)
            {
                RedrawChart(steemPriceResponse, "#2a56c6");
                priceChartLogo.SetImageResource(Resource.Drawable.ltc);
            }
        }

        async void FetchChart()
        {
            string url = "https://api.roqqu.com/v1/history?symbol=btc";

            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            string jsonResponse = await client.GetStringAsync(url);

            if (!string.IsNullOrEmpty(jsonResponse))
            {
                var resultObject = JObject.Parse(jsonResponse);
                string success = resultObject["success"].ToString();

                if (success != "true")
                {
                    return;
                }

                string dataString = resultObject["data"].ToString();
                var chartData = JsonConvert.DeserializeObject<List<CryptoPriceData>>(dataString);

                List<double> coinPrices = chartData.Select(x => x.price).ToList();
                List<Entry> entries = new List<Entry>();

                //int takeData = 0;
                //foreach (double price in coinPrices)
                //{
                //    takeData++;

                //    Entry newentry = new Entry(float.Parse(price.ToString()))
                //    {
                //        Color = SKColor.Parse("#f7931a"),
                //    };

                //    if (takeData == 2)
                //    {
                //        entries.Add(newentry);
                //        takeData = 0;
                //    }

                //}

                priceChartPriceTextView.Text = coinPrices[coinPrices.Count - 1].ToString("$#,##0.00", CultureInfo.InvariantCulture);
                double lowestPrices = coinPrices.Min();
                var mainChart = new LineChart() { Entries = (IEnumerable<ChartEntry>)entries, LineMode = LineMode.Straight, LineSize = 2f, MinValue = float.Parse(lowestPrices.ToString()), PointMode = PointMode.None };
                mainChart.Margin = 0;
                chartView.Chart = mainChart;

                priceProgressLayout.Visibility = ViewStates.Gone;
                priceHistoryLayout.Visibility = ViewStates.Visible;
            }
        }

        async void FetchData()
        {
            string btcurl = "https://api.roqqu.com/v1/history?symbol=btc";
            string ethurl = "https://api.roqqu.com/v1/history?symbol=eth";
            string eosurl = "https://api.roqqu.com/v1/history?symbol=eos";
            string steemurl = "https://api.roqqu.com/v1/history?symbol=steem";

            var handler = new HttpClientHandler();
            HttpClient client = new HttpClient(handler);
            btcPriceResponse = await client.GetStringAsync(btcurl);
            ethPriceResponse = await client.GetStringAsync(ethurl);
            eosPriceResponse = await client.GetStringAsync(eosurl);
            steemPriceResponse = await client.GetStringAsync(steemurl);
        }

        void RedrawChart(string jsonResponse, string hexcolor)
        {
            if (!string.IsNullOrEmpty(jsonResponse))
            {
                var resultObject = JObject.Parse(jsonResponse);
                string success = resultObject["success"].ToString();

                if (success != "true")
                {
                    return;
                }

                string dataString = resultObject["data"].ToString();
                var chartData = JsonConvert.DeserializeObject<List<CryptoPriceData>>(dataString);

                List<double> coinPrices = chartData.Select(x => x.price).ToList();
                List<Entry> entries = new List<Entry>();

                //int takeData = 0;
                //foreach (double price in coinPrices)
                //{
                //    takeData++;

                //    Entry newentry = new Entry(float.Parse(price.ToString()))
                //    {
                //        Color = SKColor.Parse(hexcolor),
                //    };

                //    if (takeData == 2)
                //    {
                //        entries.Add(newentry);
                //        takeData = 0;
                //    }

                //}

                priceChartPriceTextView.Text = coinPrices[coinPrices.Count - 1].ToString("$#,##0.00", CultureInfo.InvariantCulture);
                double lowestPrices = coinPrices.Min();
                var mainChart = new LineChart() { Entries = (IEnumerable<ChartEntry>)entries, LineMode = LineMode.Straight, LineSize = 2f, MinValue = float.Parse(lowestPrices.ToString()), PointMode = PointMode.None };
                mainChart.Margin = 0;
                chartView.Chart = mainChart;

            }
        }
    }
}