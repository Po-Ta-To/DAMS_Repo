﻿using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Support.Design.Widget;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Dental_IT.Droid.Adapters;
using Dental_IT.Model;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, Theme = "@style/MyTheme")]
    public class Main_Menu : AppCompatActivity
    {
        public static int SCREEN_HEIGHT;
        public static int ACTIONBAR_HEIGHT;
        public static int GRID_HEIGHT;

        private DrawerLayout drawerLayout;
        private NavigationView navigationView;
        private Intent intent;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to main menu layout
            SetContentView(Resource.Layout.Main_Menu);

            //  Create widgets
            GridView mainMenu_GridView = FindViewById<GridView>(Resource.Id.mainMenu_GridView);

            RunOnUiThread(() =>
            {
                //  Get screen height
                SCREEN_HEIGHT = Resources.DisplayMetrics.HeightPixels;

                //  Get actionbar height
                Android.Content.Res.TypedArray styledAttributes = Theme.ObtainStyledAttributes(new int[] { Android.Resource.Attribute.ActionBarSize });
                ACTIONBAR_HEIGHT = (int)styledAttributes.GetDimension(0, 0);
                styledAttributes.Recycle();

                //  Configure grid adapter for menu buttons
                mainMenu_GridView.Post(() =>
                {
                    GRID_HEIGHT = mainMenu_GridView.Height;
                    mainMenu_GridView.Adapter = new GridAdapter_MainMenu(this, buttonImages);
                });

                //Implement CustomTheme ActionBar(toolbar)
                var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                SetSupportActionBar(toolbar);

                //Set navigation drawer
                SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);

                drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
                navigationView.InflateHeaderView(Resource.Layout.sublayout_Drawer_Header);
                navigationView.InflateMenu(Resource.Menu.nav_menu);
                navigationView.SetCheckedItem(Resource.Id.nav_Home);

                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                if (prefs.Contains("name"))
                {
                    navigationView.Menu.FindItem(Resource.Id.nav_User).SetTitle(prefs.GetString("name", "User not found"));
                }                
            });

            navigationView.NavigationItemSelected += (sender, e) =>
            {
                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.nav_Home:
                        intent = new Intent(this, typeof(Main_Menu));
                        StartActivity(intent);
                        break;

                    case Resource.Id.nav_RequestAppt:
                        intent = new Intent(this, typeof(Select_Hospital));
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

                    case Resource.Id.nav_ClearData:
                        ClearData();
                        break;

                    case Resource.Id.nav_Logout:
                        Logout();
                        break;
                }

                //  React to click here and swap fragments or navigate
                drawerLayout.CloseDrawers();
            };
        }

        //  Open navigation drawer when icon is clicked
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

        //  Override back button
        public override void OnBackPressed()
        {
            Android.App.AlertDialog.Builder exitConfirm = new Android.App.AlertDialog.Builder(this);
            exitConfirm.SetMessage(Resource.String.exit_text);
            exitConfirm.SetNegativeButton(Resource.String.confirm_exit, delegate
            {
                FinishAffinity();
            });
            exitConfirm.SetNeutralButton(Resource.String.cancel, delegate
            {
                exitConfirm.Dispose();
            });
            exitConfirm.Show();

            return;
        }

        //  List of button image resources to use as icons
        private readonly int[] buttonImages =
        {
            Resource.Drawable.ic_request_appt,
            Resource.Drawable.ic_my_appt,
            Resource.Drawable.ic_treatment_info,
            Resource.Drawable.ic_search
        };

        //  Logout function
        public void Logout()
        {
            //  Logout confirmation dialog
            Android.App.AlertDialog.Builder logoutConfirm = new Android.App.AlertDialog.Builder(this);
            logoutConfirm.SetMessage(Resource.String.logout_text);
            logoutConfirm.SetNegativeButton(Resource.String.confirm_logout, delegate
            {
                //  Remove user session from shared preferences
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.Remove("remembered");
                editor.Apply();

                //  Clear user data
                UserAccount.AccessToken = null;
                UserAccount.Name = null;
                UserAccount.ID = 0;

                //  Redirect to sign in page
                intent = new Intent(this, typeof(Sign_In));
                StartActivity(intent);
            });
            logoutConfirm.SetNeutralButton(Resource.String.cancel, delegate
            {
                logoutConfirm.Dispose();
            });
            logoutConfirm.Show();
        }

        //  Clear data function
        public void ClearData()
        {
            //  Logout confirmation dialog
            Android.App.AlertDialog.Builder clearConfirm = new Android.App.AlertDialog.Builder(this);
            clearConfirm.SetMessage(Resource.String.clearData_text);
            clearConfirm.SetNegativeButton(Resource.String.confirm_clearData, delegate
            {
                //  Remove user data from shared preferences
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.Clear();
                editor.Apply();

                //  Clear remaining user data
                UserAccount.AccessToken = null;
                UserAccount.Name = null;
                UserAccount.ID = 0;

                //  Redirect to sign in page
                intent = new Intent(this, typeof(Sign_In));
                StartActivity(intent);
            });
            clearConfirm.SetNeutralButton(Resource.String.cancel, delegate
            {
                clearConfirm.Dispose();
            });
            clearConfirm.Show();
        }
    }
}