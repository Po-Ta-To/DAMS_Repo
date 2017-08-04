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
            string treatmentJson = Arguments.GetString("treatment") ?? "Data not available";

            Treatment treatment = Newtonsoft.Json.JsonConvert.DeserializeObject<Treatment>(treatmentJson);

            TextView title = v.FindViewById<TextView>(Resource.Id.dialog_Title);
            title.Text = treatment.name;

            TextView body = v.FindViewById<TextView>(Resource.Id.dialog_Body);
            body.Text = treatment.description;

            TextView priceText = v.FindViewById<TextView>(Resource.Id.dialog_PriceText);
            priceText.Text = "$" + treatment.minPrice + " - $" + treatment.maxPrice;

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