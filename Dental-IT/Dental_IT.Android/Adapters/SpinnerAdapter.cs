using Android.Content;
using Android.Views;
using Android.Widget;

namespace Dental_IT.Droid.Adapters
{
    class SpinnerAdapter : BaseAdapter, ISpinnerAdapter
    {
        private readonly Context context;
        private string[] items;

        public SpinnerAdapter(Context c, string[] i)
        {
            context = c;
            items = i;
        }

        public override int Count
        {
            get { return items.Length; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return items[position];
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);

                //  Custom dropdown view
                convertView = inflater.Inflate(Resource.Layout.sublayout_Spinner_View, null);

                TextView itemText = convertView.FindViewById<TextView>(Resource.Id.spinnerItem);
                itemText.Text = items[position];
            }

            return convertView;
        }

        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);

                //  Default dropdown item view
                convertView = inflater.Inflate(Android.Resource.Layout.SimpleSpinnerDropDownItem, parent, false);

                ((TextView)convertView).Text = items[position];
            }

            return convertView;
        }
    }
}