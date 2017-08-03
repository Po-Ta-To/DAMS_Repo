namespace Dental_IT
{
    class Hospital
    {
        private string _id;
        private string _name;

        public Hospital(string id, string name)
        {
            _id = id;
            _name = name;
        }

        public string id
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
