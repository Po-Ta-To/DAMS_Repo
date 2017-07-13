using System;
using System.Collections.Generic;
using System.Text;

namespace Dental_IT
{
    class Hospital
    {
        private int _id;
        private string _name;
        private bool _favourited;

        public Hospital(int id, string name, bool favourited = false)
        {
            _id = id;
            _name = name;
            _favourited = favourited;
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

        public bool favourited
        {
            get { return _favourited; }
            set { _favourited = value; }
        }
    }
}
