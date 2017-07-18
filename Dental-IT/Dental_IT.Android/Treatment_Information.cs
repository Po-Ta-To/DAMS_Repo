using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using System.Collections;
using Dental_IT.Droid.Adapters;
using Android.Widget;
using Android.Views;
using Android.Content;

namespace Dental_IT.Droid
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Treatment_Information : Activity
    {
        public static int LIST_HEIGHT;

        private Treatment a = new Treatment(1, "Treatment 1", 100, 500);
        private Treatment b = new Treatment(2, "Treatment 2", 200, 800);
        private Treatment c = new Treatment(3, "Treatment 3", 1350, 5400);
        private Treatment d = new Treatment(4, "Treatment 4", 45, 150);
        private Treatment e = new Treatment(5, "Treatment 5", 800, 1200);
        private Treatment f = new Treatment(6, "Treatment 6", 150, 300);
        private Treatment g = new Treatment(7, "Treatment 7", 500, 1000);

        private List<Treatment> list = new List<Treatment>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to treatment information layout
            SetContentView(Resource.Layout.Treatment_Information);

            //  Create widgets
            RecyclerView treatmentInformation_RecyclerView = FindViewById<RecyclerView>(Resource.Id.treatmentInformation_RecyclerView);

            list.Add(a);
            list.Add(b);
            list.Add(c);
            list.Add(d);
            list.Add(e);
            list.Add(f);
            list.Add(g);

            RunOnUiThread(() =>
            {
                //  Configure custom adapter for recyclerview
                treatmentInformation_RecyclerView.Post(() =>
                {
                    LIST_HEIGHT = treatmentInformation_RecyclerView.Height;
                    treatmentInformation_RecyclerView.SetLayoutManager(new LinearLayoutManager(this));

                    RecyclerViewAdapter_TreatmentInformation adapter = new RecyclerViewAdapter_TreatmentInformation(this, list);
                    treatmentInformation_RecyclerView.SetAdapter(adapter);
                });
            });

            //Implement CustomTheme ActionBar
            var toolbar = FindViewById<Android.Widget.Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "Treatment Information";

            //Set backarrow as Default
            ActionBar.SetDisplayHomeAsUpEnabled(true);

        }

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

