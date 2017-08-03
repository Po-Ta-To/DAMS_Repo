namespace Dental_IT
{
    public class ToggleState
    {
        private string _id;
        private bool _toggled;

        public ToggleState(string id, bool toggled = false)
        {
            _id = id;
            _toggled = toggled;
        }

        public string id
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
