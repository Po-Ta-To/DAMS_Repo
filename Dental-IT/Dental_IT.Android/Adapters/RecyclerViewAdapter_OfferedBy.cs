using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Dental_IT.Droid.Main;
using Dental_IT.Model;

namespace Dental_IT.Droid.Adapters
{
    class RecyclerViewAdapter_OfferedBy : RecyclerView.Adapter
    {
        private readonly Context context;
        private List<Hospital> hospitalList;

        public RecyclerViewAdapter_OfferedBy(Context c, List<Hospital> l)
        {
            context = c;
            hospitalList = l;
        }


        public override int ItemCount
        {
            get
            {
                return hospitalList.Count;
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
            OfferedBy_ViewHolder vh = holder as OfferedBy_ViewHolder;

            //  Set height of row
            vh.ItemView.LayoutParameters.Height = (Main_Menu.SCREEN_HEIGHT - Main_Menu.ACTIONBAR_HEIGHT) / 6;

            //  Set alternating background of row
            int type = GetItemViewType(position);

            if (type == 0)
            {
                vh.ItemView.SetBackgroundResource(Resource.Color._8_white);
            }

            // Set view data
            vh.hospitalName.Text = hospitalList[position].name;
            vh.treatmentPrice.Text = "$1000 - $2000";
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.sublayout_Offered_List_Item, parent, false);
            OfferedBy_ViewHolder holder = new OfferedBy_ViewHolder(view);

            holder.ItemView.Click += delegate
            {
                Intent intent = new Intent(context, typeof(Hospital_Details));
                intent.PutExtra("offered_HospitalName", holder.hospitalName.Text);
                context.StartActivity(intent);
            };

            return holder;
        }

    }

    class OfferedBy_ViewHolder : RecyclerView.ViewHolder
    {
        public TextView hospitalName { get; set; }
        public TextView treatmentPrice { get; set; }

        public OfferedBy_ViewHolder(View view) : base(view)
        {
            hospitalName = view.FindViewById<TextView>(Resource.Id.list_HospitalNameText);
            treatmentPrice = view.FindViewById<TextView>(Resource.Id.list_HospitalPriceText);
        }
    }
}