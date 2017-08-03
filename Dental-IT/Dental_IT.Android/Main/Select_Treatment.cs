using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Dental_IT.Droid.Adapters;
using Android.Preferences;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Select_Treatment : AppCompatActivity
    {
        private Treatment a = new Treatment("T1", "Treatment 1", 100, 500);
        private Treatment b = new Treatment("T2", "Treatment 2", 200, 800);
        private Treatment c = new Treatment("T3", "Treatment 3", 1350, 5400);
        private Treatment d = new Treatment("T4", "Treatment 4", 45, 150);
        private Treatment e = new Treatment("T5", "Treatment 5", 800, 1200);
        private Treatment f = new Treatment("T6", "Treatment 6", 150, 300);
        private Treatment g = new Treatment("T7", "Treatment 7", 500, 1000);
        private Treatment h = new Treatment("T8", "Treatment 8", 100, 500);
        private Treatment i = new Treatment("T9", "Treatment 9", 200, 800);
        private Treatment j = new Treatment("T10", "Treatment 10", 1350, 5400);
        private Treatment k = new Treatment("T11", "Treatment 11", 45, 150);
        private Treatment l = new Treatment("T12", "Treatment 12", 800, 1200);
        private Treatment m = new Treatment("T13", "Treatment 13", 150, 300);
        private Treatment n = new Treatment("T14", "Treatment 14", 500, 1000);

        private List<Treatment> treatmentList = new List<Treatment>();
        private List<string> prefList = new List<string>();
        private List<ToggleState> tempSelectedList = new List<ToggleState>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to treatment information layout
            SetContentView(Resource.Layout.Select_Treatment);

            //  Create widgets
            RecyclerView selectTreatment_RecyclerView = FindViewById<RecyclerView>(Resource.Id.selectTreatment_RecyclerView);

            treatmentList.Add(a);
            treatmentList.Add(b);
            treatmentList.Add(c);
            treatmentList.Add(d);
            treatmentList.Add(e);
            treatmentList.Add(f);
            treatmentList.Add(g);
            treatmentList.Add(h);
            treatmentList.Add(i);
            treatmentList.Add(j);
            treatmentList.Add(k);
            treatmentList.Add(l);
            treatmentList.Add(m);
            treatmentList.Add(n);

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);

            //  Uncomment to clear shared preferences
            //ISharedPreferencesEditor editor = prefs.Edit();
            //editor.Clear();
            //editor.Apply();

            //  If shared preferences contains treatments
            if (prefs.Contains("treatments"))
            {
                //  Retrieve list of treatment ids that are selected
                prefList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(prefs.GetString("treatments", "null"));

                //  Create a temporary list of selected treatments with all the treatments
                foreach (Treatment treatment in treatmentList)
                {
                    ToggleState tempSelected = new ToggleState(treatment.id);

                    //  Set favourited to true if hospital id corresponds with id in shared preferences
                    if (prefList.Exists(e => (e.Equals(treatment.id))))
                    {
                        tempSelected.toggled = true;
                    }

                    tempSelectedList.Add(tempSelected);
                }
            }

            //  Else if shared preferences is empty, create a temporary list of favourites with all the hospitals, setting all favourited to false by default
            else
            {
                foreach (Treatment treatment in treatmentList)
                {
                    tempSelectedList.Add(new ToggleState(treatment.id));
                }
            }

            RunOnUiThread(() =>
            {
                //  Configure custom adapter for recyclerview
                selectTreatment_RecyclerView.Post(() =>
                {
                    selectTreatment_RecyclerView.SetLayoutManager(new LinearLayoutManager(this));

                    RecyclerViewAdapter_SelectTreatment adapter = new RecyclerViewAdapter_SelectTreatment(this, treatmentList, prefList, tempSelectedList);
                    selectTreatment_RecyclerView.SetAdapter(adapter);
                });

                //Implement CustomTheme ActionBar
                var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                toolbar.SetTitle(Resource.String.selectTreatment_title);
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


        //Redirect to request appointment page when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(Request_Appointment));
            StartActivity(intent);

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnStop()
        {
            base.OnStop();

            //  Save selected treatments to shared preferences
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString("treatments", Newtonsoft.Json.JsonConvert.SerializeObject(RecyclerViewAdapter_SelectTreatment.prefList));
            editor.Apply();
        }
    }
}