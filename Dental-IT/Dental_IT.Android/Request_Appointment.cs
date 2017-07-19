using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Dental_IT.Droid.Fragments;
using System;
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;

namespace Dental_IT.Droid
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Request_Appointment : AppCompatActivity
    {
        private string TAG = "DatePickerFragment";

        DrawerLayout drawerLayout;
        NavigationView navigationView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to request appointment layout
            SetContentView(Resource.Layout.Request_Appointment);

            //  Receive data from select_hospital
            string hospitalName = Intent.GetStringExtra("hospitalName") ?? "Data not available";

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
                request_HospitalField.Text = hospitalName;

                //  Set date field onClick to show and implement DatePicker
                request_DateField.Click += delegate
                {
                    SelectDate(request_DateField);
                };
            });


            //Implement CustomTheme ActionBar
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            toolbar.SetTitle(Resource.String.requestappt_title);
            SetSupportActionBar(toolbar);

            //Set menu hambuger
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.InflateHeaderView(Resource.Layout.sublayout_Drawer_Header);
            navigationView.InflateMenu(Resource.Menu.nav_menu);

            navigationView.NavigationItemSelected += (sender, e) =>
            {
                e.MenuItem.SetChecked(true);
                //react to click here and swap fragments or navigate
                drawerLayout.CloseDrawers();
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

        //  Method to call DatePickerFragment
        private void SelectDate(EditText request_DateField)
        {
            DatePickerFragment fragment = DatePickerFragment.NewInstance(delegate (DateTime time)
            {
                request_DateField.Text = time.ToLongDateString();
            });

            fragment.Show(FragmentManager, TAG);
        }
    }
}