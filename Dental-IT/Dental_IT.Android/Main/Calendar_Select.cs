using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using MaterialCalendarLibrary;
using Android.Graphics;
using Android.Preferences;
using Android.Support.V7.App;
using Android.Views;
using Android.Support.V4.Content;
using Java.Util;
using Android.Graphics.Drawables;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Calendar_Select : AppCompatActivity
    {
        private static Java.Text.DateFormat formatter = Java.Text.DateFormat.DateInstance;
        private string selectedDate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to calendar select layout
            SetContentView(Resource.Layout.Calendar_Select);

            //  Create widgets
            MaterialCalendarView calendar = FindViewById<MaterialCalendarView>(Resource.Id.calendar);
            TextView calendar_DateText = FindViewById<TextView>(Resource.Id.calendar_DateText);
            Button calendar_ConfirmBtn = FindViewById<Button>(Resource.Id.calendar_ConfirmBtn);

            List<CalendarDay> dates = new List<CalendarDay>();

            CalendarDay a = CalendarDay.From(2017, 6, 10);
            CalendarDay b = CalendarDay.From(2017, 6, 15);
            CalendarDay c = CalendarDay.From(2017, 6, 9);
            CalendarDay d = CalendarDay.From(2017, 6, 1);

            dates.Add(a);
            dates.Add(b);
            dates.Add(c);
            dates.Add(d);

            List<CalendarDay> dates2 = new List<CalendarDay>();

            CalendarDay e = CalendarDay.From(2017, 6, 10);
            CalendarDay f = CalendarDay.From(2017, 6, 20);
            CalendarDay g = CalendarDay.From(2017, 6, 9);
            CalendarDay h = CalendarDay.From(2017, 6, 24);

            List<CalendarDay> list = new List<CalendarDay>();

            dates2.Add(e);
            dates2.Add(f);
            dates2.Add(g);
            dates2.Add(h);

            RunOnUiThread(() =>
            {
                //  Set initial select date
                calendar.SetSelectedDate(Calendar.GetInstance(Java.Util.TimeZone.GetTimeZone("Asia / Singapore")));
                selectedDate = formatter.Format(calendar.SelectedDate.Date);

                calendar_DateText.Text = selectedDate;

                //  Set background decoration on event dates
                calendar.AddDecorators(new EventDecoratorSelect(this, new Color(ContextCompat.GetColor(this, Resource.Color._5_red)), dates));
                calendar.AddDecorators(new EventDecoratorSelect(this, new Color(ContextCompat.GetColor(this, Resource.Color._5_grey)), dates2));

                //  Set date on date change
                calendar.DateChanged += delegate
                {
                    selectedDate = formatter.Format(calendar.SelectedDate.Date);
                    calendar_DateText.Text = selectedDate;
                };

                //Implement CustomTheme ActionBar
                var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                toolbar.SetTitle(Resource.String.calendarSelect_title);
                SetSupportActionBar(toolbar);

                //Set backarrow as Default
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            });

            calendar_ConfirmBtn.Click += delegate
            {
                //  Save selected date to shared preferences
                ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutString("date", selectedDate);
                editor.Apply();

                Intent intent = new Intent(this, typeof(Request_Appointment));
                StartActivity(intent);
            };
        }

        //Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }


        //Redirect to request appointment page when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(Request_Appointment));
            StartActivity(intent);

            return base.OnOptionsItemSelected(item);
        }
    }

    class EventDecoratorSelect : Java.Lang.Object, IDayViewDecorator
    {
        private Context context;
        private int color;
        private List<CalendarDay> dates;

        public EventDecoratorSelect(Context context, int color, List<CalendarDay> dates)
        {
            this.context = context;
            this.color = color;
            this.dates = dates;
        }

        public void Decorate(DayViewFacade view)
        {
            Drawable dateBg = ContextCompat.GetDrawable(context, Resource.Drawable.legend_iconSquare);
            dateBg.Mutate();    //To allow applying individual filters without affecting the rest
            dateBg.SetTint(color);
            view.SetBackgroundDrawable(dateBg);

            view.SetDaysDisabled(true);
        }

        public bool ShouldDecorate(CalendarDay day)
        {
            bool b = false;

            if (day != null)
            {
                foreach (CalendarDay cDay in dates)
                {
                    if (cDay.ToString() == day.ToString())
                    {
                        b = true;
                        break;
                    }
                    else
                    {
                        b = false;
                    }
                }

                return b;
            }
            else
            {
                return false;
            }
        }
    }
}