using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Dental_IT.Droid.Fragments;
using Java.Lang;
using Android.Support.V4.View;
using Dental_IT.Droid.Adapters;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Support.V4.Widget;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Search : AppCompatActivity, SearchView.IOnQueryTextListener, TabLayout.IOnTabSelectedListener
    {
        private Android.Support.V4.App.Fragment[] fragments;
        private ICharSequence[] titles;
        private int position;
        private SearchView searchView;

        DrawerLayout drawerLayout;
        NavigationView navigationView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to search layout
            SetContentView(Resource.Layout.Search);

            //  Set searchview listener
            searchView = FindViewById<SearchView>(Resource.Id.searchView);
            searchView.SetOnQueryTextListener(this);

            fragments = new Android.Support.V4.App.Fragment[]
            {
                new SearchHospitalFragment(),
                new SearchTreatmentFragment()
            };

            titles = CharSequence.ArrayFromStringArray(new[]
            {
                GetString(Resource.String.hospital_tab),
                GetString(Resource.String.treatment_tab)
            });

            RunOnUiThread(() =>
            {
                //  Configure tabs
                ViewPager viewpager = FindViewById<ViewPager>(Resource.Id.viewPager);
                viewpager.Adapter = new TabsAdapter(SupportFragmentManager, fragments, titles);

                TabLayout tablayout = FindViewById<TabLayout>(Resource.Id.tabLayout);
                tablayout.AddOnTabSelectedListener(this);
                tablayout.SetupWithViewPager(viewpager);

                //Implement CustomTheme ActionBar
                var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
                toolbar.SetTitle(Resource.String.search_title);
                SetSupportActionBar(toolbar);

                //Set menu hambuger
                SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);

                drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
                navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
                navigationView.InflateHeaderView(Resource.Layout.sublayout_Drawer_Header);
                navigationView.InflateMenu(Resource.Menu.nav_menu);
            });

            navigationView.NavigationItemSelected += (sender, e) =>
            {
                e.MenuItem.SetChecked(true);

                Intent intent;

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

                }
                //react to click here and swap fragments or navigate
                drawerLayout.CloseDrawers();
            };
        }

        //Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }

        //Redirect to main menu when back arrow is tapped
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

        public void OnTabReselected(TabLayout.Tab tab)
        {
            position = tab.Position;
        }

        public void OnTabSelected(TabLayout.Tab tab)
        {
            position = tab.Position;
        }

        public void OnTabUnselected(TabLayout.Tab tab)
        {
            
        }

        public bool OnQueryTextChange(string query)
        {
            if (position == 0)
            {
                SearchHospitalFragment.filter(query);
                return true;
            }
            else if (position == 1)
            {
                SearchTreatmentFragment.filter(query);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool OnQueryTextSubmit(string query)
        {
            if (position == 0)
            {
                SearchHospitalFragment.filter(query);
                searchView.ClearFocus();
                return true;
            }
            else if (position == 1)
            {
                SearchTreatmentFragment.filter(query);
                searchView.ClearFocus();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}