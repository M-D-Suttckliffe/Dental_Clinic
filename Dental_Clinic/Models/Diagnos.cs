using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dental_Clinic.Models
{
    public class Diagnos
    {
        [Key]
        public int id { get; set; }
        [DisplayName("Название Заболевания")]
        public string diagnosisName { get; set; } = null!;

        public List<MedTreatment> MedTreatments = new List<MedTreatment>();
    }
}
