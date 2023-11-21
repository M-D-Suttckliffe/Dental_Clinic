using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Dental_Clinic.Models
{
    [DataContract]
    public class MedService
    {
        [DataMember]
        [Key]
        public int id { get; set; }
        [DataMember]
        [DisplayName("Название")]
        public string name { get; set; } = null!;
        [DataMember]
        [DisplayName("Цена")]
        [DataType(DataType.Currency)]
        public int price { get; set; }
        [ValidateNever]
        public bool isDeleted { get; set; }

        public List<ServicesProvided> ServicesProvideds = new List<ServicesProvided>();
    }
}
