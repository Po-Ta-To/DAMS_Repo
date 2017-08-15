using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.V7.App;
using Dental_IT.Model;
using System;
using System.Threading.Tasks;
using System.Json;
using Android.Preferences;
using Newtonsoft.Json;
using Android.Support.V4.Content;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Appointment_Details : AppCompatActivity
    {
        private Appointment appointment;
        private string accessToken;

        API api = new API();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to appointment details layout
            SetContentView(Resource.Layout.Appointment_Details);

            //  Create widgets
            TextView apptDetails_HospitalLabel = FindViewById<TextView>(Resource.Id.apptDetails_HospitalLabel);
            TextView apptDetails_HospitalText = FindViewById<TextView>(Resource.Id.apptDetails_HospitalText);
            TextView apptDetails_DateLabel = FindViewById<TextView>(Resource.Id.apptDetails_DateLabel);
            TextView apptDetails_DateText = FindViewById<TextView>(Resource.Id.apptDetails_DateText);
            TextView apptDetails_DentistLabel = FindViewById<TextView>(Resource.Id.apptDetails_DentistLabel);
            TextView apptDetails_DentistText = FindViewById<TextView>(Resource.Id.apptDetails_DentistText);
            TextView apptDetails_SessionLabel = FindViewById<TextView>(Resource.Id.apptDetails_SessionLabel);
            TextView apptDetails_SessionText = FindViewById<TextView>(Resource.Id.apptDetails_SessionText);
            TextView apptDetails_TreatmentLabel = FindViewById<TextView>(Resource.Id.apptDetails_TreatmentLabel);
            TextView apptDetails_TreatmentText = FindViewById<TextView>(Resource.Id.apptDetails_TreatmentText);
            TextView apptDetails_StatusLabel = FindViewById<TextView>(Resource.Id.apptDetails_StatusLabel);
            TextView apptDetails_StatusText = FindViewById<TextView>(Resource.Id.apptDetails_StatusText);
            Button apptDetails_UpdateBtn = FindViewById<Button>(Resource.Id.apptDetails_UpdateBtn);
            Button apptDetails_CancelBtn = FindViewById<Button>(Resource.Id.apptDetails_CancelBtn);

            //  Shared preferences
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();

            //  Get intents
            Intent i = Intent;

            //  If redirected from my appointments page
            if (i.GetStringExtra("appointment_Details") != null)
            {
                //  Receive data from my appointments intent
                appointment = Newtonsoft.Json.JsonConvert.DeserializeObject<Appointment>(i.GetStringExtra("appointment_Details"));
            }
            else
            {
                //  Receive data from shared preferences
                if (prefs.Contains("appointment")){
                    appointment = JsonConvert.DeserializeObject<Appointment>(prefs.GetString("appointment", "null"));
                }
                else
                {
                    appointment = new Appointment();
                }
            }

            RunOnUiThread(() =>
            {
                //  Set label typeface to be same as button typeface
                apptDetails_HospitalLabel.SetTypeface(apptDetails_UpdateBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                apptDetails_DateLabel.SetTypeface(apptDetails_UpdateBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                apptDetails_DentistLabel.SetTypeface(apptDetails_UpdateBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                apptDetails_SessionLabel.SetTypeface(apptDetails_UpdateBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                apptDetails_TreatmentLabel.SetTypeface(apptDetails_UpdateBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                apptDetails_StatusLabel.SetTypeface(apptDetails_UpdateBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);

                //  Set appointment details
                apptDetails_HospitalText.Text = appointment.ClinicHospital;
                apptDetails_DateText.Text = appointment.Date.ToString("d MMMM yyyy");
                apptDetails_DentistText.Text = appointment.DentistName;
                apptDetails_SessionText.Text = appointment.TimeString;
                apptDetails_TreatmentText.Text = appointment.TreatmentsName;
                apptDetails_StatusText.Text = appointment.Status;

                //  Disable buttons on past appointments
                if (appointment.Date < DateTime.Today.Date)
                {
                    apptDetails_UpdateBtn.Enabled = false;
                    apptDetails_CancelBtn.Enabled = false;

                    apptDetails_UpdateBtn.SetBackgroundColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color._5_grey)));
                    apptDetails_CancelBtn.SetBackgroundColor(new Android.Graphics.Color(ContextCompat.GetColor(this, Resource.Color._5_grey)));
                }

                //Implement CustomTheme ActionBar
                var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                toolbar.SetTitle(Resource.String.apptDetails_title);
                SetSupportActionBar(toolbar);

                //Set backarrow as Default
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            });

            //  Handle update button
            apptDetails_UpdateBtn.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Update_Appointment));
                intent.PutExtra("update_Appointment", Newtonsoft.Json.JsonConvert.SerializeObject(appointment));
                StartActivity(intent);
            };

            //  Handle delete button
            apptDetails_CancelBtn.Click += delegate
            {
                Android.App.AlertDialog.Builder cancelConfirm = new Android.App.AlertDialog.Builder(this);
                cancelConfirm.SetTitle(Resource.String.cancel_title);
                cancelConfirm.SetMessage(Resource.String.cancel_text);
                cancelConfirm.SetNegativeButton(Resource.String.confirm_cancel, delegate
                {
                    //  Retrieve access token
                    if (prefs.Contains("token"))
                    {
                        accessToken = prefs.GetString("token", "");
                    }

                    // Cancel the appointment
                    switch (api.CancelAppointment(appointment.ID, accessToken))
                    {
                        //  Successful
                        case 1:
                            Toast.MakeText(this, Resource.String.cancel_OK, ToastLength.Short).Show();

                            Intent intent = new Intent(this, typeof(My_Appointments));
                            StartActivity(intent);
                            break;

                        //  Invalid request
                        case 2:
                            Toast.MakeText(this, Resource.String.invalid_cancel, ToastLength.Short).Show();
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
                });
                cancelConfirm.SetNeutralButton(Resource.String.no, delegate
                {
                    cancelConfirm.Dispose();
                });
                cancelConfirm.Show();
            };
        }


        //  Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }


        //  Redirect to my appointments page when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(My_Appointments));
            StartActivity(intent);
            return base.OnOptionsItemSelected(item);
        }
    }
}