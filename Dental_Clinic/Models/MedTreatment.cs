using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Dental_Clinic.Models
{
    [DataContract]
    public class MedTreatment
    {
        [DataMember]
        [Key]
        public int id { get; set; }
        [Browsable(false)]
        [DisplayName("Название болезни")]
        public int Diagnosid { get; set; }
        [DataMember]
        [DisplayName("Название лечения")]
        public string treatmentName { get; set; } = null!;
        [ValidateNever]
        public bool isDeleted { get; set; }

        public List<ListPrepforTreatment> listPrepforTreatments = new List<ListPrepforTreatment>();
        [DisplayName("Посещения")]
        public List<Visit> Visits { get; set; } = new List<Visit>();
        [DisplayName("Болезни")]
        [ValidateNever]
        public Diagnos Diagnos { get; set; } = null!;
    }
}
