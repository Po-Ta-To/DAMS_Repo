﻿namespace Dental_IT
{
    public static class Web_Config
    {
        //  Amazon AWS
        private static string conn = "http://ec2-54-214-60-226.us-west-2.compute.amazonaws.com";

        //  Local URL
        //private static string conn = "http://192.168.137.1:49814";

        // POST user to get token (Sign in)
        public static string global_connURL_postToken { get; } = conn + "/token";

        // GET user account with token (Sign in)
        public static string global_connURL_getUser { get; } = conn + "/api/UserAccountByUN";

        // CREATE/POST a new public user
        public static string global_connURL_createUser { get; } = conn + "/api/UserAccounts";

        // GET Appointments by UserID
        public static string global_connURL_getAppt { get; } = conn + "/api/AppointmentsByUser/";

        // GET Appointment details by Appointment ID
        public static string global_connURL_getApptDetailsById { get; } = conn + "/api/Appointments/"; // + ApptID

        // CREATE/POST a new Appointment
        public static string global_connURL_createAppointment { get; } = conn + "/api/Appointments";

        // UPDATE Appointment Details by Appointment ID
        public static string global_connURL_updateApptDetails { get; } = conn + "/api/Appointments"; // + ApptID

        // CANCEL Appointment by ApptID 
        public static string global_connURL_cancelApptByID { get; } = conn + "/api/CancelAppointment/"; // + ApptID

        // GET all Treatments
        public static string global_connURL_getTreatment { get; } = conn + "/api/Treatments";

        // GET Treatment details by Treatment ID
        public static string global_connURL_getTreatmentDetailsById { get; } = conn + "/api/Treatments/";

        // GET Treatments by Clinic Hospital ID
        public static string global_connURL_getTreatmentsByCHId { get; } = conn + "/api/TreatmentsByH/"; // + CH id

        // GET all Hospitals 
        public static string global_connURL_getAllHospitals { get; } = conn + "/api/ClinicHospitals";

        // GET Clinic Hospital Details by Clinic Hospital ID
        public static string global_connURL_getCHDetailsById { get; } = conn + "/api/ClinicHospitals/"; // + CH id

        // GET Clinic Hospital by Treatment ID
        public static string global_connURL_getClinicHospitalsByTreatmentID { get; } = conn + "/api/ClinicHospitalsByTreatment/"; // + Treatment ID

        // GET Opening Hours by Clinic Hospital ID
        public static string global_connURL_getOpeningHoursByCHid { get; } = conn + "/api/OpeningHours/"; // + CH id

        // GET Bookings (isFullyBooked) by Clinic Hospital ID
        public static string global_connURL_getBookingsByCHid { get; } = conn + "/api/Bookings/"; // + CH id

        // GET Doctor Dentists by Clinic Hospital ID
        public static string global_connURL_getDocDentistsByCHid { get; } = conn + "/api/DoctorDentistsByH/"; // + CH id

        // GET TimeSlot by Clinic Hospital ID
        public static string global_connURL_getClinicHospitalTimeSlotByCHid { get; } = conn + "/api/ClinicHospitalTimeSlotByH/"; // + CH id
    }
}