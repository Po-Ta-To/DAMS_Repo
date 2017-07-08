﻿using System;
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
    class GridAdapter : BaseAdapter
    {
        private readonly Context context;
        private int numRows = 2;
        private string[] items;

        public GridAdapter(Context c, string[] i)
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
                button.SetBackgroundColor(Android.Graphics.Color.Gray);
                button.SetHeight(Main_Menu.GRID_HEIGHT / numRows);
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
                            Intent intent = new Intent(context, typeof(Appointment_Details));
                            context.StartActivity(intent);
                        };
                        break;
                    case 2:
                        button.Click += delegate
                        {
                            Intent intent = new Intent(context, typeof(Hospital_Details));
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