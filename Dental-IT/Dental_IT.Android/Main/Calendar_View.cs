using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using MaterialCalendarLibrary;
using Android.Graphics;
using Android.Support.V7.App;
using Android.Views;
using Android.Support.V4.Content;
using Android.Graphics.Drawables;
using Com.Prolificinteractive.Materialcalendarview.Spans;
using System.Threading.Tasks;
using Dental_IT.Model;
using Android.Preferences;
using System;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Calendar_View : AppCompatActivity
    {
        private static Java.Text.DateFormat formatter = Java.Text.DateFormat.DateInstance;
        private List<AppointmentDate> dateList;
        private List<CalendarDay> pendingDateList = new List<CalendarDay>();
        private List<CalendarDay> tempPendingDateList = new List<CalendarDay>();
        private List<CalendarDay> confirmedDateList = new List<CalendarDay>();
        private List<CalendarDay> bothDateList = new List<CalendarDay>();
        private string accessToken;

        API api = new API();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to calendar view layout
            SetContentView(Resource.Layout.Calendar_View);

            //  Create widgets
            MaterialCalendarView calendar = FindViewById<MaterialCalendarView>(Resource.Id.calendar);

            //  Main data retrieving + processing method
            Task.Run(async () =>
            {
                try
                {
                    //  Retrieve access token
                    ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                    if (prefs.Contains("token"))
                    {
                        accessToken = prefs.GetString("token", "");
                    }

                    //  Get dates
                    dateList = await api.GetAppointmentDates(accessToken);

                    //  Place dates into respective lists
                    foreach (AppointmentDate date in dateList)
                    {
                        //System.Diagnostics.Debug.WriteLine(date.Date.ToString());
                        //System.Diagnostics.Debug.WriteLine(new Date(date.Date.Year, date.Date.Month, date.Date.Day));
                        //System.Diagnostics.Debug.WriteLine(new CalendarDay(date.Date.Year, date.Date.Month, date.Date.Day));

                        if (date.Status.Equals("Pending"))
                        {
                            pendingDateList.Add(new CalendarDay(date.Date.Year, date.Date.Month, date.Date.Day));
                            tempPendingDateList.Add(new CalendarDay(date.Date.Year, date.Date.Month, date.Date.Day));
                        }

                        if (date.Status.Equals("Confirmed"))
                        {
                            confirmedDateList.Add(new CalendarDay(date.Date.Year, date.Date.Month, date.Date.Day));
                        }
                    }

                    foreach (CalendarDay day in confirmedDateList)
                    {
                        System.Diagnostics.Debug.WriteLine(day.Date.ToString());
                    }

                    //  Check if day has both pending and confirmed
                    foreach (CalendarDay day in pendingDateList)
                    {
                        if (confirmedDateList.Exists(e => (e.Date == day.Date)))
                        {
                            tempPendingDateList.Remove(day);
                            confirmedDateList.Remove(day);
                            bothDateList.Add(day);
                        }
                    }

                    pendingDateList = tempPendingDateList;

                    RunOnUiThread(() =>
                    {
                        //  Decorate calendar
                        calendar.AddDecorators(new EventDecoratorView(this, new Color(ContextCompat.GetColor(this, Resource.Color._5_gold)), pendingDateList));
                        calendar.AddDecorators(new EventDecoratorView(this, new Color(ContextCompat.GetColor(this, Resource.Color._5_green)), confirmedDateList));
                        calendar.AddDecorators(new EventDecoratorView(this, new Color(ContextCompat.GetColor(this, Resource.Color._5_blue_lotus)), bothDateList));
                    });
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.Write("Obj: " + e.Message + e.StackTrace);
                }
            });

            RunOnUiThread(() =>
            {
                //Implement CustomTheme ActionBar
                var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
                toolbar.SetTitle(Resource.String.calendarView_title);
                SetSupportActionBar(toolbar);

                //Set backarrow as Default
                SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            });
        }

        //  Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }


        //  Redirect to my appointments page when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(My_Appointments));
            StartActivity(intent);

            return base.OnOptionsItemSelected(item);
        }
    }

    class EventDecoratorView : Java.Lang.Object, IDayViewDecorator
    {
        private Context context;
        private Color color;
        private List<CalendarDay> dates;

        public EventDecoratorView(Context context, Color color, List<CalendarDay> dates)
        {
            this.context = context;
            this.color = color;
            this.dates = dates;
        }

        public void Decorate(DayViewFacade view)
        {
            //  Support for lower API that do not have the SetTint method
            if (((int)Android.OS.Build.VERSION.SdkInt) < 21)
            {
                view.AddSpan(new DotSpan(10, color));
            }
            else
            {
                Drawable dateBg = ContextCompat.GetDrawable(context, Resource.Drawable.legend_iconSquare);
                dateBg.Mutate();    //To allow applying individual filters without affecting the rest
                dateBg.SetTint(color);
                view.SetBackgroundDrawable(dateBg);
            }
        }

        public bool ShouldDecorate(CalendarDay day)
        {
            bool b = false;

            if (dates != null)
            {
                foreach (CalendarDay cDay in dates)
                {
                    if (cDay.ToString().Equals(day.ToString()))
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