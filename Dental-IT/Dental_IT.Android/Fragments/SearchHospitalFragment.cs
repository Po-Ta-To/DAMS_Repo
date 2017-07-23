using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
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
        private List<Favourite> tempFavouriteList = new List<Favourite>();
        private List<int> prefList = new List<int>();
        private RecyclerView searchHospital_RecyclerView;

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

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Activity);

            //  If shared preferences contains favourites
            if (prefs.Contains("favourites"))
            {
                //  Retrieve list of hospital ids that are favourited
                prefList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(prefs.GetString("favourites", "null"));

                //  Create a temporary list of favourites with all the hospitals
                foreach (Hospital hosp in hospitalList)
                {
                    Favourite tempFav = new Favourite(hosp.id);

                    //  Set favourited to true if hospital id corresponds with id in shared preferences
                    if (prefList.Exists(e => (e == hosp.id)))
                    {
                        tempFav.favourited = true;
                    }

                    tempFavouriteList.Add(tempFav);
                }
            }

            //  Else if shared preferences is empty, create a temporary list of favourites with all the hospitals, setting all favourited to false by default
            else
            {
                foreach (Hospital hosp in hospitalList)
                {
                    tempFavouriteList.Add(new Favourite(hosp.id));
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

            RecyclerViewAdapter_SearchHospital adapter = new RecyclerViewAdapter_SearchHospital(Activity, hospitalList, prefList, tempFavouriteList);
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
    }
}