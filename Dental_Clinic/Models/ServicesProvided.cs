using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dental_Clinic.Models
{
    public class ServicesProvided
    {
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
        [ValidateNever]
        [DisplayName("Услуги")]
        public MedService MedService { get; set; } = null!;
    }
}
