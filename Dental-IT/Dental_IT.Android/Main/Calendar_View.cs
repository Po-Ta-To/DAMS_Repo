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

namespace Dental_IT.Droid.Main
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Calendar_View : AppCompatActivity
    {
        private static Java.Text.DateFormat formatter = Java.Text.DateFormat.DateInstance;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to calendar view layout
            SetContentView(Resource.Layout.Calendar_View);

            //  Create widgets
            MaterialCalendarView calendar = FindViewById<MaterialCalendarView>(Resource.Id.calendar);

            List<CalendarDay> dates = new List<CalendarDay>();

            CalendarDay b = CalendarDay.From(2017, 6, 15);
            CalendarDay c = CalendarDay.From(2017, 6, 9);
            CalendarDay d = CalendarDay.From(2017, 6, 1);

            dates.Add(b);
            dates.Add(c);
            dates.Add(d);

            List<CalendarDay> dates2 = new List<CalendarDay>();

            CalendarDay e = CalendarDay.From(2017, 6, 10);
            CalendarDay f = CalendarDay.From(2017, 6, 20);
            CalendarDay h = CalendarDay.From(2017, 6, 24);

            dates2.Add(e);
            dates2.Add(f);
            dates2.Add(h);

            List<CalendarDay> dates3 = new List<CalendarDay>();

            CalendarDay i = CalendarDay.From(2017, 6, 27);
            CalendarDay j = CalendarDay.From(2017, 6, 5);

            dates3.Add(i);
            dates3.Add(j);

            calendar.AddDecorators(new EventDecoratorView(this, new Color(ContextCompat.GetColor(this, Resource.Color.white)), dates));
            calendar.AddDecorators(new EventDecoratorView(this, new Color(ContextCompat.GetColor(this, Resource.Color.black)), dates2));
            calendar.AddDecorators(new EventDecoratorView(this, new Color(ContextCompat.GetColor(this, Resource.Color.grey)), dates3));

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

        //Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }


        //Redirect to my appointments page when back arrow is tapped
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
            Drawable dateBg = ContextCompat.GetDrawable(context, Resource.Drawable.legend_iconSquare);
            dateBg.Mutate();    //To allow applying individual filters without affecting the rest
            dateBg.SetTint(color);

            view.SetBackgroundDrawable(dateBg);
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