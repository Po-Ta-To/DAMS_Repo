namespace Dental_IT.Model
{
    public class Hospital
    {
        public int ID { get; set; }
        public string HospitalName { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string OpeningHours { get; set; }
        public string Price { get; set; }
        public bool IsOpenMonFri { get; set; }
        public bool IsOpenSat { get; set; }
        public bool IsOpenSunPh { get; set; }
    }
}
