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

namespace Dental_IT.Droid
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Search : AppCompatActivity
    {
        Android.Support.V4.App.Fragment[] fragments;
        ICharSequence[] titles;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to search layout
            SetContentView(Resource.Layout.Search);

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
                tablayout.SetupWithViewPager(viewpager);

                //Implement CustomTheme ActionBar
                var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
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

        //Toast displayed and redirected to MainMenu page when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(Main_Menu));
            StartActivity(intent);

            return base.OnOptionsItemSelected(item);
        }
    }
}