using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Dental_Clinic.Models
{
    [DataContract]
    public class Visit
    {
        [DataMember]
        [Key]
        public int id { get; set; }
        [Browsable(false)]
        [DisplayName("Пациент")]
        public int Patientid { get; set; }
        [Browsable(false)]
        [DisplayName("Лечение")]
        public int MedTreatmentid { get; set; }
        [Browsable(false)]
        [DisplayName("Врач")]
        public int Doctorid { get; set; }
        [DataMember]
        [DisplayName("Дата и время визита")]
        [DataType(DataType.DateTime)]
        public DateTime dateVisit { get; set; }
        [Browsable(false)]
        [ValidateNever]
        public Int16 status { get; set; }
        [DisplayName("Статус")]
        [ValidateNever]
        public string statusName => status == 0 ? "Будет" : status == 1 ? "Прошёл" : "Не указано";
        [ValidateNever]
        public bool isDeleted { get; set; }

        [DataMember]
        [ValidateNever]
        [DisplayName("Пациент")]
        public Patient Patient { get; set; } = null!;
        [DataMember]
        [ValidateNever]
        [DisplayName("Врач")]
        public Doctor Doctor { get; set; } = null!;
        [DataMember]
        [ValidateNever]
        [DisplayName("Лечение")]
        public MedTreatment MedTreatment { get; set; } = null!;
        [DataMember]
        [DisplayName("Список предоставленных услуг")]
        public List<ServicesProvided> servicesProvideds { get; set; } = new List<ServicesProvided>();
    }
}
