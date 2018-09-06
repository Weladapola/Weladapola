namespace BTCD_System.Models
{
    public class AutoIncrementU
    {
        public string TrnID { get; set; }
        public string TrnsactionName { get; set; }
        public string Prefix { get; set; }
        public decimal SerialNo { get; set; }
        public int SerialLength { get; set; }
        public int Mode { get; set; }
    }
}