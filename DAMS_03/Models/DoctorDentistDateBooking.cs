//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAMS_03.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DoctorDentistDateBooking
    {
        public int ID { get; set; }
        public System.DateTime DateOfBookings { get; set; }
        public int Bookings { get; set; }
        public int DoctorDentistID { get; set; }
    
        public virtual DoctorDentist DoctorDentist { get; set; }
    }
}
