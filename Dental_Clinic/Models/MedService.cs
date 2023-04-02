using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dental_Clinic.Models
{
    public class MedService
    {
        [Key]
        public int id { get; set; }
        [DisplayName("Название")]
        public string name { get; set; } = null!;
        [DisplayName("Цена")]
        [DataType(DataType.Currency)]
        public int price { get; set; }

        public List<ServicesProvided> ServicesProvideds = new List<ServicesProvided>();
    }
}
