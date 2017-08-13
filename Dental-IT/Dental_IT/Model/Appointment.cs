using System;

namespace Dental_IT.Model
{
    public class Appointment
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Treatments { get; set; }
        public string ClinicHospital { get; set; }
        public string Dentist { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }
    }
}
