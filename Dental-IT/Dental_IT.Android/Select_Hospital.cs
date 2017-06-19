using Android.App;
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
            ListView listView = FindViewById<ListView>(Resource.Id.hospitalListView);

            //  Configure custom adapter for listview
            listView.Post(() =>
            {
                LIST_HEIGHT = listView.Height;
                listView.Adapter = new ListAdapter(this);
            });
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
            View view;

            if (convertView == null)
            {
                view = convertView ?? activity.LayoutInflater.Inflate(Resource.Layout.Hospital_List_Item, parent, false);

                TextView hospitalName = view.FindViewById<TextView>(Resource.Id.selectHospitalText);
                var param = view.LayoutParameters;
                param.Height = Select_Hospital.LIST_HEIGHT / 6;

                hospitalName.Text = nameTexts[position];

                if (position % 2 == 1)
                {
                    view.SetBackgroundResource(Resource.Color._8_white);
                }
            }
            else
            {
                view = convertView;
            }

            return view;
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
            "Hospital 9"
        };
    }
}