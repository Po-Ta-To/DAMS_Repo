using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Dental_IT.Droid.Adapters;
using Android.Support.V7.App;
using Android.Content;
using Android.Views;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Offered_By : AppCompatActivity
    {
        private Hospital a = new Hospital("H1", "Hospital 1");
        private Hospital b = new Hospital("H2", "Hospital 2");
        private Hospital c = new Hospital("H3", "Hospital 3");
        private Hospital d = new Hospital("H4", "Hospital 4");
        private Hospital e = new Hospital("H5", "Hospital 5");
        private Hospital f = new Hospital("H6", "Hospital 6");
        private Hospital g = new Hospital("H7", "Hospital 7");
        private Hospital h = new Hospital("H8", "Hospital 8");
        private Hospital i = new Hospital("H9", "Hospital 9");
        private Hospital j = new Hospital("H10", "Hospital 10");
        private Hospital k = new Hospital("H11", "Hospital 11");
        private Hospital l = new Hospital("H12", "Hospital 12");
        private Hospital m = new Hospital("H13", "Hospital 13");

        private List<Hospital> hospitalList = new List<Hospital>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to select hospital layout
            SetContentView(Resource.Layout.Offered_By);

            //  Create widgets
            RecyclerView offeredBy_RecyclerView = FindViewById<RecyclerView>(Resource.Id.offeredBy_RecyclerView);

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

            RunOnUiThread(() =>
            {
                //  Configure custom adapter for recyclerview
                offeredBy_RecyclerView.Post(() =>
                {
                    offeredBy_RecyclerView.SetLayoutManager(new LinearLayoutManager(this));

                    RecyclerViewAdapter_OfferedBy adapter = new RecyclerViewAdapter_OfferedBy(this, hospitalList);
                    offeredBy_RecyclerView.SetAdapter(adapter);
                });

                //Implement CustomTheme ActionBar
                var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
                toolbar.SetTitle(Resource.String.offeredBy_title);
                SetSupportActionBar(toolbar);

                //Set backarrow as Default
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            });
        }

        //Redirect to main menu when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(Search));
            StartActivity(intent);

            return base.OnOptionsItemSelected(item);
        }
    
    }
}