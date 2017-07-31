using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Dental_IT.Droid.Fragments;
using Android.OS;
using Dental_IT.Droid.Main;
using System;

namespace Dental_IT.Droid.Adapters
{
    class RecyclerViewAdapter_TreatmentInformation : RecyclerView.Adapter
    {
        private readonly Activity activity;
        private readonly Context context;
        private List<Treatment> treatmentList;
        Bundle bundle = new Bundle();

        public RecyclerViewAdapter_TreatmentInformation(Activity a, Context c, List<Treatment> l)
        {
            activity = a;
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
            TreatmentInformation_ViewHolder vh = holder as TreatmentInformation_ViewHolder;

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
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.sublayout_Treatment_Info_List_Item, parent, false);
            TreatmentInformation_ViewHolder holder = new TreatmentInformation_ViewHolder(view);

            //  Display information dialog fragment on click
            holder.ItemView.Click += delegate
            {
                bundle.PutString("treatmentName", treatmentList[holder.AdapterPosition].name);

                TreatmentDialogFragment fragment = new TreatmentDialogFragment();
                FragmentTransaction transaction = activity.FragmentManager.BeginTransaction();
                fragment.Arguments = bundle;
                fragment.Show(transaction, "dialog_fragment");
            };

            return holder;
        }

        internal void Replace(List<Treatment> filteredList)
        {
            treatmentList = filteredList;
            NotifyDataSetChanged();
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