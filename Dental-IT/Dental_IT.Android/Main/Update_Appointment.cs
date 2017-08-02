using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Dental_IT.Droid.Adapters;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Update_Appointment : AppCompatActivity
    {
        DrawerLayout drawerLayout;
        NavigationView navigationView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to update appointment layout
            SetContentView(Resource.Layout.Update_Appointment);

            //  Receive data from select_hospital
            string hospitalName = Intent.GetStringExtra("update_HospitalName") ?? "Data not available";

            //  Create widgets
            TextView update_HospitalLabel = FindViewById<TextView>(Resource.Id.update_HospitalLabel);
            EditText update_HospitalField = FindViewById<EditText>(Resource.Id.update_HospitalField);
            TextView update_DateLabel = FindViewById<TextView>(Resource.Id.update_DateLabel);
            EditText update_DateField = FindViewById<EditText>(Resource.Id.update_DateField);
            TextView update_DentistLabel = FindViewById<TextView>(Resource.Id.update_DentistLabel);
            Spinner update_DentistSpinner = FindViewById<Spinner>(Resource.Id.update_DentistSpinner);
            TextView update_SessionLabel = FindViewById<TextView>(Resource.Id.update_SessionLabel);
            Spinner update_SessionSpinner = FindViewById<Spinner>(Resource.Id.update_SessionSpinner);
            Button update_TreatmentsBtn = FindViewById<Button>(Resource.Id.update_TreatmentsBtn);
            TextView update_RemarksLabel = FindViewById<TextView>(Resource.Id.update_RemarksLabel);
            EditText update_RemarksField = FindViewById<EditText>(Resource.Id.update_RemarksField);
            Button update_SubmitBtn = FindViewById<Button>(Resource.Id.update_SubmitBtn);

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

                //  Set hospital name
                update_HospitalField.Text = hospitalName;

                //  Configure spinner adapter for dentist and session dropdowns
                update_DentistSpinner.Adapter = new SpinnerAdapter(this, dentists, false);
                update_SessionSpinner.Adapter = new SpinnerAdapter(this, sessions, false);

                //  Implement CustomTheme ActionBar
                var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                toolbar.SetTitle(Resource.String.update_title);
                SetSupportActionBar(toolbar);

                //  Set menu hambuger
                SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);

                drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
                navigationView.InflateHeaderView(Resource.Layout.sublayout_Drawer_Header);
                navigationView.InflateMenu(Resource.Menu.nav_menu);

                navigationView.NavigationItemSelected += (sender, e) =>
                {
                    e.MenuItem.SetChecked(true);

                    Intent intent;

                    switch (e.MenuItem.ItemId)
                    {

                        case Resource.Id.nav_home:
                            intent = new Intent(this, typeof(Main_Menu));
                            StartActivity(intent);
                            Toast.MakeText(this, Resource.String.mainmenu, ToastLength.Short).Show();
                            break;

                        case Resource.Id.nav_RequestAppt:
                            intent = new Intent(this, typeof(Request_Appointment));
                            StartActivity(intent);

                            Toast.MakeText(this, Resource.String.request_title, ToastLength.Short).Show();
                            break;

                        case Resource.Id.nav_MyAppt:
                            intent = new Intent(this, typeof(My_Appointments));
                            StartActivity(intent);

                            Toast.MakeText(this, Resource.String.myAppts_title, ToastLength.Short).Show();
                            break;

                        case Resource.Id.nav_TreatmentInfo:
                            intent = new Intent(this, typeof(Treatment_Information));
                            StartActivity(intent);

                            Toast.MakeText(this, Resource.String.treatmentInfo_title, ToastLength.Short).Show();
                            break;

                        case Resource.Id.nav_Search:
                            intent = new Intent(this, typeof(Search));
                            StartActivity(intent);

                            Toast.MakeText(this, Resource.String.search_title, ToastLength.Short).Show();
                            break;

                    }

                    drawerLayout.CloseDrawers();
                };
            });

            //  Intent to redirect to calendar page
            update_DateField.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Calendar_Select));
                StartActivity(intent);
            };

            //  Handle select treatments button
            update_TreatmentsBtn.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Select_Treatment));
                StartActivity(intent);
            };

            //  Handle update button
            update_SubmitBtn.Click += delegate
            {
                Toast.MakeText(this, Resource.String.update_OK, ToastLength.Short).Show();

                Intent intent = new Intent(this, typeof(My_Appointments));
                StartActivity(intent);
            };
        }

        //Implement menus in the action bar; backarrow
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

        //  List of dentists to populate spinner adapter
        private string[] dentists =
        {
            "Select dentist",
            "Dentist A",
            "Dentist B",
            "Dentist C",
            "Dentist D",
            "Dentist E",
            "Dentist F",
            "Dentist G",
            "Dentist H",
            "Dentist I",
            "Dentist J",
            "Dentist K",
            "Dentist L"
        };

        //  List of sessions to populate spinner adapter
        private string[] sessions =
        {
            "Select session",
            "Session 1",
            "Session 2",
            "Session 3",
            "Session 4"
        };
    }
}