using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using System.Collections.Generic;
using Dental_IT.Droid.Adapters;
using Android.Preferences;
using Android.Support.V7.App;
using System;
using System.Threading.Tasks;
using Dental_IT.Model;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Select_Hospital : AppCompatActivity, SearchView.IOnQueryTextListener
    {
        private List<Hospital> hospitalList = new List<Hospital>();
        private List<Hospital> tempHospitalList = new List<Hospital>();
        private List<Hospital> tempHospitalListFilter = new List<Hospital>();
        private List<int> prefList = new List<int>();
        RecyclerViewAdapter_SelectHospital adapter;
        private List<ToggleState> tempFavouriteList = new List<ToggleState>();
        private SearchView searchView;

        API api = new API();

        private DrawerLayout drawerLayout;
        private NavigationView navigationView;
        private Intent intent;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to select hospital layout
            SetContentView(Resource.Layout.Select_Hospital);

            //  Create widgets
            RecyclerView selectHospital_RecyclerView = FindViewById<RecyclerView>(Resource.Id.selectHospital_RecyclerView);

            //  Set searchview listener
            searchView = FindViewById<SearchView>(Resource.Id.searchView);
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

                //Set navigation drawer
                SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);

                drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
                navigationView.InflateHeaderView(Resource.Layout.sublayout_Drawer_Header);
                navigationView.InflateMenu(Resource.Menu.nav_menu);
                navigationView.SetCheckedItem(Resource.Id.nav_RequestAppt);

                if (prefs.Contains("name"))
                {
                    navigationView.Menu.FindItem(Resource.Id.nav_User).SetTitle(prefs.GetString("name", "User not found"));
                }
            });

            navigationView.NavigationItemSelected += (sender, e) =>
            {
                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.nav_Home:
                        intent = new Intent(this, typeof(Main_Menu));
                        StartActivity(intent);
                        break;

                    case Resource.Id.nav_RequestAppt:
                        intent = new Intent(this, typeof(Select_Hospital));
                        StartActivity(intent);
                        break;

                    case Resource.Id.nav_MyAppt:
                        intent = new Intent(this, typeof(My_Appointments));
                        StartActivity(intent);
                        break;

                    case Resource.Id.nav_TreatmentInfo:
                        intent = new Intent(this, typeof(Treatment_Information));
                        StartActivity(intent);
                        break;

                    case Resource.Id.nav_Search:
                        intent = new Intent(this, typeof(Search));
                        StartActivity(intent);
                        break;

                    case Resource.Id.nav_ClearData:
                        ClearData();
                        break;

                    case Resource.Id.nav_Logout:
                        Logout();
                        break;
                }

                //  React to click here and swap fragments or navigate
                drawerLayout.CloseDrawers();
            };
        }

        //  Open navigation drawer when icon is clicked
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        //  Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
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

        //  Logout function
        public void Logout()
        {
            //  Logout confirmation dialog
            Android.App.AlertDialog.Builder logoutConfirm = new Android.App.AlertDialog.Builder(this);
            logoutConfirm.SetMessage(Resource.String.logout_text);
            logoutConfirm.SetNegativeButton(Resource.String.confirm_logout, delegate
            {
                //  Remove user session from shared preferences
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.Remove("remembered");
                editor.Apply();

                //  Clear user data
                UserAccount.AccessToken = null;
                UserAccount.Name = null;
                UserAccount.ID = 0;

                //  Redirect to sign in page
                intent = new Intent(this, typeof(Sign_In));
                StartActivity(intent);
            });
            logoutConfirm.SetNeutralButton(Resource.String.cancel, delegate
            {
                logoutConfirm.Dispose();
            });
            logoutConfirm.Show();
        }

        //  Clear data function
        public void ClearData()
        {
            //  Logout confirmation dialog
            Android.App.AlertDialog.Builder clearConfirm = new Android.App.AlertDialog.Builder(this);
            clearConfirm.SetMessage(Resource.String.clearData_text);
            clearConfirm.SetNegativeButton(Resource.String.confirm_clearData, delegate
            {
                //  Remove user data from shared preferences
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.Clear();
                editor.Apply();

                //  Clear remaining user data
                UserAccount.AccessToken = null;
                UserAccount.Name = null;
                UserAccount.ID = 0;

                //  Redirect to sign in page
                intent = new Intent(this, typeof(Sign_In));
                StartActivity(intent);
            });
            clearConfirm.SetNeutralButton(Resource.String.cancel, delegate
            {
                clearConfirm.Dispose();
            });
            clearConfirm.Show();
        }
    }
}