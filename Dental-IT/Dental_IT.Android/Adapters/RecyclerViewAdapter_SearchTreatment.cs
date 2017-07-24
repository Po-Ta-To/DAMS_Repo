using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;

namespace Dental_IT.Droid.Adapters
{
    class RecyclerViewAdapter_SearchTreatment : RecyclerView.Adapter
    {
        private readonly Context context;
        private List<Treatment> treatmentList;

        public RecyclerViewAdapter_SearchTreatment(Context c, List<Treatment> l)
        {
            context = c;
            treatmentList = l;
        }

        public override int ItemCount
        {
            get
            {
                return treatmentList.Count;
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
            SearchTreatment_ViewHolder vh = holder as SearchTreatment_ViewHolder;

            //  Set height of row
            vh.ItemView.LayoutParameters.Height = (Main_Menu.SCREEN_HEIGHT - Main_Menu.ACTIONBAR_HEIGHT) / 6;

            //  Set alternating background of row
            int type = GetItemViewType(position);

            if (type == 0)
            {
                vh.ItemView.SetBackgroundResource(Resource.Color._8_white);
            }

            // Set view data
            vh.treatmentName.Text = treatmentList[position].name;
            vh.treatmentPrice.Text = "$" + treatmentList[position].minPrice + "  -  $" + treatmentList[position].maxPrice;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.sublayout_Treatment_List_Item, parent, false);
            SearchTreatment_ViewHolder holder = new SearchTreatment_ViewHolder(view);

            holder.ItemView.Click += delegate
            {
                //Intent intent = new Intent(context, typeof(Hospital_Details));
                //intent.PutExtra("details_HospitalName", holder.hospitalName.Text);
                //context.StartActivity(intent);
            };

            return holder;
        }

    }

    class SearchTreatment_ViewHolder : RecyclerView.ViewHolder
    {
        public TextView treatmentName { get; set; }
        public TextView treatmentPrice { get; set; }

        public SearchTreatment_ViewHolder(View view) : base(view)
        {
            treatmentName = view.FindViewById<TextView>(Resource.Id.list_TreatmentNameText);
            treatmentPrice = view.FindViewById<TextView>(Resource.Id.list_TreatmentPriceText);
        }
    }
}