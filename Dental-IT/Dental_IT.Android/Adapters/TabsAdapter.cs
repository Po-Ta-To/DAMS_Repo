﻿using Android.Support.V4.App;
using Java.Lang;

namespace Dental_IT.Droid.Adapters
{
    class TabsAdapter : FragmentPagerAdapter
    {
        private readonly Android.Support.V4.App.Fragment[] fragments;
        private readonly ICharSequence[] titles;

        public TabsAdapter(Android.Support.V4.App.FragmentManager fm, Android.Support.V4.App.Fragment[] fragments, ICharSequence[] titles) : base(fm)
        {
            this.fragments = fragments;
            this.titles = titles;
        }
        public override int Count
        {
            get
            {
                return fragments.Length;
            }
        }

        public override Android.Support.V4.App.Fragment GetItem(int position)
        {
            return fragments[position];
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return titles[position];
        }
    }

    class TabsAdapterViewHolder : Java.Lang.Object
    {
        //Your adapter views to re-use
        //public TextView Title { get; set; }
    }
}