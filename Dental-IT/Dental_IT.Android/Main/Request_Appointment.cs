﻿using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;
using Android.Preferences;
using Dental_IT.Droid.Adapters;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Dental_IT.Model;
using System.Collections.Generic;
using System;
using Android.Views.InputMethods;
using Android.Support.V4.Content;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Request_Appointment : AppCompatActivity
    {
        private Hospital hosp;
        private List<Dentist> dentists = new List<Dentist>() { new Dentist() };
        private List<Session> sessions = new List<Session>() { };
        private int[] treatmentIDArr;
        private int userID;
        private string accessToken;

        private TextView request_HospitalLabel;
        private EditText request_HospitalField;
        private TextView request_DateLabel;
        private EditText request_DateField;
        private TextView request_DentistLabel;
        private Spinner request_DentistSpinner;
        private TextView request_SessionLabel;
        private Spinner request_SessionSpinner;
        private Button request_TreatmentsBtn;
        private TextView request_RemarksLabel;
        private EditText request_RemarksField;
        private Button request_SubmitBtn;

        API api = new API();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to request appointment layout
            SetContentView(Resource.Layout.Request_Appointment);

            //  Create widgets
            request_HospitalLabel = FindViewById<TextView>(Resource.Id.request_HospitalLabel);
            request_HospitalField = FindViewById<EditText>(Resource.Id.request_HospitalField);
            request_DateLabel = FindViewById<TextView>(Resource.Id.request_DateLabel);
            request_DateField = FindViewById<EditText>(Resource.Id.request_DateField);
            request_DentistLabel = FindViewById<TextView>(Resource.Id.request_DentistLabel);
            request_DentistSpinner = FindViewById<Spinner>(Resource.Id.request_DentistSpinner);
            request_SessionLabel = FindViewById<TextView>(Resource.Id.request_SessionLabel);
            request_SessionSpinner = FindViewById<Spinner>(Resource.Id.request_SessionSpinner);
            request_TreatmentsBtn = FindViewById<Button>(Resource.Id.request_TreatmentsBtn);
            request_RemarksLabel = FindViewById<TextView>(Resource.Id.request_RemarksLabel);
            request_RemarksField = FindViewById<EditText>(Resource.Id.request_RemarksField);
            request_SubmitBtn = FindViewById<Button>(Resource.Id.request_SubmitBtn);

            //  Shared preferences
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();

            //  Get intents
            Intent i = Intent;

            //  If redirected from select hospital or hospital details page (New request)
            if (i.GetStringExtra("newRequest_Hospital") != null)
            {
                //  Remove old shared preferences
                RemoveFromPreferences(prefs, editor);

                //  Receive hospital data from intent
                hosp = JsonConvert.DeserializeObject<Hospital>(i.GetStringExtra("newRequest_Hospital"));
            }
            else
            {
                //  Receive data from shared preferences
                hosp = JsonConvert.DeserializeObject<Hospital>(prefs.GetString("hospital", "null"));
                request_DateField.Text = prefs.GetString("request_Date", GetString(Resource.String.select_date));
                request_RemarksField.Text = prefs.GetString("remarks", "");
            }

            //  Retrieve dentist and session data from database
            Task.Run(async () =>
            {
                try
                {
                    //  Get dentists
                    List<Dentist> tempDentistList = await api.GetDentistsByClinicHospital(hosp.ID);

                    foreach (Dentist dentist in tempDentistList)
                    {
                        dentists.Add(dentist);
                    }

                    //  Get sessions
                    List<Session> tempSessionList = await api.GetSessionsByClinicHospital(hosp.ID);

                    foreach (Session session in tempSessionList)
                    {
                        sessions.Add(session);
                    }

                    RunOnUiThread(() =>
                    {
                        //  Configure spinner adapter for dentist and session dropdowns
                        request_DentistSpinner.Adapter = new SpinnerAdapter_Dentist(this, dentists);
                        request_SessionSpinner.Adapter = new SpinnerAdapter_Session(this, sessions);

                        if (i.GetStringExtra("newRequest_Hospital") == null)
                        {
                            //  Receive spinner data from shared preferences
                            request_DentistSpinner.SetSelection(prefs.GetInt("request_Dentist", 0));
                            request_SessionSpinner.SetSelection(prefs.GetInt("request_Session", 0));
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
                request_HospitalField.SetTextSize(Android.Util.ComplexUnitType.Px, request_HospitalLabel.TextSize);
                request_DateField.SetTextSize(Android.Util.ComplexUnitType.Px, request_HospitalLabel.TextSize);
                request_RemarksField.SetTextSize(Android.Util.ComplexUnitType.Px, request_HospitalLabel.TextSize);

                //  Set label typeface to be same as button typeface
                request_HospitalLabel.SetTypeface(request_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                request_DateLabel.SetTypeface(request_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                request_DentistLabel.SetTypeface(request_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                request_SessionLabel.SetTypeface(request_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                request_RemarksLabel.SetTypeface(request_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);

                //  Set hospital name
                request_HospitalField.Text = hosp.HospitalName;

                //Implement CustomTheme ActionBar
                var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                toolbar.SetTitle(Resource.String.request_title);
                SetSupportActionBar(toolbar);

                //Set backarrow as Default
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            });

            //  Intent to redirect to calendar page
            request_DateField.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Calendar_Select));
                intent.PutExtra("selectDate_From", "Request");
                intent.PutExtra("hosp_OpenDays", JsonConvert.SerializeObject(hosp));
                StartActivity(intent);
            };

            //  Handle select treatments button
            request_TreatmentsBtn.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Select_Treatment));
                intent.PutExtra("selectTreatmentFromRequest_HospId", hosp.ID);
                StartActivity(intent);
            };

            //  Handle request button
            request_SubmitBtn.Click += delegate
            {
                //  Get selected treatments
                if (prefs.Contains("request_Treatments"))
                {
                    List<int> tempTreatmentIDList = JsonConvert.DeserializeObject<List<int>>(prefs.GetString("request_Treatments", "null"));

                    treatmentIDArr = new int[tempTreatmentIDList.Count];

                    int count = 0;

                    foreach (int id in tempTreatmentIDList)
                    {
                        treatmentIDArr[count] = id;
                        count++;
                    }
                }

                // Validate fields
                if (Validate(request_DateField, treatmentIDArr))
                {
                    //  Close keyboard
                    InputMethodManager inputManager = (InputMethodManager)GetSystemService(Context.InputMethodService);
                    if (inputManager != null)
                    {
                        inputManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);
                    }

                    // Randomly generate a 3 digit number
                    int rgNumber = (new Random()).Next(100, 1000);

                    //  Retrieve access token
                    if (prefs.Contains("token"))
                    {
                        accessToken = prefs.GetString("token", "");
                    }

                    //  Get UserID
                    if (prefs.Contains("userID"))
                    {
                        userID = prefs.GetInt("userID", 0);
                    }

                    // Check if Remarks field is empty
                    String remarks = "";
                    if (request_RemarksField.Text.Length == 0)
                    {
                        remarks = "No Remarks";
                    }
                    else
                    {
                        remarks = request_RemarksField.Text;
                    }

                    // Create new appointment and store values
                    Appointment appt = new Appointment()
                    {
                        AppointmentID = "A" + rgNumber,
                        UserID = userID,
                        ClinicHospitalID = hosp.ID,
                        PreferredDate = request_DateField.Text,
                        PreferredTime = sessions[request_SessionSpinner.SelectedItemPosition].SlotID,
                        RequestDoctorDentistID = dentists[request_DentistSpinner.SelectedItemPosition].DentistID,
                        Treatments = treatmentIDArr,
                        Remarks = remarks
                    };

                    // Post the appointment
                    switch (api.PostAppointment(JsonConvert.SerializeObject(appt), accessToken))
                    {
                        //  Successful
                        case 1:
                            Toast.MakeText(this, Resource.String.request_OK, ToastLength.Short).Show();

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

        //  Redirect to select hospital page when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(Select_Hospital));
            StartActivity(intent);
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnStop()
        {
            base.OnStop();

            //  Save hospital name to shared preferences
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString("hospital", JsonConvert.SerializeObject(hosp));
            editor.PutInt("request_Dentist", request_DentistSpinner.SelectedItemPosition);
            editor.PutInt("request_Session", request_SessionSpinner.SelectedItemPosition);
            editor.Apply();
        }

        //  Method to remove old shared preferences
        public void RemoveFromPreferences(ISharedPreferences prefs, ISharedPreferencesEditor editor)
        {
            //  Remove hospital from shared preferences
            if (prefs.Contains("hospital"))
            {
                editor.Remove("hospital");
            }

            //  Remove selected treatments from shared preferences
            if (prefs.Contains("request_Treatments"))
            {
                editor.Remove("request_Treatments");
            }

            //  Remove selected date from shared preferences
            if (prefs.Contains("request_Date"))
            {
                editor.Remove("request_Date");
            }

            //  Remove selected dentist from shared preferences
            if (prefs.Contains("request_Dentist"))
            {
                editor.Remove("request_Dentist");
            }

            //  Remove selected session from shared preferences
            if (prefs.Contains("request_Session"))
            {
                editor.Remove("request_Session");
            }

            editor.Apply();
        }

        // Method to validate the fields
        private bool Validate(EditText request_DateField, int[] treatmentIDArr)
        {
            // Validate the preferred date
            if (request_DateField.Text.Length == 0)
            {
                TextView errorText = (TextView)request_DateField;
                request_DateLabel.RequestFocus();
                errorText.Hint = GetString(Resource.String.no_date);
                errorText.SetHintTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.red)));
                errorText.Error = "";

                return false;
            }
            else if (DateTime.ParseExact(request_DateField.Text, "d MMMM yyyy", null) < DateTime.Today){
                TextView errorText = (TextView)request_DateField;
                request_DateLabel.RequestFocus();
                request_DateField.SetTextColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color.red)));
                errorText.Error = "";
                Toast.MakeText(this, Resource.String.invalid_date, ToastLength.Short).Show();

                return false;
            }

            // Check if one or more treatments are selected
            if(treatmentIDArr == null)
            {
                Toast.MakeText(this, Resource.String.no_treatment, ToastLength.Short).Show();
                return false;
            }

            return true;
        }
    }
}