using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using Dental_IT.Droid.Adapters;
using Android.Preferences;
using Android.Support.V7.App;
using System;
using System.Threading.Tasks;
using System.Json;
using System.Net;
using System.IO;
using Dental_IT.Model;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Select_Hospital : AppCompatActivity, Android.Support.V7.Widget.SearchView.IOnQueryTextListener
    {
        private List<Hospital> hospitalList = new List<Hospital>();
        private List<Hospital> tempHospitalList = new List<Hospital>();
        private List<Hospital> tempHospitalListFilter = new List<Hospital>();
        private List<int> prefList = new List<int>();
        RecyclerViewAdapter_SelectHospital adapter;
        private List<ToggleState> tempFavouriteList = new List<ToggleState>();
        private Android.Support.V7.Widget.SearchView searchView;

        API api = new API();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to select hospital layout
            SetContentView(Resource.Layout.Select_Hospital);

            //  Create widgets
            RecyclerView selectHospital_RecyclerView = FindViewById<RecyclerView>(Resource.Id.selectHospital_RecyclerView);

            //  Set searchview listener
            searchView = FindViewById<Android.Support.V7.Widget.SearchView>(Resource.Id.searchView);
            searchView.SetOnQueryTextListener(this);

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);

            //  Main data retrieving + processing method
            Task.Run(async () =>
            {
                try
                {
                    //  Get hospitals
                    hospitalList = await api.GetClinicHospitals();

                    foreach (Hospital hospital in hospitalList)
                    {
                        tempHospitalList.Add(hospital);
                        tempHospitalListFilter.Add(hospital);
                    }

                    //  If shared preferences contains favourites
                    if (prefs.Contains("favourites"))
                    {
                        //  Retrieve list of hospital ids that are favourited
                        prefList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(prefs.GetString("favourites", "null"));

                        //  If favourites shared preferences is empty
                        if (prefList.Count == 0)
                        {
                            hospitalList.Clear();
                        }
                        else
                        {
                            //  Loop through all hospitals
                            foreach (Hospital hosp in hospitalList)
                            {
                                //  Add to favourites list if hospital id corresponds with id in shared preferences
                                if (prefList.Exists(e => (e.Equals(hosp.ID))))
                                {
                                    ToggleState tempFav = new ToggleState(hosp.ID, true);
                                    tempFavouriteList.Add(tempFav);
                                }
                                //  Remove from hospital list if hospital id does not correspond with id in shared preferences
                                else
                                {
                                    tempHospitalList.Remove(tempHospitalList.Find(e => (e.ID.Equals(hosp.ID))));
                                }
                            }

                            hospitalList = tempHospitalList;
                        }
                    }

                    //  Else if shared preferences is empty, don't display any hospitals
                    else
                    {
                        hospitalList.Clear();
                    }

                    RunOnUiThread(() =>
                    {
                        //  Configure custom adapter for recyclerview
                        selectHospital_RecyclerView.SetLayoutManager(new LinearLayoutManager(this));

                        adapter = new RecyclerViewAdapter_SelectHospital(this, hospitalList, prefList, tempFavouriteList);
                        selectHospital_RecyclerView.SetAdapter(adapter);
                    });
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.Write("Obj: " + e.Message + e.StackTrace);
                }
            });

            RunOnUiThread(() =>
            {
                //Implement CustomTheme ActionBar
                var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                toolbar.SetTitle(Resource.String.selectHosp_title);
                SetSupportActionBar(toolbar);

                //Set backarrow as Default
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            });
        }

        //  Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }

        //Toast displayed and redirected to SignIn page when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(Main_Menu));
            StartActivity(intent);
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

        public bool OnQueryTextChange(string query)
        {
            return false;
        }

        public bool OnQueryTextSubmit(string query)
        {
            List<Hospital> filteredList = filter(tempHospitalListFilter, query);
            adapter.Replace(filteredList);

            return true;
        }

        //  Search filter logic
        private List<Hospital> filter(List<Hospital> temp, string query)
        {
            string lowerCaseQuery = query.ToLower();

            List<Hospital> filteredList = new List<Hospital>();

            foreach (Hospital hosp in temp)
            {
                string text = hosp.HospitalName.ToLower();
                if (text.Contains(lowerCaseQuery))
                {
                    filteredList.Add(hosp);
                }
            }

            return filteredList;
        }
    }
}