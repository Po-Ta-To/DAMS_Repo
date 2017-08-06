using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Dental_IT.Droid.Adapters;
using Dental_IT.Model;
using System.Threading.Tasks;
using System;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Treatments_Offered : AppCompatActivity
    {
        private List<Treatment> list = new List<Treatment>();

        DrawerLayout drawerLayout;
        NavigationView navigationView;
        private string hospitalName;
        RecyclerViewAdapter_TreatmentInformation adapter;
        private Hospital hosp;

        API api = new API();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to treatments offered layout
            SetContentView(Resource.Layout.Treatments_Offered);

            //  Create widgets
            RecyclerView treatmentsOffered_RecyclerView = FindViewById<RecyclerView>(Resource.Id.treatmentsOffered_RecyclerView);

            //  Get intents
            Intent i = Intent;

            //  Receive data from hospital_details
            if (i.GetStringExtra("offered_Hospital") != null)
            {
                //  Receive data from search hospital or treatments offered
                hosp = Newtonsoft.Json.JsonConvert.DeserializeObject<Hospital>(i.GetStringExtra("offered_Hospital"));
            }
            else
            {
                hosp = new Hospital();
            }

            //  Main data retrieving + processing method
            Task.Run(async () =>
            {
                try
                {
                    //  Get treatments
                    List<Treatment> treatmentList = await api.GetTreatmentsByClinicHospital(hosp.ID);

                    RunOnUiThread(() =>
                    {
                        //  Configure custom adapter for recyclerview
                        treatmentsOffered_RecyclerView.SetLayoutManager(new LinearLayoutManager(this));

                        adapter = new RecyclerViewAdapter_TreatmentInformation(this, this, treatmentList);
                        treatmentsOffered_RecyclerView.SetAdapter(adapter);
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
                toolbar.SetTitle(Resource.String.treatmentsOffered_title);
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

        //  Redirected to hospital details page when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(Hospital_Details));
            intent.PutExtra("details_HospitalName", hospitalName);
            StartActivity(intent);

            return base.OnOptionsItemSelected(item);
        }
    }
}