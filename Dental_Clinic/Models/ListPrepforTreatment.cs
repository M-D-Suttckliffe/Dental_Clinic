using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dental_Clinic.Models
{
    public class ListPrepforTreatment
    {
        [Key]
        public int id { get; set; }
        [Browsable(false)]
        public int MedTreatmentid { get; set; }
        [Browsable(false)]
        public int Medicationid { get; set; }
        [DisplayName("Кол-во лекарств")]
        public string amountMedications { get; set; } = null!;

        public Medication Medication { get; set; } = null!;
        public MedTreatment MedTreatment { get; set; } = null!;
    }
}
