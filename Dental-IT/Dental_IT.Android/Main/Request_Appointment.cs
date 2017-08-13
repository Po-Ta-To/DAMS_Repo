using Android.App;
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
using System.Net.Http;
using Dental_IT.Model;
using System.Collections.Generic;
using System;
using Android.Views.InputMethods;
namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Request_Appointment : AppCompatActivity
    {
        private DrawerLayout drawerLayout;
        private NavigationView navigationView;
        private Hospital hosp;
        private List<Dentist> dentists = new List<Dentist>() { new Dentist() };
        private List<string> sessions = new List<string>(){ "Select session" };
        int[] dentistIDArr;

        API api = new API();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to request appointment layout
            SetContentView(Resource.Layout.Request_Appointment);

            //  Create widgets
            TextView request_HospitalLabel = FindViewById<TextView>(Resource.Id.request_HospitalLabel);
            EditText request_HospitalField = FindViewById<EditText>(Resource.Id.request_HospitalField);
            TextView request_DateLabel = FindViewById<TextView>(Resource.Id.request_DateLabel);
            EditText request_DateField = FindViewById<EditText>(Resource.Id.request_DateField);
            TextView request_DentistLabel = FindViewById<TextView>(Resource.Id.request_DentistLabel);
            Spinner request_DentistSpinner = FindViewById<Spinner>(Resource.Id.request_DentistSpinner);
            TextView request_SessionLabel = FindViewById<TextView>(Resource.Id.request_SessionLabel);
            Spinner request_SessionSpinner = FindViewById<Spinner>(Resource.Id.request_SessionSpinner);
            Button request_TreatmentsBtn = FindViewById<Button>(Resource.Id.request_TreatmentsBtn);
            TextView request_RemarksLabel = FindViewById<TextView>(Resource.Id.request_RemarksLabel);
            EditText request_RemarksField = FindViewById<EditText>(Resource.Id.request_RemarksField);
            Button request_SubmitBtn = FindViewById<Button>(Resource.Id.request_SubmitBtn);

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

                //  Receive hospital name data from intent
                hosp = Newtonsoft.Json.JsonConvert.DeserializeObject<Hospital>(i.GetStringExtra("newRequest_Hospital"));
            }
            else
            {
                //  Receive data from shared preferences
                hosp = Newtonsoft.Json.JsonConvert.DeserializeObject<Hospital>(prefs.GetString("hospital", "null"));
                request_DateField.Text = prefs.GetString("date", GetString(Resource.String.select_date));
            }

            //  Retrieve dentist and session data from database
            Task.Run(async () =>
            {
                try
                {
                    //  Get dentists
                    List<Dentist> tempDentistList = await api.GetDentistsByClinicHospital(hosp.ID);
                    dentistIDArr = new int[tempDentistList.Count];

                    int count = 0;

                    foreach (Dentist den in tempDentistList)
                    {
                        dentists.Add(den);
                        dentistIDArr[count] = den.DentistID;
                    }

                    //  Get sessions
                    List<string> tempSessionList = await api.GetSessionsByClinicHospital(hosp.ID);

                    foreach (string session in tempSessionList)
                    {
                        sessions.Add(session);
                    }

                    RunOnUiThread(() =>
                    {
                        //  Configure spinner adapter for dentist and session dropdowns
                        request_DentistSpinner.Adapter = new SpinnerAdapter_Dentist(this, dentists);
                        request_SessionSpinner.Adapter = new SpinnerAdapter(this, sessions, false);
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

                //Set navigation drawer
                SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);

                drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
                navigationView.InflateHeaderView(Resource.Layout.sublayout_Drawer_Header);
                navigationView.InflateMenu(Resource.Menu.nav_menu);
                navigationView.SetCheckedItem(Resource.Id.nav_RequestAppt);
                navigationView.Menu.FindItem(Resource.Id.nav_User).SetTitle(UserAccount.Name);

                navigationView.NavigationItemSelected += (sender, e) =>
                {
                    Intent intent;
                    switch (e.MenuItem.ItemId)
                    {
                        case Resource.Id.nav_Home:
                            intent = new Intent(this, typeof(Main_Menu));
                            StartActivity(intent);
                            break;

                        case Resource.Id.nav_RequestAppt:
                            intent = new Intent(this, typeof(Request_Appointment));
                            StartActivity(intent);
                            break;

                        case Resource.Id.nav_MyAppt:
                            intent = new Intent(this, typeof(My_Appointments));
                            StartActivity(intent);
                            break;

                        case Resource.Id.nav_TreatmentInfo:
                            intent = new Intent(this, typeof(Treatment_Information));
                            StartActivity(intent);
                            break;

                        case Resource.Id.nav_Search:
                            intent = new Intent(this, typeof(Search));
                            StartActivity(intent);
                            break;

                        case Resource.Id.nav_Logout:

                            //  Remove user session from shared preferences
                            editor.Remove("remembered");
                            editor.Apply();

                            //  Clear user data
                            UserAccount.AccessToken = null;
                            UserAccount.Name = null;

                            //  Redirect to sign in page
                            intent = new Intent(this, typeof(Sign_In));
                            StartActivity(intent);

                            break;
                    }

                    //  React to click here and swap fragments or navigate
                    drawerLayout.CloseDrawers();
                };
            });

            //  Intent to redirect to calendar page
            request_DateField.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Calendar_Select));
                StartActivity(intent);
            };

            //  Handle select treatments button
            request_TreatmentsBtn.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Select_Treatment));
                intent.PutExtra("selectTreatment_HospId", hosp.ID);
                StartActivity(intent);
            };

            //  Handle request button
            request_SubmitBtn.Click += delegate
            {
                //Toast.MakeText(this, Resource.String.request_OK, ToastLength.Short).Show();

                //  Close keyboard
                InputMethodManager inputManager = (InputMethodManager)GetSystemService(Context.InputMethodService);
                inputManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);

                // Randomly generate a 3 digit number
                int rgNumber = (new Random()).Next(100, 1000);

                // Create new appointment
                Appointment appt = new Appointment()
                {
                    AppointmentID = "A" + rgNumber,
                    UserID = 1,
                    ClinicHospitalID = hosp.ID,
                    PreferredDate = request_DateField.Text,
                    //PreferredTime = request_SessionSpinner.getSelectedItemPosition(), //Session
                    //RequestDoctorDentistID = dentistIDArr[request_DentistSpinner.getSelectedItemPosition()],
                    // Treatments
                    Remarks = request_RemarksField.Text
                };

                // Post the appointment
                // api.PostAppointment(appt + "");

                Intent intent = new Intent(this, typeof(My_Appointments));
                StartActivity(intent);
            };
        }

        //  Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }

        //Toast displayed and redirected to SignIn page when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnStop()
        {
            base.OnStop();

            //  Save hospital name to shared preferences
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutString("hospital", Newtonsoft.Json.JsonConvert.SerializeObject(hosp));
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
            if (prefs.Contains("treatments"))
            {
                editor.Remove("treatments");
            }

            //  Remove selected date from shared preferences
            if (prefs.Contains("date"))
            {
                editor.Remove("date");
            }

            editor.Apply();
        }

        //// Method that Post an appointment using passed url & item value
        //public async Task RequestAppointment(string url, Appointment appt)
        //{
        //    var json = JsonConvert.SerializeObject(appt);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    HttpResponseMessage response = null;
        //    response = await client.PostAsync(new Uri(url), content);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        Debug.WriteLine("Appointment successfully saved.");
        //    }
        //}
    }
}