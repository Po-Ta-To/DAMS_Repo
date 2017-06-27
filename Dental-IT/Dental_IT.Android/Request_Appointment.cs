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
            TextView request_RemarksText = FindViewById<TextView>(Resource.Id.request_RemarksText);
            EditText request_RemarksField = FindViewById<EditText>(Resource.Id.request_RemarksField);

            //  Set text sizes to be same as text field text sizes
            request_HospitalText.SetTextSize(Android.Util.ComplexUnitType.Px, request_HospitalField.TextSize);
            request_DateText.SetTextSize(Android.Util.ComplexUnitType.Px, request_HospitalField.TextSize);
            request_DentistText.SetTextSize(Android.Util.ComplexUnitType.Px, request_HospitalField.TextSize);
            request_SessionText.SetTextSize(Android.Util.ComplexUnitType.Px, request_HospitalField.TextSize);
            request_RemarksText.SetTextSize(Android.Util.ComplexUnitType.Px, request_HospitalField.TextSize);

            //  Set hospital name
            request_HospitalField.Text = hospitalName;
        }
    }
}