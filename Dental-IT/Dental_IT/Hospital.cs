namespace Dental_IT
{
    class Hospital
    {
        private int _id;
        private string _name;

        public Hospital(int id, string name)
        {
            _id = id;
            _name = name;
        }

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
