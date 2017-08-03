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

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Treatment_Information : AppCompatActivity, Android.Support.V7.Widget.SearchView.IOnQueryTextListener
    {
        private Treatment a = new Treatment("T1", "Treatment 1", 100, 500);
        private Treatment b = new Treatment("T2", "Treatment 2", 200, 800);
        private Treatment c = new Treatment("T3", "Treatment 3", 1350, 5400);
        private Treatment d = new Treatment("T4", "Treatment 4", 45, 150);
        private Treatment e = new Treatment("T5", "Treatment 5", 800, 1200);
        private Treatment f = new Treatment("T6", "Treatment 6", 150, 300);
        private Treatment g = new Treatment("T7", "Treatment 7", 500, 1000);

        // A list to store the list of treatments 
        private List<Treatment> treatmentList = new List<Treatment>();

        private List<Treatment> tempTreatmentList = new List<Treatment>();

        private Android.Support.V7.Widget.SearchView searchView;
        RecyclerViewAdapter_TreatmentInformation adapter;

        DrawerLayout drawerLayout;
        NavigationView navigationView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to treatment information layout
            SetContentView(Resource.Layout.Treatment_Information);

            //  Create widgets
            RecyclerView treatmentInformation_RecyclerView = FindViewById<RecyclerView>(Resource.Id.treatmentInformation_RecyclerView);

            treatmentList.Add(a);
            treatmentList.Add(b);
            treatmentList.Add(c);
            treatmentList.Add(d);
            treatmentList.Add(e);
            treatmentList.Add(f);
            treatmentList.Add(g);

            foreach (Treatment treatment in treatmentList)
            {
                tempTreatmentList.Add(treatment);
            }

            // Get all the treatments
            Task.Run(async () =>
            {
                try
                {
                    string url = "http://localhost:49813/api/Treatments";

                    // Get json value by passing the URL
                    JsonValue json = await GetTreatments(url);

                    foreach (JsonObject obj in json)
                    {
                        Treatment tr = new Treatment(obj["ID"], obj["TreatmentName"], 100, 500);
                        System.Diagnostics.Debug.Write(obj["ID"]);
                        System.Diagnostics.Debug.Write(obj["TreatmentName"]);
                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.Write(e.Message);
                }
            });

            //  Set searchview listener
            searchView = FindViewById<Android.Support.V7.Widget.SearchView>(Resource.Id.searchView);
            searchView.SetOnQueryTextListener(this);

            RunOnUiThread(() =>
            {
                //  Configure custom adapter for recyclerview
                treatmentInformation_RecyclerView.Post(() =>
                {
                    treatmentInformation_RecyclerView.SetLayoutManager(new LinearLayoutManager(this));

                    adapter = new RecyclerViewAdapter_TreatmentInformation(this, this, treatmentList);
                    treatmentInformation_RecyclerView.SetAdapter(adapter);
                });

                //Implement CustomTheme ActionBar
                var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                toolbar.SetTitle(Resource.String.treatmentInfo_title);
                SetSupportActionBar(toolbar);

                //Set menu hambuger
                SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);

                drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
                navigationView.InflateHeaderView(Resource.Layout.sublayout_Drawer_Header);
                navigationView.InflateMenu(Resource.Menu.nav_menu);

                navigationView.NavigationItemSelected += (sender, e) =>
                {
                    e.MenuItem.SetChecked(true);

                    Intent intent;

                    switch (e.MenuItem.ItemId)
                    {

                        case Resource.Id.nav_home:
                            intent = new Intent(this, typeof(Main_Menu));
                            StartActivity(intent);
                            Toast.MakeText(this, Resource.String.mainmenu, ToastLength.Short).Show();
                            break;

                        case Resource.Id.nav_RequestAppt:
                            intent = new Intent(this, typeof(Request_Appointment));
                            StartActivity(intent);

                            Toast.MakeText(this, Resource.String.request_title, ToastLength.Short).Show();
                            break;

                        case Resource.Id.nav_MyAppt:
                            intent = new Intent(this, typeof(My_Appointments));
                            StartActivity(intent);

                            Toast.MakeText(this, Resource.String.myAppts_title, ToastLength.Short).Show();
                            break;

                        case Resource.Id.nav_TreatmentInfo:
                            intent = new Intent(this, typeof(Treatment_Information));
                            StartActivity(intent);

                            Toast.MakeText(this, Resource.String.treatmentInfo_title, ToastLength.Short).Show();
                            break;

                        case Resource.Id.nav_Search:
                            intent = new Intent(this, typeof(Search));
                            StartActivity(intent);

                            Toast.MakeText(this, Resource.String.search_title, ToastLength.Short).Show();
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
                string text = treatment.name.ToLower();
                if (text.Contains(lowerCaseQuery))
                {
                    filteredList.Add(treatment);
                }
            }

            return filteredList;
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
                using (WebResponse response = await request.GetResponseAsync())
                {
                    // Get a stream representation of the HTTP web response:
                    using (Stream stream = response.GetResponseStream())
                    {
                        // Use this stream to build a JSON document object:
                        JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));

                        // Return the JSON document:
                        return jsonDoc;
                    }
                }
            }
            catch (WebException e)
            {
                System.Diagnostics.Debug.Write(e.Message);
                return new JsonArray();
            }
        } // End of GetTreatments() method
    }
}

