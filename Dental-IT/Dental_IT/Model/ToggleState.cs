namespace Dental_IT.Model
{
    public class ToggleState
    {
        private int _id;
        private bool _toggled;

        public ToggleState(int id, bool toggled = false)
        {
            _id = id;
            _toggled = toggled;
        }

        public int id
        {
            get { return _id; }
        }

        public bool toggled
        {
            get { return _toggled; }
            set { _toggled = value; }
        }
    }
}
