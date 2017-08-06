using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Dental_IT.Droid.Adapters;
using Android.Support.V7.App;
using Android.Content;
using Android.Views;
using Dental_IT.Model;
using System.Threading.Tasks;
using System;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Offered_By : AppCompatActivity
    {
        private List<Hospital> hospitalList = new List<Hospital>();
        private int treatmentId;

        API api = new API();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to select hospital layout
            SetContentView(Resource.Layout.Offered_By);

            //  Create widgets
            RecyclerView offeredBy_RecyclerView = FindViewById<RecyclerView>(Resource.Id.offeredBy_RecyclerView);

            //  Receive data from search treatments
            treatmentId = Intent.GetIntExtra("offeredBy_TreatmentId", 0);

            //  Main data retrieving + processing method
            Task.Run(async () =>
            {
                try
                {
                    //  Get treatments
                    hospitalList = await api.GetClinicHospitalsByTreatment(treatmentId);

                    RunOnUiThread(() =>
                    {
                        //  Configure custom adapter for recyclerview
                        offeredBy_RecyclerView.SetLayoutManager(new LinearLayoutManager(this));

                        RecyclerViewAdapter_OfferedBy adapter = new RecyclerViewAdapter_OfferedBy(this, hospitalList);
                        offeredBy_RecyclerView.SetAdapter(adapter);
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
                var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
                toolbar.SetTitle(Resource.String.offeredBy_title);
                SetSupportActionBar(toolbar);

                //Set backarrow as Default
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            });
        }

        //  Redirect to search when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(Search));
            StartActivity(intent);

            return base.OnOptionsItemSelected(item);
        }
    
    }
}