using System;
using System.Collections.Generic;
using System.Text;

namespace Dental_IT
{
    public static class Web_Config
    {
        // CREATE/POST a new public user
        public static string global_connURL_createUser { get; } = "http://localhost:49813/api/UserAccounts";

        // GET Appointments by UserID
        public static string global_connURL_getApptByID { get; } = "/api/AppointmentsByUser/"; // + UserID

        // GET Appointment details by Apptointment ID
        public static string global_connURL_getApptDetailsById { get; } = "/api/Appointments/"; // + ApptID

        // CREATE/POST a new Appointment
        public static string global_connURL_createAppointment { get; } = "/api/Appointments";

        // UPDATE Appointment Details by Appointment ID
        public static string global_connURL_updateApptDetails { get; } = "/api/Appointments/"; // + ApptID

        // DELETE Appointment by ApptID 
        public static string global_connURL_deleteApptByID { get; } = "/api/Appointments/"; // + ApptID

        // GET all Treatments
        public static string global_connURL_getTreatment { get; } = "/api/Treatments";

        // GET Treatment details by Treatment ID
        public static string global_connURL_getTreatmentDetailsById { get; } = "/api/Treatments/";

        // GET Treatments by Hospital Clinic ID
        public static string global_connURL_getTreatmentsByCHId { get; } = "/api/TreatmentsByH/"; // + CH id

        // GET all Hospitals 
        public static string global_connURL_getAllHospitals { get; } = "/api/ClinicHospitals";

        // GET Hospital Detatils Clinic Hospital ID
        public static string global_connURL_getClinicHospitalID { get; } = "/api/ClinicHospitals/"; // + CH id

        // GET Hospital Clinic by Treatment ID
        public static string global_connURL_getClinicHospitalsByTreatmentID { get; } = "/api/ClinicHospitalsByTreatment/"; // + Treatment ID

        // GET Opening Hours by Hospital Clinic ID
        public static string global_connURL_getOpeningHoursByCHid { get; } = "/api/OpeningHours/"; // + CH id

        // GET Bookings (isFullyBooked) by Hospital Clinic ID
        public static string global_connURL_getBookingsByCHid { get; } = "/api/Bookings/"; // + CH id

        // GET Doctor Dentists by Hospital Clinic ID
        public static string global_connURL_getDocDentistsByCHid { get; } = "/api/DoctorDentistsByH/"; // + CH id

        // GET TimeSlot by Hospital Clinic ID
        public static string global_connURL_getClinicHospitalTimeSlotByCHid { get; } = "/api/ClinicHospitalTimeSlotByH/"; // + CH id
    }
}
