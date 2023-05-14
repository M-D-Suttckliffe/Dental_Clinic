using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dental_Clinic.Models
{
    public class MedTreatment
    {
        [Key]
        public int id { get; set; }
        [Browsable(false)]
        [DisplayName("Название болезни")]
        public int Diagnosid { get; set; }
        [DisplayName("Название лечения")]
        public string treatmentName { get; set; } = null!;
        [ValidateNever]
        public bool isDeleted { get; set; }

        public List<ListPrepforTreatment> listPrepforTreatments = new List<ListPrepforTreatment>();
        [DisplayName("Посещения")]
        public List<Visit> Visits { get; set; } = new List<Visit>();
        [DisplayName("Болезни")]
        public Diagnos Diagnos { get; set; } = null!;
    }
}
