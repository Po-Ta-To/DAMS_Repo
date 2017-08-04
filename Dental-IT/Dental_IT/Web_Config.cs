using System;
using System.Collections.Generic;
using System.Text;

namespace Dental_IT
{
    public static class Web_Config
    {
        // CREATE/POST a new public user
        public static string global_connURL_createUser { get; } = "192.168.137.1:49814/api/UserAccounts";

        // GET Appointments by UserID
        public static string global_connURL_getApptByID { get; } = "192.168.137.1:49814/api/AppointmentsByUser/"; // + UserID

        // GET Appointment details by Apptointment ID
        public static string global_connURL_getApptDetailsById { get; } = "192.168.137.1:49814/api/Appointments/"; // + ApptID

        // CREATE/POST a new Appointment
        public static string global_connURL_createAppointment { get; } = "192.168.137.1:49814/api/Appointments";

        // UPDATE Appointment Details by Appointment ID
        public static string global_connURL_updateApptDetails { get; } = "192.168.137.1:49814/api/Appointments/"; // + ApptID

        // DELETE Appointment by ApptID 
        public static string global_connURL_deleteApptByID { get; } = "192.168.137.1:49814/api/Appointments/"; // + ApptID

        // GET all Treatments
        public static string global_connURL_getTreatment { get; } = "192.168.137.1:49814/api/Treatments";

        // GET Treatment details by Treatment ID
        public static string global_connURL_getTreatmentDetailsById { get; } = "192.168.137.1:49814/api/Treatments/";

        // GET Treatments by Hospital Clinic ID
        public static string global_connURL_getTreatmentsByCHId { get; } = "192.168.137.1:49814/api/TreatmentsByH/"; // + CH id

        // GET Highest & Lowest Treatment Prices
        public static string global_connURL_GetTreatmentHighestLowestPrice { get; } = conn + "/api/ClinicHospitalTreatmentPrice/"; // + Treatment id

        // GET all Hospitals 
        public static string global_connURL_getAllHospitals { get; } = "192.168.137.1:49814/api/ClinicHospitals";

        // GET Hospital Detatils Clinic Hospital ID
        public static string global_connURL_getCHDetailsById { get; } = "192.168.137.1:49814/api/ClinicHospitals/"; // + CH id

        // GET Hospital Clinic by Treatment ID
        public static string global_connURL_getClinicHospitalsByTreatmentID { get; } = "192.168.137.1:49814/api/ClinicHospitalsByTreatment/"; // + Treatment ID

        // GET Opening Hours by Hospital Clinic ID
        public static string global_connURL_getOpeningHoursByCHid { get; } = "192.168.137.1:49814/api/OpeningHours/"; // + CH id

        // GET Bookings (isFullyBooked) by Hospital Clinic ID
        public static string global_connURL_getBookingsByCHid { get; } = "192.168.137.1:49814/api/Bookings/"; // + CH id

        // GET Doctor Dentists by Hospital Clinic ID
        public static string global_connURL_getDocDentistsByCHid { get; } = "192.168.137.1:49814/api/DoctorDentistsByH/"; // + CH id

        // GET TimeSlot by Hospital Clinic ID
        public static string global_connURL_getClinicHospitalTimeSlotByCHid { get; } = "192.168.137.1:49814/api/ClinicHospitalTimeSlotByH/"; // + CH id
    }
}
