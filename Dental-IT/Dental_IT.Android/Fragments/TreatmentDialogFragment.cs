using System;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Dental_IT.Droid.Fragments
{
    public class TreatmentDialogFragment : DialogFragment
    {
        Button closeBtn;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //  Remove title bar
            Dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);

            //  Set up dialog
            View v = inflater.Inflate(Resource.Layout.sublayout_Treatment_Info_Dialog, container, true);
            string treatmentName = Arguments.GetString("treatmentName") ?? "Data not available";

            TextView title = v.FindViewById<TextView>(Resource.Id.dialog_Title);
            title.Text = treatmentName;

            TextView body = v.FindViewById<TextView>(Resource.Id.dialog_Body);
            body.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum. Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

            TextView priceText = v.FindViewById<TextView>(Resource.Id.dialog_PriceText);
            priceText.Text = "$1500 - $3500";

            closeBtn = v.FindViewById<Button>(Resource.Id.dialog_closeBtn);
            closeBtn.Click += Button_Click;

            return v;
        }

        public override void OnResume()
        {
            // Set size of the dialog
            Dialog.Window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);

            base.OnResume();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            // Unwire event
            if (disposing)
                closeBtn.Click -= Button_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Dismiss();
        }
    }
}