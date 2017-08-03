using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using Dental_IT.Droid.Main;

namespace Dental_IT.Droid.Adapters
{
    class RecyclerViewAdapter_SelectTreatment : RecyclerView.Adapter
    {
        private readonly Context context;
        private List<Treatment> treatmentList;
        public static List<string> prefList;
        private List<ToggleState> tempSelectedList;

        public RecyclerViewAdapter_SelectTreatment(Context c, List<Treatment> t, List<string> p, List<ToggleState> temp)
        {
            context = c;
            treatmentList = t;
            prefList = p;
            tempSelectedList = temp;
        }

        public override int ItemCount
        {
            get
            {
                return treatmentList.Count;
            }
        }

        private void OnItemClick(int position, SelectTreatment_ViewHolder holder)
        {
            if (tempSelectedList[position].toggled == false)
            {
                tempSelectedList[position].toggled = true;
                holder.treatmentChkbox.Checked = true;

                prefList.Add(tempSelectedList[position].id);
            }
            else
            {
                tempSelectedList[position].toggled = false;
                holder.treatmentChkbox.Checked = false;

                prefList.Remove(prefList.Find(e => (e.Equals(tempSelectedList[position].id))));
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
            SelectTreatment_ViewHolder vh = holder as SelectTreatment_ViewHolder;

            //  Set height of row
            vh.ItemView.LayoutParameters.Height = (Main_Menu.SCREEN_HEIGHT - Main_Menu.ACTIONBAR_HEIGHT) / 6;

            //  Set alternating background of row
            int type = GetItemViewType(position);

            if (type == 0)
            {
                vh.ItemView.SetBackgroundResource(Resource.Color._8_white);
            }

            // Set view data
            vh.treatmentChkbox.Checked = tempSelectedList[position].toggled;            
            vh.treatmentName.Text = treatmentList[position].name;
            vh.treatmentPrice.Text = "$" + treatmentList[position].minPrice + "  -  $" + treatmentList[position].maxPrice;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.sublayout_Treatment_Select_List_Item, parent, false);
            SelectTreatment_ViewHolder holder = new SelectTreatment_ViewHolder(view);

            holder.ItemView.Click -= delegate { OnItemClick(holder.AdapterPosition, holder); };
            holder.ItemView.Click += delegate { OnItemClick(holder.AdapterPosition, holder); };

            return holder;
        }
    }

    class SelectTreatment_ViewHolder : RecyclerView.ViewHolder
    {
        public CheckBox treatmentChkbox { get; set; }
        public TextView treatmentName { get; set; }
        public TextView treatmentPrice { get; set; }

        public SelectTreatment_ViewHolder(View view) : base(view)
        {
            treatmentChkbox = view.FindViewById<CheckBox>(Resource.Id.list_TreatmentChkbox);
            treatmentName = view.FindViewById<TextView>(Resource.Id.list_TreatmentNameText);
            treatmentPrice = view.FindViewById<TextView>(Resource.Id.list_TreatmentPriceText);
        }
    }
}