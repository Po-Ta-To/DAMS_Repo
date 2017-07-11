using System;

namespace Dental_IT
{
	public class Hospital_Favourites
	{
        private int _id;
        private bool _favourited;

		public Hospital_Favourites (int id, bool favourited)
		{
            _id = id;
            _favourited = favourited;
		}

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }

        public bool favourited
        {
            get { return _favourited; }
            set { _favourited = value; }
        }
	}
}

