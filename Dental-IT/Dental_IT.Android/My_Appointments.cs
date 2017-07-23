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

namespace Dental_IT.Droid
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class My_Appointments : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set view to my appointments layout
            SetContentView(Resource.Layout.My_Appointments);

            //  Create and launch upcoming appointments fragment
            AppointmentsUpcomingFragment fragment = new AppointmentsUpcomingFragment();
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            transaction.Add(Resource.Id.fragmentContainer, fragment);
            transaction.Commit();
        }
    }
}