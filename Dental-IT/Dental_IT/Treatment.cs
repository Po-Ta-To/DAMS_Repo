﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dental_IT
{
    class Treatment
    {
        private int _id;
        private string _name;
        private double _minPrice;
        private double _maxPrice;
        private string _description;

        public Treatment(int id, string name, double minPrice, double maxPrice, string description = "")
        {
            _id = id;
            _name = name;
            _minPrice = minPrice;
            _maxPrice = maxPrice;
            _description = description;

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

        public double minPrice
        {
            get { return _minPrice; }
            set { _minPrice = value; }
        }

        public double maxPrice
        {
            get { return _maxPrice; }
            set { _maxPrice = value; }
        }

        public string description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}