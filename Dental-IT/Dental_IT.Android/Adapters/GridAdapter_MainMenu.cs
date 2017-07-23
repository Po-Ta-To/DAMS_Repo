using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Dental_IT.Droid.Adapters
{
    class GridAdapter_MainMenu : BaseAdapter
    {
        private readonly Context context;
        private int numRows = 2;
        private string[] items;

        public GridAdapter_MainMenu(Context c, string[] i)
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
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Button button;

            if (convertView == null)
            {
                button = new Button(context);

                button.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                button.SetBackgroundColor(new Android.Graphics.Color(Resource.Color.blue));
                button.SetTextColor(new Android.Graphics.Color(Resource.Color.white));
                button.SetHeight(Main_Menu.GRID_HEIGHT / numRows);
                button.SetAllCaps(false);
                button.Text = items[position];

                switch (position)
                {
                    case 0:
                        button.Click += delegate
                        {
                            Intent intent = new Intent(context, typeof(Select_Hospital));
                            context.StartActivity(intent);
                        };
                        break;

                    case 1:
                        button.Click += delegate
                        {
                            Intent intent = new Intent(context, typeof(My_Appointments));
                            context.StartActivity(intent);
                        };
                        break;
                    case 2:
                        button.Click += delegate
                        {
                            Intent intent = new Intent(context, typeof(Treatment_Information));
                            context.StartActivity(intent);
                        };
                        break;
                    case 3:
                        button.Click += delegate
                        {
                            Intent intent = new Intent(context, typeof(Search));
                            context.StartActivity(intent);
                        };
                        break;
                }
            }
            else
            {
                button = (Button)convertView;
            }

            return button;
        }
    }
}