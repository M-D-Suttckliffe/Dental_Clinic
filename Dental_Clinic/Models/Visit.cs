using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dental_Clinic.Models
{
    public class Visit
    {
        [Key]
        public int id { get; set; }
        [Browsable(false)]
        public int Patientid { get; set; }
        [Browsable(false)]
        public int MedTreatmentid { get; set; }
        [DisplayName("Дата и время визита")]
        [DataType(DataType.DateTime)]
        public DateTime dateVisit { get; set; }

        public Patient Patient { get; set; } = null!;
        public MedTreatment MedTreatment { get; set; } = null!;
    }
}
