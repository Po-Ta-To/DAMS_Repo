using Android.Content;
using Android.Views;
using Android.Widget;
using Dental_IT.Droid.Main;

namespace Dental_IT.Droid.Adapters
{
    class GridAdapter_MainMenu : BaseAdapter
    {
        private readonly Context context;
        private int[] items;

        public GridAdapter_MainMenu(Context c, int[] i)
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
            ImageButton button;

            if (convertView == null)
            {
                button = new ImageButton(context);

                button.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                button.SetImageResource(items[position]);
                button.SetScaleType(ImageView.ScaleType.CenterCrop);
                button.Background = null;
                button.SetPadding(0, 0, 0, 0);

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
                button = (ImageButton)convertView;
            }

            return button;
        }
    }
}