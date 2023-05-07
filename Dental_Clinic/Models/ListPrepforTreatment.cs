using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dental_Clinic.Models
{
    public class ListPrepforTreatment
    {
        [Key]
        public int id { get; set; }
        [Browsable(false)]
        [DisplayName("Лечение")]
        public int MedTreatmentid { get; set; }
        [Browsable(false)]
        [DisplayName("Лекарство")]
        public int Medicationid { get; set; }
        [DisplayName("Кол-во лекарств")]
        public string amountMedications { get; set; } = null!;
        [ValidateNever]
        public bool isDeleted { get; set; }

        [DisplayName("Лекарства")]
        public Medication Medication { get; set; } = null!;
        [DisplayName("Лечения")]
        public MedTreatment MedTreatment { get; set; } = null!;
    }
}
