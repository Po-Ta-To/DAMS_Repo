using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Dental_IT.Droid.Adapters;

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
            GridView mainMenu_GridView = FindViewById<GridView>(Resource.Id.mainMenu_GridView);

            RunOnUiThread(() =>
            {
                //  Get screen height
                SCREEN_HEIGHT = Resources.DisplayMetrics.HeightPixels;

                //  Get actionbar height
                Android.Content.Res.TypedArray styledAttributes = Theme.ObtainStyledAttributes(new int[] { Android.Resource.Attribute.ActionBarSize });
                ACTIONBAR_HEIGHT = (int)styledAttributes.GetDimension(0, 0);
                styledAttributes.Recycle();

                //  Configure grid adapter for menu buttons
                mainMenu_GridView.Post(() =>
                {
                    GRID_HEIGHT = mainMenu_GridView.Height;
                    mainMenu_GridView.Adapter = new GridAdapter(this, buttonTexts);
                });
            });

            //Implement CustomTheme ActionBar(toolbar)
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "Dental-It";

            //Set menu hambuger
            ActionBar.SetHomeAsUpIndicator(Resource.Drawable.ic_menu);

            ActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        //  List of button texts to popular grid adapter
        private readonly string[] buttonTexts =
        {
            "Button 1",
            "Button 2",
            "Button 3",
            "Button 4"
        };
    }
}