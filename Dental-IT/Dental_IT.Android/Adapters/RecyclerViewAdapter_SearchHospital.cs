using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Dental_IT.Droid.Main;

namespace Dental_IT.Droid.Adapters
{
    class RecyclerViewAdapter_SearchHospital : RecyclerView.Adapter
    {
        private readonly Context context;
        private List<Hospital> hospitalList;
        public static List<string> prefList;
        private List<ToggleState> tempFavouriteList;
        public event EventHandler<int> ItemClick;

        public RecyclerViewAdapter_SearchHospital(Context c, List<Hospital> l, List<string> p, List<ToggleState> temp)
        {
            context = c;
            hospitalList = l;
            prefList = p;
            tempFavouriteList = temp;
        }

        public override int ItemCount
        {
            get
            {
                return hospitalList.Count;
            }
        }

        private void OnClick(int position)
        {
            ItemClick?.Invoke(this, position);
        }

        private void OnItemClick(object sender, int position)
        {
            if (tempFavouriteList[position].toggled == false)
            {
                tempFavouriteList[position].toggled = true;

                prefList.Add(tempFavouriteList[position].id);

                Toast.MakeText(context, hospitalList[position].name + " added to favourites!", ToastLength.Short).Show();
            }
            else
            {
                tempFavouriteList[position].toggled = false;

                prefList.Remove(prefList.Find(e => (e == tempFavouriteList[position].id)));

                Toast.MakeText(context, hospitalList[position].name + " removed from favourites!", ToastLength.Short).Show();
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
            SearchHospital_ViewHolder vh = holder as SearchHospital_ViewHolder;

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
            vh.hospitalFavourites.Checked = tempFavouriteList[position].toggled;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.sublayout_Hospital_List_Item, parent, false);
            SearchHospital_ViewHolder holder = new SearchHospital_ViewHolder(view, OnClick);

            holder.ItemView.Click += delegate
            {
                Intent intent = new Intent(context, typeof(Hospital_Details));
                intent.PutExtra("details_HospitalName", holder.hospitalName.Text);
                context.StartActivity(intent);
            };

            ItemClick -= OnItemClick;
            ItemClick += OnItemClick;

            return holder;
        }

        public void Replace(List<Hospital> filteredList)
        {
            hospitalList = filteredList;
            NotifyDataSetChanged();
        }
    }

    class SearchHospital_ViewHolder : RecyclerView.ViewHolder
    {
        public TextView hospitalName { get; set; }
        public ToggleButton hospitalFavourites { get; set; }

        public SearchHospital_ViewHolder(View view, Action<int> listener) : base(view)
        {
            hospitalName = view.FindViewById<TextView>(Resource.Id.list_HospitalText);
            hospitalFavourites = view.FindViewById<ToggleButton>(Resource.Id.list_FavouritesToggle);

            hospitalFavourites.Click += (sender, e) => listener(AdapterPosition);
        }
    }
}