﻿using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Java.Lang;

namespace Dental_IT.Droid
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Register : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to register layout
            SetContentView(Resource.Layout.Register);

            //  Create widgets
            EditText registerEmailField = FindViewById<EditText>(Resource.Id.registerEmailField);
            EditText registerPasswordField = FindViewById<EditText>(Resource.Id.registerPasswordField);
            EditText repeatPasswordField = FindViewById<EditText>(Resource.Id.repeatPasswordField);
            EditText registerDOBField = FindViewById<EditText>(Resource.Id.registerDOBField);
            Spinner registerGenderDropdown = FindViewById<Spinner>(Resource.Id.registerGenderDropdown);
            EditText registerNRICField = FindViewById<EditText>(Resource.Id.registerNRICField);
            EditText registerMobileField = FindViewById<EditText>(Resource.Id.registerMobileField);
            CheckBox pdpaCkhbox = FindViewById<CheckBox>(Resource.Id.pdpaChkbox);
            TextView pdpaText = FindViewById<TextView>(Resource.Id.pdpaText);
            Button registerBtn = FindViewById<Button>(Resource.Id.registerBtn);

            //  Configure spinner
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.gender_dropdown, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            registerGenderDropdown.Adapter = adapter;

            //  Configure pdpa dialog
            IClickableSpan pdpaClick = new IClickableSpan();
            pdpaClick.Click += delegate
            {
                AlertDialog.Builder pdpa = new AlertDialog.Builder(this);       
                pdpa.SetTitle("Test");
                pdpa.SetMessage("Testing");
                pdpa.SetNeutralButton("OK", delegate
                {
                    pdpa.Dispose();
                });
                pdpa.Show();
            };

            Android.Text.SpannableString pdpaSpan = new Android.Text.SpannableString(pdpaText.Text);
            pdpaSpan.SetSpan(pdpaClick, 15, 35, Android.Text.SpanTypes.ExclusiveExclusive);
            pdpaSpan.SetSpan(new Android.Text.Style.ForegroundColorSpan(Android.Graphics.Color.Red), 15, 35, Android.Text.SpanTypes.ExclusiveExclusive);
            pdpaText.TextFormatted = pdpaSpan;
            pdpaText.MovementMethod = Android.Text.Method.LinkMovementMethod.Instance;

            //  Set button text size to be same as text field text sizes
            registerBtn.SetTextSize(Android.Util.ComplexUnitType.Px, registerEmailField.TextSize);

            //  Intent to redirect to main menu page
            registerBtn.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Main_Menu));
                StartActivity(intent);
            };
        }
    }

    //  Class for implementing clickable span
    class IClickableSpan : Android.Text.Style.ClickableSpan
    {
        public Action<View> Click;

        public override void OnClick(View widget)
        {
            Click?.Invoke(widget);
        }
    }

    //  Class for spinner custom adapter
    class SpinnerAdapter : BaseAdapter, ISpinnerAdapter
    {
        private readonly Context context;

        public SpinnerAdapter(Context c)
        {
            context = c;
        }

        public override int Count
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            throw new NotImplementedException();
        }
    }
}