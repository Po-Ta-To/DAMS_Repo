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
using Android.Support.V7.App;
using Android.Support.V4.Widget;
using Android.Support.Design.Widget;

namespace Dental_IT.Droid
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Search : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Search);

            SearchHospitalFragment fragment = new SearchHospitalFragment();
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            transaction.Add(Resource.Id.fragmentContainer, fragment);
            transaction.Commit();
        }

        //Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }


        //Toast displayed and redirected to SignIn page when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(Sign_In));
            StartActivity(intent);

            Toast.MakeText(this, "Sign In",
                ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }

    }
  }
