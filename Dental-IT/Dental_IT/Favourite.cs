namespace Dental_IT
{
    public class Favourite
    {
        private int _id;
        private bool _favourited;

        public Favourite(int id, bool favourited = false)
        {
            _id = id;
            _favourited = favourited;
        }

        public int id
        {
            get { return _id; }
        }

        public bool favourited
        {
            get { return _favourited; }
            set { _favourited = value; }
        }
    }
}
