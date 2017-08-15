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
using Com.Prolificinteractive.Materialcalendarview.Spans;
using Dental_IT.Model;
using Newtonsoft.Json;
using System;
using Java.Text;

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Calendar_Select : AppCompatActivity
    {
        private static SimpleDateFormat formatter = new SimpleDateFormat("d MMMM yyyy");
        private string selectedDate;
        private string prefString;
        private string initialUpdateDate;
        private Hospital hosp;
        private List<string> closedDays = new List<string>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to calendar select layout
            SetContentView(Resource.Layout.Calendar_Select);

            //  Create widgets
            MaterialCalendarView calendar = FindViewById<MaterialCalendarView>(Resource.Id.calendar);
            TextView calendar_DateText = FindViewById<TextView>(Resource.Id.calendar_DateText);
            Button calendar_ConfirmBtn = FindViewById<Button>(Resource.Id.calendar_ConfirmBtn);

            //  Shared preferences
            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            ISharedPreferencesEditor editor = prefs.Edit();

            //  Get intents
            Intent i = Intent;

            //  Check which appointment page it is redirected from
            if (i.GetStringExtra("selectDate_From") != null)
            {
                if (i.GetStringExtra("selectDate_From").Equals("Request"))
                {
                    prefString = "request_Date";
                    hosp = JsonConvert.DeserializeObject<Hospital>(i.GetStringExtra("hosp_OpenDays"));

                    //  Check which days the hospital is closed
                    if (hosp.IsOpenMonFri == false)
                    {
                        closedDays.Add("Monday");
                        closedDays.Add("Tuesday");
                        closedDays.Add("Wednesday");
                        closedDays.Add("Thursday");
                        closedDays.Add("Friday");
                    }
                    if (hosp.IsOpenSat == false)
                    {
                        closedDays.Add("Saturday");
                    }
                    if (hosp.IsOpenSunPh == false)
                    {
                        closedDays.Add("Sunday");
                    }
                }
                else if (i.GetStringExtra("selectDate_From").Equals("Update"))
                {
                    prefString = "update_Date";
                    hosp = new Hospital();
                    initialUpdateDate = i.GetStringExtra("initial_UpdateDate");
                }
            }
            else
            {
                prefString = null;
            }

            RunOnUiThread(() =>
            {
                //  Set initial select date

                //  From request
                if (prefString.Equals("request_Date"))
                {
                    if (prefs.Contains(prefString))
                    {
                        calendar.SetSelectedDate(new Date(prefs.GetString(prefString, "")));
                    }
                    else
                    {
                        calendar.SetSelectedDate(Calendar.GetInstance(Java.Util.TimeZone.GetTimeZone("Asia / Singapore")));
                    }

                    //  Set background decoration on event dates
                    //calendar.AddDecorators(new EventDecoratorSelect(this, new Color(ContextCompat.GetColor(this, Resource.Color._5_red)), dates));
                    calendar.AddDecorators(new EventDecoratorSelect(this, new Color(ContextCompat.GetColor(this, Resource.Color._5_grey)), closedDays));
                }
                //  From update
                else if (prefString.Equals("update_Date"))
                {
                    calendar.SetSelectedDate(new Date(initialUpdateDate));
                }
                
                selectedDate = formatter.Format(calendar.SelectedDate.Date);
                calendar_DateText.Text = selectedDate;

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
                editor.PutString(prefString, selectedDate);
                editor.Apply();

                if (prefString.Equals("request_Date"))
                {
                    Intent intent = new Intent(this, typeof(Request_Appointment));
                    StartActivity(intent);
                }
                else if (prefString.Equals("update_Date"))
                {
                    Intent intent = new Intent(this, typeof(Update_Appointment));
                    StartActivity(intent);
                }
            };
        }

        //  Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }


        //  Redirect to request appointment or update appointment page when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (prefString.Equals("request_Date"))
            {
                Intent intent = new Intent(this, typeof(Request_Appointment));
                StartActivity(intent);
            }
            else if (prefString.Equals("update_Date"))
            {
                Intent intent = new Intent(this, typeof(Update_Appointment));
                StartActivity(intent);
            }

            return base.OnOptionsItemSelected(item);
        }
    }

    class EventDecoratorSelect : Java.Lang.Object, IDayViewDecorator
    {
        private Context context;
        private int color;
        private List<string> days;
        private static SimpleDateFormat dayFormatter = new SimpleDateFormat("EEEE");

        public EventDecoratorSelect(Context context, int color, List<string> days)
        {
            this.context = context;
            this.color = color;
            this.days = days;
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

            view.SetDaysDisabled(true);
        }

        public bool ShouldDecorate(CalendarDay day)
        {
            return days.Contains(dayFormatter.Format(day.Date));
        }
    }
}