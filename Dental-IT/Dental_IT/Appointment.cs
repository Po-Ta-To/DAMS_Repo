namespace Dental_IT
{
    class Appointment
    {
        private int _id;
        private string _treatments;
        private string _dentist;
        private string _date;
        private string _time;

        public Appointment(int id, string treatments, string dentist, string date, string time)
        {
            _id = id;
            _treatments = treatments;
            _dentist = dentist;
            _date = date;
            _time = time;
        }

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string treatments
        {
            get { return _treatments; }
            set { _treatments = value; }
        }

        public string dentist
        {
            get { return _dentist; }
            set { _dentist = value; }
        }

        public string date
        {
            get { return _date; }
            set { _date = value; }
        }

        public string time
        {
            get { return _time; }
            set { _time = value; }
        }
    }
}
