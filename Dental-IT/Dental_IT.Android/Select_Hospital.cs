using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using Dental_IT.Droid.Adapters;

namespace Dental_IT.Droid
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Select_Hospital : Activity
    {
        public static int LIST_HEIGHT;

        private Hospital a = new Hospital(1, "Hospital 1");
        private Hospital b = new Hospital(2, "Hospital 2");
        private Hospital c = new Hospital(3, "Hospital 3", true);
        private Hospital d = new Hospital(4, "Hospital 4");
        private Hospital e = new Hospital(5, "Hospital 5");
        private Hospital f = new Hospital(6, "Hospital 6");
        private Hospital g = new Hospital(7, "Hospital 7", true);
        private Hospital h = new Hospital(8, "Hospital 8");
        private Hospital i = new Hospital(9, "Hospital 9");
        private Hospital j = new Hospital(10, "Hospital 10");
        private Hospital k = new Hospital(11, "Hospital 11");
        private Hospital l = new Hospital(12, "Hospital 12");
        private Hospital m = new Hospital(13, "Hospital 13");

        private List<Hospital> list = new List<Hospital>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to select hospital layout
            SetContentView(Resource.Layout.Select_Hospital);

            //  Create widgets
            RecyclerView selectHospital_RecyclerView = FindViewById<RecyclerView>(Resource.Id.selectHospital_RecyclerView);

            list.Add(a);
            list.Add(b);
            list.Add(c);
            list.Add(d);
            list.Add(e);
            list.Add(f);
            list.Add(e);
            list.Add(g);
            list.Add(h);
            list.Add(i);
            list.Add(j);
            list.Add(k);
            list.Add(l);
            list.Add(m);

            RunOnUiThread(() =>
            {
                //  Configure custom adapter for recyclerview
                selectHospital_RecyclerView.Post(() =>
                {
                    LIST_HEIGHT = selectHospital_RecyclerView.Height;
                    selectHospital_RecyclerView.SetLayoutManager(new LinearLayoutManager(this));

                    RecyclerViewAdapter_SelectHospital adapter = new RecyclerViewAdapter_SelectHospital(this, list);
                    selectHospital_RecyclerView.SetAdapter(adapter);
                });
            });

            //Implement CustomTheme ActionBar
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "Select Hospital/Clinics ";

            //Set backarrow as Default
            ActionBar.SetDisplayHomeAsUpEnabled(true);
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
    }
}