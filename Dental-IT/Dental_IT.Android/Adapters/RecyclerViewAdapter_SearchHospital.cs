using System;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.Support.V7.Widget;
using System.Collections.Generic;
using Android.Preferences;

namespace Dental_IT.Droid.Adapters
{
    class RecyclerViewAdapter_SearchHospital : RecyclerView.Adapter
    {
        private readonly Context context;
        private List<Hospital> hospitalList;
        public static List<int> prefList;
        private List<Favourite> tempFavouriteList;
        public event EventHandler<int> ItemClick;
        ISharedPreferences prefs;

        public RecyclerViewAdapter_SearchHospital(Context c, List<Hospital> l, List<int> f, List<Favourite> temp)
        {
            context = c;
            hospitalList = l;
            prefList = f;
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
            if (tempFavouriteList[position].favourited == false)
            {
                tempFavouriteList[position].favourited = true;

                prefList.Add(tempFavouriteList[position].id);

                Toast.MakeText(context, hospitalList[position].name + " added to favourites!", ToastLength.Short).Show();
            }
            else
            {
                tempFavouriteList[position].favourited = false;

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

            vh.hospitalName.Text = hospitalList[position].name;
            vh.hospitalFavourites.Checked = tempFavouriteList[position].favourited;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.sublayout_Hospital_List_Item, parent, false);
            SearchHospital_ViewHolder holder = new SearchHospital_ViewHolder(view, OnClick);

            //  Get list of favourites from shared preferences
            prefs = PreferenceManager.GetDefaultSharedPreferences(context);

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