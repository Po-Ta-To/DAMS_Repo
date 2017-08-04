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

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Treatments_Offered : AppCompatActivity
    {
        //private Treatment a = new Treatment(1, "Treatment 1", 100, 500);
        //private Treatment b = new Treatment(2, "Treatment 2", 200, 800);
        //private Treatment c = new Treatment(3, "Treatment 3", 1350, 5400);
        //private Treatment d = new Treatment(4, "Treatment 4", 45, 150);
        //private Treatment e = new Treatment(5, "Treatment 5", 800, 1200);
        //private Treatment f = new Treatment(6, "Treatment 6", 150, 300);
        //private Treatment g = new Treatment(7, "Treatment 7", 500, 1000);

        private List<Treatment> list = new List<Treatment>();

        DrawerLayout drawerLayout;
        NavigationView navigationView;
        private string hospitalName;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to treatments offered layout
            SetContentView(Resource.Layout.Treatments_Offered);

            //  Receive data from hospital_details
            hospitalName = Intent.GetStringExtra("details_HospitalName") ?? "Data not available";

            //  Create widgets
            RecyclerView treatmentsOffered_RecyclerView = FindViewById<RecyclerView>(Resource.Id.treatmentsOffered_RecyclerView);

            //list.Add(a);
            //list.Add(b);
            //list.Add(c);
            //list.Add(d);
            //list.Add(e);
            //list.Add(f);
            //list.Add(g);

            RunOnUiThread(() =>
            {
                //  Configure custom adapter for recyclerview
                treatmentsOffered_RecyclerView.Post(() =>
                {
                    treatmentsOffered_RecyclerView.SetLayoutManager(new LinearLayoutManager(this));

                    RecyclerViewAdapter_TreatmentInformation adapter = new RecyclerViewAdapter_TreatmentInformation(this, this, list);
                    treatmentsOffered_RecyclerView.SetAdapter(adapter);
                });

                //Implement CustomTheme ActionBar
                var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                toolbar.SetTitle(Resource.String.treatmentsOffered_title);
                SetSupportActionBar(toolbar);

                //Set backarrow as Default
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            });
        }

        //Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }

        //Redirected to hospital details page when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(Hospital_Details));
            intent.PutExtra("details_HospitalName", hospitalName);
            StartActivity(intent);

            return base.OnOptionsItemSelected(item);
        }
    }
}