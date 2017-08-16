using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Android.Views.InputMethods;
using Android.Preferences;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Dental_IT.Model;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Dental_IT.Droid.Adapters;
using System;
using Android.Support.V4.Content;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Update_Appointment : AppCompatActivity
    {
        private List<Dentist> dentists = new List<Dentist>() { new Dentist() };
        private List<Session> sessions = new List<Session>() { new Session() };
        private Appointment appt;
        private int[] treatmentIDArr;
        private string accessToken;

        private TextView update_HospitalLabel;
        private EditText update_HospitalField;
        private TextView update_DateLabel;
        private EditText update_DateField;
        private TextView update_DentistLabel;
        private Spinner update_DentistSpinner;
        private TextView update_SessionLabel;
        private Spinner update_SessionSpinner;
        private Button update_TreatmentsBtn;
        private TextView update_RemarksLabel;
        private EditText update_RemarksField;
        private Button update_SubmitBtn;

        API api = new API();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to update appointment layout
            SetContentView(Resource.Layout.Update_Appointment);

            //  Create widgets
            update_HospitalLabel = FindViewById<TextView>(Resource.Id.update_HospitalLabel);
            update_HospitalField = FindViewById<EditText>(Resource.Id.update_HospitalField);
            update_DateLabel = FindViewById<TextView>(Resource.Id.update_DateLabel);
            update_DateField = FindViewById<EditText>(Resource.Id.update_DateField);
            update_DentistLabel = FindViewById<TextView>(Resource.Id.update_DentistLabel);
            update_DentistSpinner = FindViewById<Spinner>(Resource.Id.update_DentistSpinner);
            update_SessionLabel = FindViewById<TextView>(Resource.Id.update_SessionLabel);
            update_SessionSpinner = FindViewById<Spinner>(Resource.Id.update_SessionSpinner);
            update_TreatmentsBtn = FindViewById<Button>(Resource.Id.update_TreatmentsBtn);
            update_RemarksLabel = FindViewById<TextView>(Resource.Id.update_RemarksLabel);
            update_RemarksField = FindViewById<EditText>(Resource.Id.update_RemarksField);
            update_SubmitBtn = FindViewById<Button>(Resource.Id.update_SubmitBtn);

            //  Shared preferences
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();

            //  Get intents
            Intent i = Intent;

            //  If redirected from appointment details page (New update)
            if (i.GetStringExtra("update_Appointment") != null)
            {
                //  Remove old shared preferences
                RemoveFromPreferences(prefs, editor);

                //  Receive appointment data from intent
                appt = JsonConvert.DeserializeObject<Appointment>(i.GetStringExtra("update_Appointment"));
                update_DateField.Text = appt.Date.ToString("d MMMM yyyy");
                update_RemarksField.Text = appt.Remarks;
            }
            else
            {
                //  Receive data from shared preferences
                appt = JsonConvert.DeserializeObject<Appointment>(prefs.GetString("appointment", "null"));

                if (prefs.Contains("update_Date"))
                {
                    update_DateField.Text = prefs.GetString("update_Date", GetString(Resource.String.select_date));
                }
                else
                {
                    update_DateField.Text = appt.Date.ToString("d MMMM yyyy");
                }

                update_RemarksField.Text = prefs.GetString("remarks", "");
            }

            //  Retrieve dentist and session data from database
            Task.Run(async () =>
            {
                try
                {
                    //  Get dentists
                    List<Dentist> tempDentistList = await api.GetDentistsByClinicHospital(appt.ClinicHospitalID);

                    foreach (Dentist dentist in tempDentistList)
                    {
                        dentists.Add(dentist);
                    }

                    //  Get sessions
                    List<Session> tempSessionList = await api.GetSessionsByClinicHospital(appt.ClinicHospitalID);

                    foreach (Session session in tempSessionList)
                    {
                        sessions.Add(session);
                    }

                    RunOnUiThread(() =>
                    {
                        //  Configure spinner adapter for dentist and session dropdowns
                        update_DentistSpinner.Adapter = new SpinnerAdapter_Dentist(this, dentists);
                        update_SessionSpinner.Adapter = new SpinnerAdapter_Session(this, sessions);

                        if (i.GetStringExtra("update_Appointment") == null)
                        {
                            //  Receive spinner data from shared preferences
                            update_DentistSpinner.SetSelection(prefs.GetInt("update_Dentist", 0));
                            update_SessionSpinner.SetSelection(prefs.GetInt("update_Session", 0));
                        }
                        else
                        {
                            //  Receive spinner data from appointment object
                            int a = dentists.FindIndex(e => e.DentistID == appt.DoctorDentistID);
                            update_DentistSpinner.SetSelection(dentists.FindIndex(e => e.DentistID == appt.DoctorDentistID));
                            int b = sessions.FindIndex(e => e.SlotID == appt.AppointmentTime);
                            update_SessionSpinner.SetSelection(sessions.FindIndex(e => e.SlotID == appt.AppointmentTime));
                        }
                    });
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.Write("Obj: " + e.Message + e.StackTrace);
                }
            });

            RunOnUiThread(() =>
            {
                //  Set textfield text sizes to be same as textview text sizes
                update_HospitalField.SetTextSize(Android.Util.ComplexUnitType.Px, update_HospitalLabel.TextSize);
                update_DateField.SetTextSize(Android.Util.ComplexUnitType.Px, update_HospitalLabel.TextSize);
                update_RemarksField.SetTextSize(Android.Util.ComplexUnitType.Px, update_HospitalLabel.TextSize);

                //  Set label typeface to be same as button typeface
                update_HospitalLabel.SetTypeface(update_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                update_DateLabel.SetTypeface(update_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                update_DentistLabel.SetTypeface(update_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                update_SessionLabel.SetTypeface(update_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                update_RemarksLabel.SetTypeface(update_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);

                //  Set remaining appointment details
                update_HospitalField.Text = appt.ClinicHospital;

                //  Implement CustomTheme ActionBar
                var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                toolbar.SetTitle(Resource.String.update_title);
                SetSupportActionBar(toolbar);

                //Set backarrow as Default
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            });

            //  Intent to redirect to calendar page
            update_DateField.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Calendar_Select));
                intent.PutExtra("selectDate_From", "Update");
                intent.PutExtra("initial_UpdateDate", update_DateField.Text);
                //intent.PutExtra("hosp_OpenDays", JsonConvert.SerializeObject(hosp));
                StartActivity(intent);
            };

            //  Handle select treatments button
            update_TreatmentsBtn.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Select_Treatment));
                intent.PutExtra("selectTreatmentFromUpdate_Appointment", JsonConvert.SerializeObject(appt));
                StartActivity(intent);
            };

            //  Handle update button
            update_SubmitBtn.Click += delegate
            { 
                // Validate fields
                if (Validate(update_DateField, treatmentIDArr))
                {
                    // Close keyboard
                    InputMethodManager inputManager = (InputMethodManager)GetSystemService(Context.InputMethodService);
                    inputManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);

                //  Retrieve access token
                if (prefs.Contains("token"))
                {
                    accessToken = prefs.GetString("token", "");
                }

                    //  Get selected treatments
                    if (prefs.Contains("update_Treatments"))
                    {
                        List<int> tempTreatmentIDList = JsonConvert.DeserializeObject<List<int>>(prefs.GetString("update_Treatments", "null"));

                        treatmentIDArr = new int[tempTreatmentIDList.Count];

                        int count = 0;

                        foreach (int id in tempTreatmentIDList)
                        {
                            treatmentIDArr[count] = id;
                            count++;
                        }
                    }

                    //If treatments unchanged, use original treatments
                    if (treatmentIDArr == null)
                    {
                        treatmentIDArr = appt.Treatments;
                    }

                    // Check if Remarks field is empty
                    String remarks = "";
                    if (update_RemarksField.Text.Length == 0)
                    {
                        remarks = "No Remarks";
                    }
                    else
                    {
                        remarks = update_RemarksField.Text;
                    }

                    // Create new appointment to store updated values
                    Appointment apptToBeUpdated = new Appointment()
                    {
                        ID = appt.ID,
                        PreferredDate = update_DateField.Text,
                        PreferredTime = sessions[update_SessionSpinner.SelectedItemPosition].SlotID,
                        RequestDoctorDentistID = dentists[update_DentistSpinner.SelectedItemPosition].DentistID,
                        Treatments = treatmentIDArr,
                        Remarks = remarks
                    };

                    // Update the appointment
                    switch (api.PutAppointment(JsonConvert.SerializeObject(apptToBeUpdated), accessToken))
                    {
                        //  Successful
                        case 1:
                            Toast.MakeText(this, Resource.String.update_OK, ToastLength.Short).Show();

                            Intent intent = new Intent(this, typeof(My_Appointments));
                            StartActivity(intent);
                            break;

                        //  Invalid request
                        case 2:
                            Toast.MakeText(this, Resource.String.invalid_request, ToastLength.Short).Show();
                            break;

                        //  No internet connectivity
                        case 3:
                            Toast.MakeText(this, Resource.String.network_error, ToastLength.Short).Show();
                            break;

                        //  Backend problem
                        case 4:
                            Toast.MakeText(this, Resource.String.server_error, ToastLength.Short).Show();
                            break;

                        default:
                            Toast.MakeText(this, Resource.String.error, ToastLength.Short).Show();
                            break;
                    }
                }
            };
        }

        //  Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }

        //  Redirect to appointment details page when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(Appointment_Details));
            StartActivity(intent);

            return base.OnOptionsItemSelected(item);
        }

        protected override void OnPause()
        {
            base.OnStop();

            //  Save hospital name to shared preferences
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString("appointment", JsonConvert.SerializeObject(appt));
            editor.PutInt("update_Dentist", update_DentistSpinner.SelectedItemPosition);
            editor.PutInt("update_Session", update_SessionSpinner.SelectedItemPosition);
            editor.PutString("remarks", update_RemarksField.Text);
            editor.Apply();
        }

        //  Method to remove old shared preferences
        public void RemoveFromPreferences(ISharedPreferences prefs, ISharedPreferencesEditor editor)
        {
            //  Remove appointment from shared preferences
            if (prefs.Contains("appointment"))
            {
                editor.Remove("appointment");
            }

            //  Remove selected treatments from shared preferences
            if (prefs.Contains("update_Treatments"))
            {
                editor.Remove("update_Treatments");
            }

            //  Remove selected date from shared preferences
            if (prefs.Contains("update_Date"))
            {
                editor.Remove("update_Date");
            }

            //  Remove selected dentist from shared preferences
            if (prefs.Contains("update_Dentist"))
            {
                editor.Remove("update_Dentist");
            }

            //  Remove selected session from shared preferences
            if (prefs.Contains("update_Session"))
            {
                editor.Remove("update_Session");
            }

            editor.Apply();
        }

        // Method to validate the fields
        private bool Validate(EditText update_DateField, int[] treatmentIDArr)
        {
            // Check if preferred date is valid
            if (DateTime.ParseExact(update_DateField.Text, "d MMMM yyyy", null) < DateTime.Today)
            {
                TextView errorText = (TextView)update_DateField;
                update_DateLabel.RequestFocus();
                update_DateField.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.red)));
                errorText.Error = "";
                Toast.MakeText(this, Resource.String.invalid_date, ToastLength.Short).Show();

                return false;
            }

            // Check if one or more treatments are selected
            if (treatmentIDArr == null)
            {
                Toast.MakeText(this, Resource.String.no_treatment, ToastLength.Short).Show();
                return false;
            }

            return true;
        }
    }
}