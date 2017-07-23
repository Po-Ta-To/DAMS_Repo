using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Dental_IT.Droid.Fragments;
using Android.Support.V7.Widget;
using Java.Lang;
using Android.Support.V4.View;
using Dental_IT.Droid.Adapters;
using Android.Support.V4.App;
using Android.Support.Design.Widget;

namespace Dental_IT.Droid
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Search : FragmentActivity
    {
        Android.Support.V4.App.Fragment[] fragments;
        ICharSequence[] titles;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to search layout
            SetContentView(Resource.Layout.Search);

            //  Set actionbar mode to tabs
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            fragments = new Android.Support.V4.App.Fragment[]
            {
                new SearchHospitalFragment(),
                new SearchTreatmentFragment()
            };

            titles = CharSequence.ArrayFromStringArray(new[]
            {
                GetString(Resource.String.hospital_tab),
                GetString(Resource.String.clinic_tab)
            });

            //  Configure tabs
            ViewPager viewpager = FindViewById<ViewPager>(Resource.Id.viewPager);
            viewpager.Adapter = new TabsAdapter(SupportFragmentManager, fragments, titles);

            TabLayout tablayout = FindViewById<TabLayout>(Resource.Id.tabLayout);
            tablayout.SetupWithViewPager(viewpager);
        }
    }
}