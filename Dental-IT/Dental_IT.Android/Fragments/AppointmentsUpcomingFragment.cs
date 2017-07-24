using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Dental_IT.Droid.Adapters;

namespace Dental_IT.Droid.Fragments
{
    public class AppointmentsUpcomingFragment : Android.Support.V4.App.Fragment
    {
        private Appointment a = new Appointment(1, "Teeth cleaning, Braces", "Dr Tan KK", "6/7/2017", "1.00pm");
        private Appointment b = new Appointment(2, "Fillings", "Dr Ang Boon Lye", "18/7/2017", "12.00am");
        private Appointment c = new Appointment(3, "Regular dental checkup", "Dr Tan KK", "1/8/2017", "3.30pm");
        private Appointment d = new Appointment(4, "Scaling, polishing", "Dr Tan KK", "25/8/2017", "8.45am");
        private Appointment e = new Appointment(5, "Wisdom tooth removal, regular dental checkup", "Dr Hugh Watson", "6/9/2017", "9.00am");
        private Appointment f = new Appointment(6, "Fillings", "Dr Ang Boon Lye", "6/10/2017", "12.00pm");
        private Appointment g = new Appointment(7, "Teeth cleaning", "Dr Ang Boon Lye", "25/10/2017", "9.30am");
        private Appointment h = new Appointment(8, "Regular dental checkup, Teeth cleaning", "Dr Janice Chia", "12/12/2017", "9.00am");

        private List<Appointment> appointmentList = new List<Appointment>();
        private RecyclerView appointmentsUpcoming_RecyclerView;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            appointmentList.Add(a);
            appointmentList.Add(b);
            appointmentList.Add(c);
            appointmentList.Add(d);
            appointmentList.Add(e);
            appointmentList.Add(f);
            appointmentList.Add(g);
            appointmentList.Add(h);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (container == null)
            {
                return null;
            }

            View v = inflater.Inflate(Resource.Layout.Appointments_Upcoming, container, false);

            appointmentsUpcoming_RecyclerView = v.FindViewById<RecyclerView>(Resource.Id.appointmentsUpcoming_RecyclerView);

            //  Configure custom adapter for recyclerview
            appointmentsUpcoming_RecyclerView.SetLayoutManager(new LinearLayoutManager(Activity));

            RecyclerViewAdapter_Appointments adapter = new RecyclerViewAdapter_Appointments(Activity, appointmentList);
            appointmentsUpcoming_RecyclerView.SetAdapter(adapter);

            return v;
        }
    }
}