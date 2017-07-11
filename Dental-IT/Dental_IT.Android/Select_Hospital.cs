using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;

namespace Dental_IT.Droid
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Select_Hospital : Activity
    {
        public static int LIST_HEIGHT;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to select hospital layout
            SetContentView(Resource.Layout.Select_Hospital);

            //  Create widgets
            RecyclerView selectHospital_RecyclerView = FindViewById<RecyclerView>(Resource.Id.selectHospital_RecyclerView);

            RunOnUiThread(() =>
            {
                //  Configure custom adapter for recyclerview
                selectHospital_RecyclerView.Post(() =>
                {
                    LIST_HEIGHT = selectHospital_RecyclerView.Height;
                    selectHospital_RecyclerView.SetLayoutManager(new LinearLayoutManager(this));

                    RecyclerAdapter adapter = new RecyclerAdapter(this);
                    selectHospital_RecyclerView.SetAdapter(adapter);
                });
            });

            //Implement CustomTheme ActionBar
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "Select Hospital/Clinics ";

            //Set backarrow as Default
            ActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        //Readonly of list of hospitals in search bars
        private readonly List<string> Hospitals = new List<string>
        {
            "Tan Tock Seng Hospital", "Pristine Dentalworks", "DP Dental", "Smile Dental Group"
        };

        //Implement menus in the action bar; backarrow
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            return true;
        }


        //Toast displayed and redirected to SignIn page when back arrow is tapped
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Intent intent = new Intent(this, typeof(Main_Menu));
            StartActivity(intent);

            Toast.MakeText(this, "Main Menu" + item.TitleFormatted,
                ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }
    }

    public class RecyclerAdapter : RecyclerView.Adapter
    {
        private readonly Activity activity;
        public event EventHandler<int> ItemClick;

        public RecyclerAdapter(Activity a)
        {
            activity = a;
        }

        public override int ItemCount
        {
            get
            {
                return nameTexts.Length;
            }
        }

        private void OnClick(int position)
        {
            ItemClick?.Invoke(this, position);
        }

        private void OnItemClick(object sender, int position)
        {
            int buttonNum = position + 1;
            Toast.MakeText(activity, "This is button number " + buttonNum, ToastLength.Short).Show();
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
            ViewHolder vh = holder as ViewHolder;

            //  Set height of row
            vh.ItemView.LayoutParameters.Height = Select_Hospital.LIST_HEIGHT / 6;

            //  Set alternating background of row
            int type = GetItemViewType(position);

            if (type == 0)
            {
                vh.ItemView.SetBackgroundResource(Resource.Color._8_white);
            }

            vh.hospitalName.Text = nameTexts[position];
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View view = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.sublayout_Hospital_List_Item, parent, false);
            ViewHolder holder = new ViewHolder(view, OnClick);

            holder.ItemView.Click += delegate
            {
                Intent intent = new Intent(activity, typeof(Request_Appointment));
                intent.PutExtra("hospitalName", holder.hospitalName.Text);
                activity.StartActivity(intent);
            };

            ItemClick -= OnItemClick;
            ItemClick += OnItemClick;

            return holder;
        }

        private readonly string[] nameTexts =
        {
            "Hospital 1",
            "Hospital 2",
            "Hospital 3",
            "Hospital 4",
            "Hospital 5",
            "Hospital 6",
            "Hospital 7",
            "Hospital 8",
            "Hospital 9",
            "Hospital 10",
            "Hospital 11",
            "Hospital 12",
            "Hospital 13",
            "Hospital 14",
            "Hospital 15",
            "Hospital 16",
            "Hospital 17",
            "Hospital 18",
            "Hospital 19",
            "Hospital 20",
            "Hospital 21",
            "Hospital 22",
            "Hospital 23",
            "Hospital 24",
            "Hospital 25"
        };
    }

    class ViewHolder : RecyclerView.ViewHolder
    {
        public TextView hospitalName { get; set; }
        public ToggleButton hospitalFavourites { get; set; }

        public ViewHolder(View view, Action<int> listener) : base(view)
        {
            hospitalName = view.FindViewById<TextView>(Resource.Id.selectHospital_HospitalText);
            hospitalFavourites = view.FindViewById<ToggleButton>(Resource.Id.selectHospital_FavouritesToggle);

            hospitalFavourites.Click += (sender, e) => listener(AdapterPosition);
        }
    }
}