using System.Collections.Generic;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Support.V7.Widget;
using Android.Preferences;
using Dental_IT.Droid.Adapters;

namespace Dental_IT.Droid.Fragments
{
    public class SearchHospitalFragment : Android.Support.V4.App.Fragment
    {
        private Hospital a = new Hospital(1, "Hospital 1");
        private Hospital b = new Hospital(2, "Hospital 2");
        private Hospital c = new Hospital(3, "Hospital 3");
        private Hospital d = new Hospital(4, "Hospital 4");
        private Hospital e = new Hospital(5, "Hospital 5");
        private Hospital f = new Hospital(6, "Hospital 6");
        private Hospital g = new Hospital(7, "Hospital 7");
        private Hospital h = new Hospital(8, "Hospital 8");
        private Hospital i = new Hospital(9, "Hospital 9");
        private Hospital j = new Hospital(10, "Hospital 10");
        private Hospital k = new Hospital(11, "Hospital 11");
        private Hospital l = new Hospital(12, "Hospital 12");
        private Hospital m = new Hospital(13, "Hospital 13");

        private List<Hospital> hospitalList = new List<Hospital>();
        private List<int> prefList = new List<int>();
        private List<ToggleState> tempFavouriteList = new List<ToggleState>();
        private RecyclerView searchHospital_RecyclerView;
        private static RecyclerViewAdapter_SearchHospital adapter;
        private static List<Hospital> tempHospitalList = new List<Hospital>();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            hospitalList.Add(a);
            hospitalList.Add(b);
            hospitalList.Add(c);
            hospitalList.Add(d);
            hospitalList.Add(e);
            hospitalList.Add(f);
            hospitalList.Add(g);
            hospitalList.Add(h);
            hospitalList.Add(i);
            hospitalList.Add(j);
            hospitalList.Add(k);
            hospitalList.Add(l);
            hospitalList.Add(m);

            //  Populate temp list for search
            foreach (Hospital hosp in hospitalList)
            {
                tempHospitalList.Add(hosp);
            }

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Activity);

            //  If shared preferences contains favourites
            if (prefs.Contains("favourites"))
            {
                //  Retrieve list of hospital ids that are favourited
                prefList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(prefs.GetString("favourites", "null"));

                //  Create a temporary list of favourites with all the hospitals
                foreach (Hospital hosp in hospitalList)
                {
                    ToggleState tempFav = new ToggleState(hosp.id);

                    //  Set favourited to true if hospital id corresponds with id in shared preferences
                    if (prefList.Exists(e => (e.Equals(hosp.id))))
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
                    tempFavouriteList.Add(new ToggleState(hosp.id));
                }
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (container == null)
            {
                return null;
            }

            View v = inflater.Inflate(Resource.Layout.Search_Hospital, container, false);

            searchHospital_RecyclerView = v.FindViewById<RecyclerView>(Resource.Id.searchHospital_RecyclerView);

            //  Configure custom adapter for recyclerview
            searchHospital_RecyclerView.SetLayoutManager(new LinearLayoutManager(Activity));

            adapter = new RecyclerViewAdapter_SearchHospital(Activity, hospitalList, prefList, tempFavouriteList);
            searchHospital_RecyclerView.SetAdapter(adapter);

            return v;
        }

        public override void OnStop()
        {
            base.OnStop();

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
                string text = hosp.name.ToLower();
                if (text.Contains(lowerCaseQuery))
                {
                    filteredList.Add(hosp);
                }
            }

            adapter.Replace(filteredList);
        }
    }
}