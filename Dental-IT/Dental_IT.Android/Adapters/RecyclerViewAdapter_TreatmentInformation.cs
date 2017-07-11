using System;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections;
using System.Collections.Generic;

namespace Dental_IT.Droid.Adapters
{
    class RecyclerViewAdapter_TreatmentInformation : RecyclerView.Adapter
    {
        private readonly Context context;
        private List<Treatment> list;

        public RecyclerViewAdapter_TreatmentInformation(Context c, List<Treatment> l)
        {
            context = c;
            list = l;
        }

        public override int ItemCount
        {
            get
            {
                return list.Count;
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
            TreatmentInformation_ViewHolder vh = holder as TreatmentInformation_ViewHolder;

            //  Set height of row
            vh.ItemView.LayoutParameters.Height = Treatment_Information.LIST_HEIGHT / 6;

            //  Set alternating background of row
            int type = GetItemViewType(position);

            if (type == 0)
            {
                vh.ItemView.SetBackgroundResource(Resource.Color._8_white);
            }

            vh.treatmentName.Text = list[position].name;
            vh.treatmentPrice.Text = "$" + list[position].minPrice + "  -  $" + list[position].maxPrice;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.sublayout_Treatment_Info_List_Item, parent, false);
            TreatmentInformation_ViewHolder holder = new TreatmentInformation_ViewHolder(view);

            return holder;
        }
    }

    class TreatmentInformation_ViewHolder : RecyclerView.ViewHolder
    {
        public TextView treatmentName { get; set; }
        public TextView treatmentPrice { get; set; }

        public TreatmentInformation_ViewHolder(View view) : base(view)
        {
            treatmentName = view.FindViewById<TextView>(Resource.Id.treatmentInformation_TreatmentNameText);
            treatmentPrice = view.FindViewById<TextView>(Resource.Id.treatmentInformation_TreatmentPriceText);
        }
    }
}