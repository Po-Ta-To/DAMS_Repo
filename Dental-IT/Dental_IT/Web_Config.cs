namespace Dental_IT
{
    public static class Web_Config
    {
        private static string conn = "http://192.168.137.1:49814";

        // CREATE/POST a new public user
        public static string global_connURL_createUser { get; } = conn + "/api/UserAccounts";

        // GET Appointments by UserID
        public static string global_connURL_getApptByID { get; } = conn + "/api/AppointmentsByUser/"; // + UserID

        // GET Appointment details by Apptointment ID
        public static string global_connURL_getApptDetailsById { get; } = conn + "/api/Appointments/"; // + ApptID

        // CREATE/POST a new Appointment
        public static string global_connURL_createAppointment { get; } = conn + "/api/Appointments";

        // UPDATE Appointment Details by Appointment ID
        public static string global_connURL_updateApptDetails { get; } = conn + "/api/Appointments/"; // + ApptID

        // DELETE Appointment by ApptID 
        public static string global_connURL_deleteApptByID { get; } = conn + "/api/Appointments/"; // + ApptID

        // GET all Treatments
        public static string global_connURL_getTreatment { get; } = conn + "/api/Treatments";

        // GET Treatment details by Treatment ID
        public static string global_connURL_getTreatmentDetailsById { get; } = conn + "/api/Treatments/";

        // GET Treatments by Hospital Clinic ID
        public static string global_connURL_getTreatmentsByCHId { get; } = conn + "/api/TreatmentsByH/"; // + CH id

        // GET all Hospitals 
        public static string global_connURL_getAllHospitals { get; } = conn + "/api/ClinicHospitals";

        // GET Hospital Detatils Clinic Hospital ID
        public static string global_connURL_getCHDetailsById { get; } = conn + "/api/ClinicHospitals/"; // + CH id

        // GET Hospital Clinic by Treatment ID
        public static string global_connURL_getClinicHospitalsByTreatmentID { get; } = conn + "/api/ClinicHospitalsByTreatment/"; // + Treatment ID

        // GET Opening Hours by Hospital Clinic ID
        public static string global_connURL_getOpeningHoursByCHid { get; } = conn + "/api/OpeningHours/"; // + CH id

        // GET Bookings (isFullyBooked) by Hospital Clinic ID
        public static string global_connURL_getBookingsByCHid { get; } = conn + "/api/Bookings/"; // + CH id

        // GET Doctor Dentists by Hospital Clinic ID
        public static string global_connURL_getDocDentistsByCHid { get; } = conn + "/api/DoctorDentistsByH/"; // + CH id

        // GET TimeSlot by Hospital Clinic ID
        public static string global_connURL_getClinicHospitalTimeSlotByCHid { get; } = conn + "/api/ClinicHospitalTimeSlotByH/"; // + CH id
    }
}