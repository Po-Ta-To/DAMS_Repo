using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Support.V7.Widget;
using Dental_IT.Droid.Adapters;
using System.Threading.Tasks;
using System;
using Dental_IT.Model;

namespace Dental_IT.Droid.Fragments
{
    public class SearchTreatmentFragment : Android.Support.V4.App.Fragment
    {
        private List<Treatment> treatmentList = new List<Treatment>();
        private RecyclerView searchTreatment_RecyclerView;
        private static RecyclerViewAdapter_SearchTreatment adapter;
        private static List<Treatment> tempTreatmentList = new List<Treatment>();
        private static Xamarin.RangeSlider.RangeSliderControl slider;

        API api = new API();

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

            //  Main data retrieving + processing method
            Task.Run(async () =>
            {
                try
                {
                    treatmentList.Clear();
                    tempTreatmentList.Clear();

                    //  Get treatments
                    treatmentList = await api.GetTreatments();

                    foreach (Treatment treatment in treatmentList)
                    {
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
    }
}