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
            GridView gridView = FindViewById<GridView>(Resource.Id.mainMenuGridView);

            //  Get screen height
            SCREEN_HEIGHT = Resources.DisplayMetrics.HeightPixels;

            //  Get actionbar height
            Android.Content.Res.TypedArray styledAttributes = Theme.ObtainStyledAttributes(new int[] { Android.Resource.Attribute.ActionBarSize });
            ACTIONBAR_HEIGHT = (int)styledAttributes.GetDimension(0, 0);
            styledAttributes.Recycle();

            //  Configure custom adapter for gridview
            gridView.Post(() => {
                GRID_HEIGHT = gridView.Height;
                gridView.Adapter = new GridAdapter(this);
            });            
        }
    }

    //  Class for gridview custom adapter
    class GridAdapter : BaseAdapter
    {
        private readonly Context context;
        private int numRows = 2;

        public GridAdapter(Context c)
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
            button.SetBackgroundColor(Android.Graphics.Color.Gray);
            button.SetHeight(Main_Menu.GRID_HEIGHT / numRows);
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