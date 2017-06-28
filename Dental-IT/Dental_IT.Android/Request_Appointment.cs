using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Dental_IT.Droid
{
    [Activity(MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Request_Appointment : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to request appointment layout
            SetContentView(Resource.Layout.Request_Appointment);

            //  Receive data from select_hospital
            string hospitalName = Intent.GetStringExtra("hospitalName") ?? "Data not available";

            //  Create widgets
            TextView request_HospitalText = FindViewById<TextView>(Resource.Id.request_HospitalText);
            EditText request_HospitalField = FindViewById<EditText>(Resource.Id.request_HospitalField);
            TextView request_DateText = FindViewById<TextView>(Resource.Id.request_DateText);
            EditText request_DateField = FindViewById<EditText>(Resource.Id.request_DateField);
            TextView request_DentistText = FindViewById<TextView>(Resource.Id.request_DentistText);
            TextView request_SessionText = FindViewById<TextView>(Resource.Id.request_SessionText);
            Button request_TreatmentsBtn = FindViewById<Button>(Resource.Id.request_TreatmentsBtn);
            TextView request_RemarksText = FindViewById<TextView>(Resource.Id.request_RemarksText);
            EditText request_RemarksField = FindViewById<EditText>(Resource.Id.request_RemarksField);

            RunOnUiThread(() =>
            {
                //  Set text sizes to be same as textview text sizes
                request_HospitalField.SetTextSize(Android.Util.ComplexUnitType.Px, request_HospitalText.TextSize);
                request_DateField.SetTextSize(Android.Util.ComplexUnitType.Px, request_HospitalText.TextSize);
                request_RemarksField.SetTextSize(Android.Util.ComplexUnitType.Px, request_HospitalText.TextSize);

                //  Set type face to be same as button typeface
                request_HospitalText.SetTypeface(request_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                request_DateText.SetTypeface(request_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                request_DentistText.SetTypeface(request_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                request_SessionText.SetTypeface(request_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);
                request_RemarksText.SetTypeface(request_TreatmentsBtn.Typeface, Android.Graphics.TypefaceStyle.Normal);

                //  Set hospital name
                request_HospitalField.Text = hospitalName;
            });
        }
    }
}