using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Dental_IT.Droid.Main;
using Dental_IT.Model;

namespace Dental_IT.Droid.Adapters
{
    class RecyclerViewAdapter_Appointments : RecyclerView.Adapter
    {
        private readonly Context context;
        private List<Appointment> appointmentList;
        private bool greyed;

        public RecyclerViewAdapter_Appointments(Context c, List<Appointment> l, bool g)
        {
            context = c;
            appointmentList = l;
            greyed = g;
        }

        public override int ItemCount
        {
            get
            {
                return appointmentList.Count;
            }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override int GetItemViewType(int position)
        {
            return position % 2 == 1 ? 0 : 1;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            Appointments_ViewHolder vh = holder as Appointments_ViewHolder;

            //  Set height of row
            vh.ItemView.LayoutParameters.Height = (Main_Menu.SCREEN_HEIGHT - Main_Menu.ACTIONBAR_HEIGHT) / 6;

            //  Set alternating background of row
            int type = GetItemViewType(position);

            if (type == 0)
            {
                vh.ItemView.SetBackgroundResource(Resource.Color._8_white);
            }

            // Set view data
            vh.treatmentsName.Text = appointmentList[position].Treatments;
            vh.dentistName.Text = appointmentList[position].Dentist;
            //vh.dateTime.Text = appointmentList[position].date.ToString("dd/MM/yyyy") + ", " + appointmentList[position].time;
            vh.dateTime.Text = appointmentList[position].Date.ToString() + ", " + appointmentList[position].Time;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view;

            //  Set colour overlay
            if (greyed == true)
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.sublayout_Appointments_List_Item_Grey, parent, false);
            }
            else
            {
                view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.sublayout_Appointments_List_Item, parent, false);
            }
            
            Appointments_ViewHolder holder = new Appointments_ViewHolder(view);

            holder.ItemView.Click += delegate
            {
                Intent intent = new Intent(context, typeof(Appointment_Details));
                intent.PutExtra("appointment", Newtonsoft.Json.JsonConvert.SerializeObject(appointmentList[holder.AdapterPosition]));
                context.StartActivity(intent);
            };

            return holder;
        }
    }

    class Appointments_ViewHolder : RecyclerView.ViewHolder
    {
        public TextView treatmentsName { get; set; }
        public TextView dentistName { get; set; }
        public TextView dateTime { get; set; }

        public Appointments_ViewHolder(View view) : base(view)
        {
            treatmentsName = view.FindViewById<TextView>(Resource.Id.list_TreatmentsText);
            dentistName = view.FindViewById<TextView>(Resource.Id.list_DentistText);
            dateTime = view.FindViewById<TextView>(Resource.Id.list_DateTimeText);
        }
    }
}