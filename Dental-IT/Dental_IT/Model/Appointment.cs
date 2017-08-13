using System;
using System.Collections.Generic;

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

        // For POST & PUT Appt
        public string AppointmentID { get; set; }
        public int ClinicHospitalID { get; set; }
        public string PreferredDate { get; set; }
        public int PreferredTime { get; set; }
        public int RequestDoctorDentistID { get; set; }
        public string Remarks { get; set; }
    }
}
