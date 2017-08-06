using System;

namespace Dental_IT.Model
{
    public class Appointment
    {
        public int ID { get; set; }
        public string Treatments { get; set; }
        public string Dentist { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}
