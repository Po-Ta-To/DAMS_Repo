using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Dental_IT.Droid.Adapters;
using Android.Views;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Widget;
using Android.Content;
using System;
using System.Threading.Tasks;
using System.Json;
using System.Net;
using System.IO;
using Dental_IT.Model;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Treatment_Information : AppCompatActivity, Android.Support.V7.Widget.SearchView.IOnQueryTextListener
    {
        private List<Treatment> tempTreatmentList = new List<Treatment>();

        private RecyclerView treatmentInformation_RecyclerView;
        private Android.Support.V7.Widget.SearchView searchView;
        private RecyclerViewAdapter_TreatmentInformation adapter;
        private Intent intent;

        private DrawerLayout drawerLayout;
        private NavigationView navigationView;

        API api = new API();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to treatment information layout
            SetContentView(Resource.Layout.Treatment_Information);

            //  Create widgets
            treatmentInformation_RecyclerView = FindViewById<RecyclerView>(Resource.Id.treatmentInformation_RecyclerView);

            //  Set searchview listener
            searchView = FindViewById<Android.Support.V7.Widget.SearchView>(Resource.Id.searchView);
            searchView.SetOnQueryTextListener(this);

            //  Main data retrieving + processing method
            Task.Run(async () =>
            {
                try
                {
                    //  Get treatments
                    List<Treatment> treatmentList = await api.GetTreatments();

                    foreach (Treatment treatment in treatmentList)
                    {
                        tempTreatmentList.Add(treatment);
                    }

                    RunOnUiThread(() =>
                    {
                        //  Configure custom adapter for recyclerview
                        treatmentInformation_RecyclerView.SetLayoutManager(new LinearLayoutManager(this));

                        adapter = new RecyclerViewAdapter_TreatmentInformation(this, this, treatmentList);
                        treatmentInformation_RecyclerView.SetAdapter(adapter);
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
                toolbar.SetTitle(Resource.String.treatmentInfo_title);
                SetSupportActionBar(toolbar);

                //Set navigation drawer
                SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);

                drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
                navigationView.InflateHeaderView(Resource.Layout.sublayout_Drawer_Header);
                navigationView.InflateMenu(Resource.Menu.nav_menu);
                navigationView.SetCheckedItem(Resource.Id.nav_TreatmentInfo);
                navigationView.Menu.FindItem(Resource.Id.nav_User).SetTitle(UserAccount.Name);

                navigationView.NavigationItemSelected += (sender, e) =>
                {
                    switch (e.MenuItem.ItemId)
                    {

                        case Resource.Id.nav_Home:
                            intent = new Intent(this, typeof(Main_Menu));
                            StartActivity(intent);
                            break;

                        case Resource.Id.nav_RequestAppt:
                            intent = new Intent(this, typeof(Request_Appointment));
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
                    //react to click here and swap fragments or navigate
                    drawerLayout.CloseDrawers();
                };
            });
        }

        //Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }

        //Toast displayed and redirected to SignIn page when back arrow is tapped
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

        public bool OnQueryTextChange(string query)
        {
            List<Treatment> filteredList = filter(tempTreatmentList, query);
            adapter.Replace(filteredList);

            return true;
        }

        public bool OnQueryTextSubmit(string query)
        {
            List<Treatment> filteredList = filter(tempTreatmentList, query);
            adapter.Replace(filteredList);
            searchView.ClearFocus();

            return true;
        }

        //  Search filter logic
        private List<Treatment> filter(List<Treatment> temp, string query)
        {
            string lowerCaseQuery = query.ToLower();

            List<Treatment> filteredList = new List<Treatment>();

            foreach (Treatment treatment in temp)
            {
                string text = treatment.TreatmentName.ToLower();
                if (text.Contains(lowerCaseQuery))
                {
                    filteredList.Add(treatment);
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
                ISharedPreferences prefs = Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(this);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.Remove("remembered");
                editor.Apply();

                //  Clear user data
                UserAccount.UserName = null;
                UserAccount.AccessToken = null;
                UserAccount.Name = null;

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
                ISharedPreferences prefs = Android.Preferences.PreferenceManager.GetDefaultSharedPreferences(this);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.Clear();
                editor.Apply();
            });
            clearConfirm.SetNeutralButton(Resource.String.cancel, delegate
            {
                clearConfirm.Dispose();
            });
            clearConfirm.Show();
        }
    }
}

