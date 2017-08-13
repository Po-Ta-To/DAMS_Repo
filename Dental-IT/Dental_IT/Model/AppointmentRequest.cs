namespace Dental_IT.Model
{
    class AppointmentRequest
    {
        public string AppointmentID { get; set; }
        public int UserID { get; set; }
        public int ClinicHospitalID { get; set; }
        public string PreferredDate { get; set; }
        public int PreferredTime { get; set; }
        public int RequestDoctorDentistID { get; set; }
        public string Remarks { get; set; }
        public int[] Treatments { get; set; }
    }
}
