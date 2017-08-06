using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Dental_IT.Droid.Adapters;
using Dental_IT.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dental_IT.Droid.Fragments
{
    public class AppointmentsPastFragment : Android.Support.V4.App.Fragment
    {
        private List<Appointment> appointmentList = new List<Appointment>();
        private List<Appointment> finalAppointmentList = new List<Appointment>();
        private RecyclerView appointmentsPast_RecyclerView;
        private RecyclerViewAdapter_Appointments adapter;

        API api = new API();

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (container == null)
            {
                return null;
            }

            View v = inflater.Inflate(Resource.Layout.Appointments_Past, container, false);

            appointmentsPast_RecyclerView = v.FindViewById<RecyclerView>(Resource.Id.appointmentsPast_RecyclerView);

            //  Main data retrieving + processing method
            Task.Run(async () =>
            {
                try
                {
                    //  Get appointments
                    appointmentList = await api.GetAppointments();

                    //  Check if appointment is past
                    foreach (Appointment appointment in appointmentList)
                    {
                        if (appointment.Date < DateTime.Today.Date)
                        {
                            finalAppointmentList.Add(appointment);
                        }
                    }

                    Activity.RunOnUiThread(() =>
                    {
                        //  Configure custom adapter for recyclerview
                        appointmentsPast_RecyclerView.SetLayoutManager(new LinearLayoutManager(Activity));

                        adapter = new RecyclerViewAdapter_Appointments(Activity, finalAppointmentList, true);
                        appointmentsPast_RecyclerView.SetAdapter(adapter);
                    });
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.Write("Obj: " + e.Message + e.StackTrace);
                }
            });

            return v;
        }
    }
}