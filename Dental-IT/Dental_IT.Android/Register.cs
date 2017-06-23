using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

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
            EditText register_EmailField = FindViewById<EditText>(Resource.Id.register_EmailField);
            EditText register_PasswordField = FindViewById<EditText>(Resource.Id.register_PasswordField);
            EditText register_RepeatPasswordField = FindViewById<EditText>(Resource.Id.register_RepeatPasswordField);
            EditText register_DOBField = FindViewById<EditText>(Resource.Id.register_DOBField);
            Spinner register_GenderDropdown = FindViewById<Spinner>(Resource.Id.register_GenderDropdown);
            EditText register_NRICField = FindViewById<EditText>(Resource.Id.register_NRICField);
            EditText register_MobileField = FindViewById<EditText>(Resource.Id.register_MobileField);
            CheckBox register_PdpaChkbox = FindViewById<CheckBox>(Resource.Id.register_PdpaChkbox);
            TextView register_PdpaText = FindViewById<TextView>(Resource.Id.register_PdpaText);
            Button register_RegisterBtn = FindViewById<Button>(Resource.Id.register_RegisterBtn);

            //  Configure spinner
            var adapter = ArrayAdapter.CreateFromResource(this, Resource.Array.gender_dropdown, Android.Resource.Layout.SimpleSpinnerItem);

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            register_GenderDropdown.Adapter = adapter;

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

            Android.Text.SpannableString pdpaSpan = new Android.Text.SpannableString(register_PdpaText.Text);
            pdpaSpan.SetSpan(pdpaClick, 15, 35, Android.Text.SpanTypes.ExclusiveExclusive);
            pdpaSpan.SetSpan(new Android.Text.Style.ForegroundColorSpan(Android.Graphics.Color.Red), 15, 35, Android.Text.SpanTypes.ExclusiveExclusive);
            register_PdpaText.TextFormatted = pdpaSpan;
            register_PdpaText.MovementMethod = Android.Text.Method.LinkMovementMethod.Instance;

            //  Set button text size to be same as text field text sizes
            register_RegisterBtn.SetTextSize(Android.Util.ComplexUnitType.Px, register_EmailField.TextSize);

            //  Intent to redirect to main menu page
            register_RegisterBtn.Click += delegate
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