using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using Dental_IT.Droid.Adapters;
using Android.Preferences;

namespace Dental_IT.Droid
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Select_Hospital : Activity
    {
        public static int LIST_HEIGHT;
        //public static List<Favourite> favouriteList;

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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to select hospital layout
            SetContentView(Resource.Layout.Select_Hospital);

            //  Create widgets
            RecyclerView selectHospital_RecyclerView = FindViewById<RecyclerView>(Resource.Id.selectHospital_RecyclerView);

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

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);

            //  Uncomment to clear shared preferences
            //ISharedPreferencesEditor editor = prefs.Edit();
            //editor.Clear();
            //editor.Apply();

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

            RunOnUiThread(() =>
            {
                //  Configure custom adapter for recyclerview
                selectHospital_RecyclerView.Post(() =>
                {
                    LIST_HEIGHT = selectHospital_RecyclerView.Height;
                    selectHospital_RecyclerView.SetLayoutManager(new LinearLayoutManager(this));

                    RecyclerViewAdapter_SelectHospital adapter = new RecyclerViewAdapter_SelectHospital(this, hospitalList, prefList, tempFavouriteList);
                    selectHospital_RecyclerView.SetAdapter(adapter);
                });

                //Implement CustomTheme ActionBar
                var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
                SetActionBar(toolbar);
                ActionBar.Title = "Select Hospital/Clinics ";

                //Set backarrow as Default
                ActionBar.SetDisplayHomeAsUpEnabled(true);
            });
        }

        //Readonly of list of hospitals in search bars
        private readonly List<string> Hospitals = new List<string>
        {
            "Tan Tock Seng Hospital", "Pristine Dentalworks", "DP Dental", "Smile Dental Group"
        };

        //Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }


        //Toast displayed and redirected to SignIn page when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(Main_Menu));
            StartActivity(intent);

            Toast.MakeText(this, "Main Menu" + item.TitleFormatted,
                ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnStop()
        {
            base.OnStop();

            //  Save favourites to shared preferences
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString("favourites", Newtonsoft.Json.JsonConvert.SerializeObject(RecyclerViewAdapter_SelectHospital.prefList));
            editor.Apply();
        }
    }
}