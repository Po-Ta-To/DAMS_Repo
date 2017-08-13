using System.Collections.Generic;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Support.V7.Widget;
using Android.Preferences;
using Dental_IT.Droid.Adapters;
using Dental_IT.Model;
using System.Threading.Tasks;
using System;

namespace Dental_IT.Droid.Fragments
{
    public class SearchHospitalFragment : Android.Support.V4.App.Fragment
    {
        private List<Hospital> hospitalList = new List<Hospital>();
        private List<int> prefList = new List<int>();
        private List<ToggleState> tempFavouriteList = new List<ToggleState>();
        private RecyclerView searchHospital_RecyclerView;
        private static RecyclerViewAdapter_SearchHospital adapter;
        private static List<Hospital> tempHospitalList = new List<Hospital>();

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

            View v = inflater.Inflate(Resource.Layout.Search_Hospital, container, false);
            
            searchHospital_RecyclerView = v.FindViewById<RecyclerView>(Resource.Id.searchHospital_RecyclerView);
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Activity);

            //  Main data retrieving + processing method
            Task.Run(async () =>
            {
                try
                {
                    hospitalList.Clear();
                    tempHospitalList.Clear();

                    //  Get hospitals
                    hospitalList = await api.GetClinicHospitals();

                    foreach (Hospital hospital in hospitalList)
                    {
                        tempHospitalList.Add(hospital);
                    }

                    //  If shared preferences contains favourites
                    if (prefs.Contains("favourites"))
                    {
                        //  Retrieve list of hospital ids that are favourited
                        prefList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(prefs.GetString("favourites", "null"));

                        //  Create a temporary list of favourites with all the hospitals
                        foreach (Hospital hosp in hospitalList)
                        {
                            ToggleState tempFav = new ToggleState(hosp.ID);

                            //  Set favourited to true if hospital id corresponds with id in shared preferences
                            if (prefList.Exists(e => (e.Equals(hosp.ID))))
                            {
                                tempFav.toggled = true;
                            }

                            tempFavouriteList.Add(tempFav);
                        }
                    }

                    //  Else if shared preferences is empty, create a temporary list of favourites with all the hospitals, setting all favourited to false by default
                    else
                    {
                        foreach (Hospital hosp in hospitalList)
                        {
                            tempFavouriteList.Add(new ToggleState(hosp.ID));
                        }
                    }

                    Activity.RunOnUiThread(() =>
                    {
                        //  Configure custom adapter for recyclerview
                        searchHospital_RecyclerView.SetLayoutManager(new LinearLayoutManager(Activity));

                        adapter = new RecyclerViewAdapter_SearchHospital(Activity, hospitalList, prefList, tempFavouriteList);
                        searchHospital_RecyclerView.SetAdapter(adapter);
                    });
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.Write("Obj: " + e.Message + e.StackTrace);
                }
            });

            return v;
        }

        public override void OnPause()
        {
            base.OnPause();

            //  Save favourites to shared preferences
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Activity);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString("favourites", Newtonsoft.Json.JsonConvert.SerializeObject(RecyclerViewAdapter_SearchHospital.prefList));
            editor.Apply();
        }

        //  Search filter logic
        public static void filter(string query)
        {
            string lowerCaseQuery = query.ToLower();

            List<Hospital> filteredList = new List<Hospital>();

            foreach (Hospital hosp in tempHospitalList)
            {
                string text = hosp.HospitalName.ToLower();
                if (text.Contains(lowerCaseQuery))
                {
                    filteredList.Add(hosp);
                }
            }

            adapter.Replace(filteredList);
        }
    }
}