using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Dental_IT.Droid.Adapters;
using System;
using System.Collections.Generic;

namespace Dental_IT.Droid.Fragments
{
    public class AppointmentsPastFragment : Android.Support.V4.App.Fragment
    {
        private Appointment a = new Appointment(1, "Teeth cleaning, Braces", "Dr Tan KK", DateTime.ParseExact("06/07/2017", "dd/MM/yyyy", null), "1.00pm");
        private Appointment b = new Appointment(2, "Fillings", "Dr Ang Boon Lye", DateTime.ParseExact("18/07/2017", "dd/MM/yyyy", null), "12.00am");
        private Appointment c = new Appointment(3, "Regular dental checkup", "Dr Tan KK", DateTime.ParseExact("01/08/2017", "dd/MM/yyyy", null), "3.30pm");
        private Appointment d = new Appointment(4, "Scaling, polishing", "Dr Tan KK", DateTime.ParseExact("25/08/2017", "dd/MM/yyyy", null), "8.45am");
        private Appointment e = new Appointment(5, "Wisdom tooth removal, regular dental checkup", "Dr Hugh Watson", DateTime.ParseExact("06/09/2017", "dd/MM/yyyy", null), "9.00am");
        private Appointment f = new Appointment(6, "Fillings", "Dr Ang Boon Lye", DateTime.ParseExact("06/10/2017", "dd/MM/yyyy", null), "12.00pm");
        private Appointment g = new Appointment(7, "Teeth cleaning", "Dr Ang Boon Lye", DateTime.ParseExact("25/10/2017", "dd/MM/yyyy", null), "9.30am");
        private Appointment h = new Appointment(8, "Regular dental checkup, Teeth cleaning", "Dr Janice Chia", DateTime.ParseExact("12/12/2017", "dd/MM/yyyy", null), "9.00am");

        private List<Appointment> appointmentList = new List<Appointment>();
        private List<Appointment> finalAppointmentList = new List<Appointment>();
        private RecyclerView appointmentsPast_RecyclerView;

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

            //  Check if appointment is past
            foreach (Appointment a in appointmentList)
            {
                if (a.date < DateTime.Today.Date)
                {
                    finalAppointmentList.Add(a);
                }
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (container == null)
            {
                return null;
            }

            View v = inflater.Inflate(Resource.Layout.Appointments_Past, container, false);

            appointmentsPast_RecyclerView = v.FindViewById<RecyclerView>(Resource.Id.appointmentsPast_RecyclerView);

            //  Configure custom adapter for recyclerview
            appointmentsPast_RecyclerView.SetLayoutManager(new LinearLayoutManager(Activity));

            RecyclerViewAdapter_Appointments adapter = new RecyclerViewAdapter_Appointments(Activity, finalAppointmentList, true);
            appointmentsPast_RecyclerView.SetAdapter(adapter);

            return v;
        }
    }
}