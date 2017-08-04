using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Support.V7.Widget;
using Dental_IT.Droid.Adapters;
using System.Threading.Tasks;
using System.Json;
using System.Net;
using System.IO;
using System;
using Dental_IT.Model;
using Android.Widget;

namespace Dental_IT.Droid.Fragments
{
    public class SearchTreatmentFragment : Android.Support.V4.App.Fragment
    {
        private List<Treatment> treatmentList = new List<Treatment>();
        private RecyclerView searchTreatment_RecyclerView;
        private static RecyclerViewAdapter_SearchTreatment adapter;
        private static List<Treatment> tempTreatmentList = new List<Treatment>();
        private static Xamarin.RangeSlider.RangeSliderControl slider;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (container == null)
            {
                return null;
            }

            View v = inflater.Inflate(Resource.Layout.Search_Treatment, container, false);

            searchTreatment_RecyclerView = v.FindViewById<RecyclerView>(Resource.Id.searchTreatment_RecyclerView);
            slider = v.FindViewById<Xamarin.RangeSlider.RangeSliderControl>(Resource.Id.slider);

            //  Configure range slider
            slider.TextFormat = "$0";
            slider.SetSelectedMaxValue(slider.GetAbsoluteMaxValue());

            //  Get all the treatments from database
            Task.Run(async () =>
            {
                try
                {
                    treatmentList.Clear();
                    tempTreatmentList.Clear();
                    
                    string url = Web_Config.global_connURL_getTreatment;

                    //  Get json value by passing the URL
                    JsonValue json = await GetTreatments(url);

                    //  Create objects from json value and populate lists
                    foreach (JsonObject obj in json)
                    {
                        System.Diagnostics.Debug.WriteLine("Obj: " + obj.ToString());

                        Treatment treatment = new Treatment()
                        {
                            ID = Int32.Parse(obj["ID"]),
                            TreatmentName = obj["TreatmentName"],
                            TreatmentDesc = obj["TreatmentDesc"],
                            Price = obj["Price"],
                            Price_d = obj["Price_d"],
                            PriceLow = Double.Parse(obj["PriceLow"]),
                            PriceHigh = Double.Parse(obj["PriceHigh"])
                        };

                        treatmentList.Add(treatment);
                        tempTreatmentList.Add(treatment);
                    }

                    Activity.RunOnUiThread(() =>
                    {
                        //  Configure custom adapter for recyclerview
                        searchTreatment_RecyclerView.SetLayoutManager(new LinearLayoutManager(Activity));

                        adapter = new RecyclerViewAdapter_SearchTreatment(Activity, treatmentList);
                        searchTreatment_RecyclerView.SetAdapter(adapter);
                    });
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.Write("Obj: " + e.Message + e.StackTrace);
                }
            });

            return v;
        }

        //  Search filter logic
        public static void filter(string query)
        {
            string lowerCaseQuery = query.ToLower();
            int minPrice = (int)slider.GetSelectedMinValue();
            int maxPrice = (int)slider.GetSelectedMaxValue();

            List<Treatment> filteredList = new List<Treatment>();

            foreach (Treatment treatment in tempTreatmentList)
            {
                string text = treatment.TreatmentName.ToLower();
                if (text.Contains(lowerCaseQuery) && minPrice <= treatment.PriceLow && maxPrice >= treatment.PriceHigh)
                {
                    filteredList.Add(treatment);
                }
            }

            adapter.Replace(filteredList);
        }

        // Gets Treatments data from the passed URL.
        private async Task<JsonValue> GetTreatments(string url)
        {
            try
            {
                // Create an HTTP web request using the URL:
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
                request.ContentType = "application/json";
                request.Method = "GET";

                // Send the request to the server and wait for the response:
                using (WebResponse response = request.GetResponse())
                {
                    // Get a stream representation of the HTTP web response:
                    using (Stream stream = response.GetResponseStream())
                    {
                        // Use this stream to build a JSON document object:
                        JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                        System.Diagnostics.Debug.WriteLine("JSON doc: " + jsonDoc.ToString());

                        // Return the JSON document:
                        return jsonDoc;
                    }
                }
            }
            catch (WebException e)
            {
                System.Diagnostics.Debug.Write("JSON doc: " + e.Message);
                return new JsonArray();
            }
        } // End of GetTreatments() method
    }
}