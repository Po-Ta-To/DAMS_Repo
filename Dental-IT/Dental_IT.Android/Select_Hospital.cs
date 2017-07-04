using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

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
            ListView selectHospital_ListView = FindViewById<ListView>(Resource.Id.selectHospital_ListView);

            //  Configure custom adapter for listview
            selectHospital_ListView.Post(() =>
            {
                LIST_HEIGHT = selectHospital_ListView.Height;
                selectHospital_ListView.Adapter = new ListAdapter(this);
            });

            selectHospital_ListView.FastScrollEnabled = true;
        }
    }

    public class ListAdapter : BaseAdapter
    {
        private readonly Activity activity;

        public ListAdapter(Activity a)
        {
            activity = a;
        }

        public override int Count
        {
            get { return nameTexts.Length; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder holder;
            View view = convertView;
            int type;

            if (view == null)
            {
                holder = new ViewHolder();

                view = activity.LayoutInflater.Inflate(Resource.Layout.sublayout_Hospital_List_Item, parent, false);
                holder.hospitalName = view.FindViewById<TextView>(Resource.Id.selectHospital_HospitalText);
                holder.hospitalFavourites = view.FindViewById<ToggleButton>(Resource.Id.selectHospital_FavouritesToggle);
                holder.position = position;
                System.Diagnostics.Debug.WriteLine(holder.position);

                //  Positions are not working correctly, this is a temporary solution
                view.Click += delegate
                {
                    Intent intent = new Intent(activity, typeof(Request_Appointment));
                    intent.PutExtra("hospitalName", holder.hospitalName.Text);
                    activity.StartActivity(intent);
                };

                holder.hospitalFavourites.SetOnCheckedChangeListener(null);
                holder.hospitalFavourites.Click += delegate
                {
                    holder.hospitalFavourites.Text = position.ToString();
                };

                view.Tag = holder;
            }
            else
            {
                holder = view.Tag as ViewHolder;
            }            
            
            //  Set height of row
            view.LayoutParameters.Height = Select_Hospital.LIST_HEIGHT / 6;

            //  Set alternating background of row
            type = GetItemViewType(holder.position);

            if (type == 0)
            {
                view.SetBackgroundResource(Resource.Color._8_white);
            }

            //  Row contents
            holder.hospitalName.Text = nameTexts[position];
            holder.hospitalFavourites.Text = position.ToString();

            return view;
        }

        //  Number of different rows
        public override int ViewTypeCount
        {
            get
            {
                return 2;
            }
        }

        //  To alternate rows
        public override int GetItemViewType(int position)
        {
            return position % 2 == 1 ? 0 : 1;
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

    class ViewHolder : Java.Lang.Object
    {
        public TextView hospitalName { get; set; }
        public ToggleButton hospitalFavourites { get; set; }
        public int position { get; set; }
    }
}