using System;

namespace Dental_IT.Model
{
    public class Appointment
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int[] Treatments { get; set; }
        public string TreatmentsName { get; set; }
        public string ClinicHospital { get; set; }
        public string DentistName { get; set; }
        public DateTime Date { get; set; }
        public string TimeString { get; set; }
        public string Status { get; set; }

        //  For request and update
        public string AppointmentID { get; set; }
        public int ClinicHospitalID { get; set; }
        public string PreferredDate { get; set; }
        public int PreferredTime { get; set; }
        public int AppointmentTime { get; set; }
        public int RequestDoctorDentistID { get; set; }
        public int DoctorDentistID { get; set; }
        public string Remarks { get; set; }
    }
}
