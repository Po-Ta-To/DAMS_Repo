using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Support.V7.Widget;
using Dental_IT.Droid.Adapters;
using System;
using Dental_IT.Model;
using System.Threading.Tasks;
using Android.Content;
using Android.Preferences;

namespace Dental_IT.Droid.Fragments
{
    public class AppointmentsUpcomingFragment : Android.Support.V4.App.Fragment
    {
        private List<Appointment> appointmentList = new List<Appointment>();
        private List<Appointment> finalAppointmentList = new List<Appointment>();
        private RecyclerView appointmentsUpcoming_RecyclerView;
        private RecyclerViewAdapter_Appointments adapter;
        private string accessToken;

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

            View v = inflater.Inflate(Resource.Layout.Appointments_Upcoming, container, false);

            appointmentsUpcoming_RecyclerView = v.FindViewById<RecyclerView>(Resource.Id.appointmentsUpcoming_RecyclerView);

            //  Main data retrieving + processing method
            Task.Run(async () =>
            {
                try
                {
                    //  Retrieve access token
                    ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(Context);
                    if (prefs.Contains("token"))
                    {
                        accessToken = prefs.GetString("token", "");
                    }

                    //  Get appointments
                    appointmentList = await api.GetAppointments(accessToken);

                    //  Check if appointment is past
                    foreach (Appointment appointment in appointmentList)
                    {
                        if (appointment.Date >= DateTime.Today.Date)
                        {
                            finalAppointmentList.Add(appointment);
                        }
                    }

                    Activity.RunOnUiThread(() =>
                    {
                        //  Configure custom adapter for recyclerview
                        appointmentsUpcoming_RecyclerView.SetLayoutManager(new LinearLayoutManager(Activity));

                        adapter = new RecyclerViewAdapter_Appointments(Activity, finalAppointmentList, false);
                        appointmentsUpcoming_RecyclerView.SetAdapter(adapter);
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