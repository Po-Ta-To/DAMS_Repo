using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using System.Collections;
using Dental_IT.Droid.Adapters;

namespace Dental_IT.Droid
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Treatment_Information : Activity
    {
        public static int LIST_HEIGHT;

        private Treatment a = new Treatment(1, "Teatment 1", 100, 500);
        private Treatment b = new Treatment(2, "Teatment 2", 200, 800);
        private Treatment c = new Treatment(3, "Teatment 3", 1350, 5400);
        private Treatment d = new Treatment(4, "Teatment 4", 45, 150);
        private Treatment e = new Treatment(5, "Teatment 5", 800, 1200);
        private Treatment f = new Treatment(6, "Teatment 6", 150, 300);
        private Treatment g = new Treatment(7, "Teatment 7", 500, 1000);

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
        }
    }
}