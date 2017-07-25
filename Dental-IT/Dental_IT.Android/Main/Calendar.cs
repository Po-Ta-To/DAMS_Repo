using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using MaterialCalendarLibrary;
using Com.Prolificinteractive.Materialcalendarview.Spans;
using Android.Graphics;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Calendar : Activity
    {
        private static Java.Text.DateFormat formatter = Java.Text.DateFormat.DateInstance;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to calendar layout
            SetContentView(Resource.Layout.Calendar);

            //  Create widgets
            MaterialCalendarView calendar = FindViewById<MaterialCalendarView>(Resource.Id.calendarView);
            TextView calendar_DateText = FindViewById<TextView>(Resource.Id.calendar_DateText);
            Button calendar_ConfirmBtn = FindViewById<Button>(Resource.Id.calendar_ConfirmBtn);

            calendar.SetSelectedDate(Java.Util.Calendar.GetInstance(Java.Util.Locale.English));
            calendar_DateText.Text = formatter.Format(calendar.SelectedDate.Date);

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

            calendar.AddDecorators(new EventDecorator(Color.Red, dates));
            calendar.AddDecorators(new EventDecorator(Color.Green, dates2));

            calendar.DateChanged += delegate
            {
                calendar_DateText.Text = formatter.Format(calendar.SelectedDate.Date);
            };

            calendar_ConfirmBtn.Click += delegate
            {
                Intent intent = new Intent(this, typeof(Request_Appointment));
                intent.PutExtra("calendar_Date", formatter.Format(calendar.SelectedDate.Date));
                StartActivity(intent);
            };
        }
    }

    class EventDecorator : Java.Lang.Object, IDayViewDecorator
    {
        private int color;
        private List<CalendarDay> dates;

        public EventDecorator(int color, List<CalendarDay> dates)
        {
            this.color = color;
            this.dates = dates;
        }

        public void Decorate(DayViewFacade view)
        {
            view.AddSpan(new DotSpan(10, color));
            view.SetDaysDisabled(true);
        }

        public bool ShouldDecorate(CalendarDay day)
        {
            if (dates[0].ToString() == day.ToString() || dates[1].ToString() == day.ToString() || dates[2].ToString() == day.ToString() || dates[3].ToString() == day.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}