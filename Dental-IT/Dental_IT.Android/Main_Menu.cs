using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Dental_IT.Droid.Adapters;
using System;

namespace Dental_IT.Droid
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, Theme = "@style/MyTheme")]
    public class Main_Menu : AppCompatActivity
    {
        public static int SCREEN_HEIGHT;
        public static int ACTIONBAR_HEIGHT;
        public static int GRID_HEIGHT;

        DrawerLayout drawerLayout;
        NavigationView navigationView;
        Android.Support.V4.App.FragmentTransaction fragmentTransaction;

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
                    mainMenu_GridView.Adapter = new GridAdapter_MainMenu(this, buttonTexts);
                });
            });

            //Implement CustomTheme ActionBar(toolbar)
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            //Set menu hambuger
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            SetupDrawerContent(navigationView);
            navigationView.InflateHeaderView(Resource.Layout.sublayout_Drawer_Header);
            navigationView.InflateMenu(Resource.Menu.nav_menu);

        }

            private void SetupDrawerContent(NavigationView navigationView)
             {
            navigationView.NavigationItemSelected += (sender, e) =>
            {
                e.MenuItem.SetChecked(true);

                Android.Support.V4.App.FragmentTransaction transaction = SupportFragmentManager.BeginTransaction();
                switch (e.MenuItem.ItemId)
                {
                    case Resource.Id.nav_home:
                        Android.Support.V4.App.Fragment home = new Android.Support.V4.App.Fragment();
                        transaction.Replace(Resource.Id.frame_container, home).Commit();
                        break;

                    case Resource.Id.nav_RequestAppt:
                        Android.Support.V4.App.Fragment requestAppt = new Android.Support.V4.App.Fragment();
                        transaction.Replace(Resource.Id.frame_container, requestAppt).Commit();
                        break;

                }
                //react to click here and swap fragments or navigate
                drawerLayout.CloseDrawers();
            };
        }
 

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
           switch (item.ItemId)
           {
               case Android.Resource.Id.Home:
                   drawerLayout.OpenDrawer(Android.Support.V4.View.GravityCompat.Start);
                  return true;

                //case Resource.Id.nav_home:
                //    intent = new Intent(this, typeof(Main_Menu));
                //    break;

                //case Resource.Id.nav_RequestAppt:
                //    intent = new Intent(this, typeof(Request_Appointment));
                //    break;

                //case Resource.Id.nav_MyAppt:
                //    intent = new Intent(this, typeof(Appointment_Details));
                //    break;

                //case Resource.Id.nav_TreatmentInfo:
                //    intent = new Intent(this, typeof(Treatment_Information));
                //    break;

                //case Resource.Id.nav_Search:
                //    intent = new Intent(this, typeof(Select_Hospital));
                //    break;

                //default:
                //      intent = new Intent(this, typeof(Main_Menu));
                //    break;
            }
            //StartActivity(intent);
            return base.OnOptionsItemSelected(item);
        }
        //  List of button texts to popular grid adapter
        private readonly string[] buttonTexts =
        {
            "Request Appointment",
            "My Appointments",
            "Treatment Info",
            "Search"
        };

        int[] imageid =
        {
            //Add icons here (e.g Resource.Drawable.image.png)
        };
    }
}