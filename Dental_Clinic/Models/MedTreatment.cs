using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dental_Clinic.Models
{
    public class MedTreatment
    {
        [Key]
        public int id { get; set; }
        [Browsable(false)]
        public int Diagnosid { get; set; }
        [DisplayName("Название болезни")]
        public string treatmentName { get; set; } = null!;

        public List<ListPrepforTreatment> ListPrepforTreatments = new List<ListPrepforTreatment>();
        public List<Visit> Visits { get; set; } = new List<Visit>();
        public Diagnos Diagnos { get; set; } = null!;
    }
}
