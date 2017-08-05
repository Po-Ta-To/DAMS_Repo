using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Dental_IT.Droid.Main;
using static Android.Views.View;

namespace Dental_IT.Droid.Adapters
{
    class GridAdapter_MainMenu : BaseAdapter
    {
        private readonly Context context;
        private int[] buttonImages;
        private int numRows = 2;

        public GridAdapter_MainMenu(Context c, int[] i)
        {
            context = c;
            buttonImages = i;
        }

        public override int Count
        {
            get { return buttonImages.Length; }
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
            MyImageButton button;

            if (convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);

                //  Grid buttons
                convertView = inflater.Inflate(Resource.Layout.sublayout_Menu_Button, null);

                button = convertView.FindViewById<MyImageButton>(Resource.Id.mainMenu_ImgBtn);
          
                button.SetMinimumHeight(Main_Menu.GRID_HEIGHT / numRows);

                //TypedValue tv = new TypedValue();
                //context.Theme.ResolveAttribute(Resource.Attribute.selectableItemBackground, tv, true);
                //button.SetBackgroundResource(tv.ResourceId);

                //  This code is another method that works similarly to the above
                //int[] attrs = new int[] { Android.Resource.Attribute.SelectableItemBackground };
                //TypedArray ta = context.ObtainStyledAttributes(attrs);
                //Drawable drawableFromTheme = ta.GetDrawable(0);
                //ta.Recycle();
                //button.SetBackgroundDrawable(drawableFromTheme);

                button.SetImageResource(buttonImages[position]);

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

            return convertView;
        }
    }

    class MyImageButton : ImageButton, IOnTouchListener
    {
        private Context context;

        public MyImageButton(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            this.context = context;
            this.SetOnTouchListener(this);
        }

        public bool OnTouch(View v, MotionEvent e)
        {
            if (e.Action == MotionEventActions.Down)
            {
                ImageView iv = (ImageView)v;
                iv.SetColorFilter(new Android.Graphics.Color(context.GetColor(Resource.Color._5_grey)));
            }
            else if (e.Action == MotionEventActions.Up)
            {
                ImageView iv = (ImageView)v;
                iv.ClearColorFilter();
            }
            return true;
        }
    };
}