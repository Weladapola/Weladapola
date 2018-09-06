
using System.ComponentModel.DataAnnotations;

namespace BTCD_System.Models
{
    public class ProvinceM
    {
        public int ProvinceId { get; set; }
        public int CountryId { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public string ProvincePostalCode { get; set; }
        public string ProvinceTelephoneCode { get; set; }

    }
}
