using System;

using Android.App;
using Android.OS;
using Android.Widget;

namespace Dental_IT.Droid.Fragments
{
    class DatePickerFragment : DialogFragment, DatePickerDialog.IOnDateSetListener
    {
        // Initialize this value to prevent NullReferenceExceptions.
        Action<DateTime> dateSelectedHandler = delegate { };

        public static DatePickerFragment NewInstance(Action<DateTime> onDateSelected)
        {
            DatePickerFragment fragment = new DatePickerFragment();
            fragment.dateSelectedHandler = onDateSelected;
            return fragment;
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            DateTime now = DateTime.Now;
            DatePickerDialog dialog = new DatePickerDialog(Activity, this, now.Year, now.Month, now.Day);
            return dialog;
        }

        public void OnDateSet(DatePicker view, int year, int month, int dayOfMonth)
        {
            // Note: month is a value between 0 and 11, not 1 and 12!

            DateTime selectedDate = new DateTime(year, month + 1, dayOfMonth);
            dateSelectedHandler(selectedDate);
        }
    }
}