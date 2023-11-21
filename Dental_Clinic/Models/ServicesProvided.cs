using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Dental_Clinic.Models
{
    [DataContract]
    public class ServicesProvided
    {
        [DataMember]
        [Key]
        public int id { get; set; }
        [Browsable(false)]
        [DisplayName("Посещение")]
        public int Visitid { get; set; }
        [Browsable(false)]
        [DisplayName("Услуга")]
        public int MedServiceid { get; set; }
        [ValidateNever]
        public bool isDeleted { get; set; }

        [ValidateNever]
        [DisplayName("Посещения")]
        public Visit Visit { get; set; } = null!;
        [DataMember]
        [ValidateNever]
        [DisplayName("Услуги")]
        public MedService MedService { get; set; } = null!;
    }
}
