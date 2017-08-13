using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Dental_IT.Model;

namespace Dental_IT.Droid.Adapters
{
    class SpinnerAdapter_Session : BaseAdapter, ISpinnerAdapter
    {
        private readonly Context context;
        private List<Session> items;

        public SpinnerAdapter_Session(Context c, List<Session> i)
        {
            context = c;
            items = i;
        }

        public override int Count
        {
            get { return items.Count; }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
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
                itemText.Text = items[position].SlotString;
            }

            return convertView;
        }

        public override View GetDropDownView(int position, View convertView, ViewGroup parent)
        {
            View spinnerView;

            if (convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);

                //  Default dropdown item view
                spinnerView = inflater.Inflate(Android.Resource.Layout.SimpleSpinnerDropDownItem, parent, false);
            }
            else
            {
                spinnerView = convertView;
            }

            if (items[position].SlotID == 0)
            {
                ((TextView)spinnerView).Text = "";
            }
            else
            {
                ((TextView)spinnerView).Text = items[position].SlotString;
            }

            return spinnerView;
        }
    }
}