namespace Dental_IT.Model
{
    class Treatment
    {
        public int ID { get; set; }
        public string TreatmentName { get; set; }
        public string TreatmentDesc { get; set; }
        public string Price { get; set; }
        public string Price_d { get; set; }
        public double PriceLow { get; set; }
        public double PriceHigh { get; set; }
    }
}
