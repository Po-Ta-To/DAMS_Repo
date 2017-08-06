using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Dental_IT.Droid.Adapters;
using Android.Preferences;
using Dental_IT.Model;
using System.Threading.Tasks;
using System;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Select_Treatment : AppCompatActivity
    {
        private List<Treatment> treatmentList = new List<Treatment>();
        private List<int> prefList = new List<int>();
        private List<ToggleState> tempSelectedList = new List<ToggleState>();
        private RecyclerView selectTreatment_RecyclerView;

        API api = new API();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to treatment information layout
            SetContentView(Resource.Layout.Select_Treatment);

            //  Create widgets
            selectTreatment_RecyclerView = FindViewById<RecyclerView>(Resource.Id.selectTreatment_RecyclerView);
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);

            //  Receive data from request appointment
            int hospId = Intent.GetIntExtra("selectTreatment_HospId", 0);

            //  Main data retrieving + processing method
            Task.Run(async () =>
            {
                try
                {
                    //  Get treatments
                    treatmentList = await api.GetTreatmentsByClinicHospital(hospId);

                    //  If shared preferences contains treatments
                    if (prefs.Contains("treatments"))
                    {
                        //  Retrieve list of treatment ids that are selected
                        prefList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(prefs.GetString("treatments", "null"));

                        //  Create a temporary list of selected treatments with all the treatments
                        foreach (Treatment treatment in treatmentList)
                        {
                            ToggleState tempSelected = new ToggleState(treatment.ID);

                            //  Set favourited to true if hospital id corresponds with id in shared preferences
                            if (prefList.Exists(e => (e.Equals(treatment.ID))))
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
                            tempSelectedList.Add(new ToggleState(treatment.ID));
                        }
                    }

                    RunOnUiThread(() =>
                    {
                        //  Configure custom adapter for recyclerview
                        selectTreatment_RecyclerView.SetLayoutManager(new LinearLayoutManager(this));

                        RecyclerViewAdapter_SelectTreatment adapter = new RecyclerViewAdapter_SelectTreatment(this, treatmentList, prefList, tempSelectedList);
                        selectTreatment_RecyclerView.SetAdapter(adapter);
                    });
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.Write("Obj: " + e.Message + e.StackTrace);
                }
            });

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

        //  Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }


        //  Redirect to request appointment page when back arrow is tapped
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