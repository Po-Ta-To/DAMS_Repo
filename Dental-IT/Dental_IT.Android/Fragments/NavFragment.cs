
using Android.App;
using Android.OS;
using Android.Views;

namespace Dental_IT.Droid.Fragments
{
    public class NavFragment : Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here

            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            var view = inflater.Inflate(Resource.Layout.Main_Menu, container, false);
            return view;

            var requestAppt = inflater.Inflate(Resource.Layout.Request_Appointment, container, false);
            return view;


            return base.OnCreateView(inflater, container, savedInstanceState);
        }
    }
}