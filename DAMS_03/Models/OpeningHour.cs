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
    
    public partial class OpeningHour
    {
        public int ID { get; set; }
        public int OpeningHoursDay { get; set; }
        public System.TimeSpan TimeRangeStart { get; set; }
        public System.TimeSpan TimeRangeEnd { get; set; }
        public int ClinicHospitalID { get; set; }
    
        public virtual ClinicHospital ClinicHospital { get; set; }
    }
}
