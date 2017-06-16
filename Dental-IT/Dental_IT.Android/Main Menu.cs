using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Dental_IT.Droid
{
    [Activity(ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Main_Menu : Activity
    {
        public static int SCREEN_HEIGHT;
        public static int ACTIONBAR_HEIGHT;
        public static int GRID_HEIGHT;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //  Set view to main menu layout
            SetContentView(Resource.Layout.Main_Menu);

            //  Create widgets
            GridView gridview = FindViewById<GridView>(Resource.Id.mainMenuGridview);

            //  Get screen height
            SCREEN_HEIGHT = Resources.DisplayMetrics.HeightPixels;

            //  Get actionbar height
            Android.Content.Res.TypedArray styledAttributes = Theme.ObtainStyledAttributes(new int[] { Android.Resource.Attribute.ActionBarSize });
            ACTIONBAR_HEIGHT = (int)styledAttributes.GetDimension(0, 0);
            styledAttributes.Recycle();

            ////  Get gridview height
            //ViewTreeObserver vto = gridview.ViewTreeObserver;
            //vto.GlobalLayout += (sender, args) =>
            //{
            //    if (gridview.Height > 0)
            //    {
            //        GRID_HEIGHT = gridview.Height;
                        
            //        //  Configure gridview            
            //        gridview.Adapter = new CustomAdapter(this);

            //        return;
            //    }
            //};

            gridview.Post(() => {
                GRID_HEIGHT = gridview.Height;
                gridview.Adapter = new CustomAdapter(this);
            });            
        }
    }

    class CustomAdapter : BaseAdapter
    {
        private readonly Context context;
        private int numRows = 2;

        public CustomAdapter(Context c)
        {
            context = c;
        }

        public override int Count
        {
            get { return buttonTexts.Length; }
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
            View view;

            if (convertView == null)
            {
                // if it's not recycled, initialize some attributes
                button = new Button(context);
            }
            else
            {
                button = (Button)convertView;
            }

            button.LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
            //imageButton.SetBackgroundColor(Android.Graphics.Color.Transparent);
            button.SetBackgroundColor(Android.Graphics.Color.Gray);
            button.SetHeight(Main_Menu.GRID_HEIGHT / numRows);
            //button.SetHeight(Convert.ToInt32((Main_Menu.SCREEN_HEIGHT - Main_Menu.ACTIONBAR_HEIGHT) * 0.6) / numRows);
            button.Text = buttonTexts[position];
            return button;
        }

        private readonly string[] buttonTexts =
        {
            "Button 1",
            "Button 2",
            "Button 3",
            "Button 4"
        };
    }
}