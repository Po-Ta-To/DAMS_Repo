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

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Search : AppCompatActivity, SearchView.IOnQueryTextListener, TabLayout.IOnTabSelectedListener
    {
        private Android.Support.V4.App.Fragment[] fragments;
        private ICharSequence[] titles;
        private int position;
        private SearchView searchView;

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

                //Set backarrow as Default
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            });
        }

        //Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }

        //Redirect to main menu when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(Main_Menu));
            StartActivity(intent);

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