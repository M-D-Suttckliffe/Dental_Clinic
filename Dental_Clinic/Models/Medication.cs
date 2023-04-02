using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dental_Clinic.Models
{
    public class Medication
    {
        [Key]
        public int id { get; set; }
        [DisplayName("Название Лекарства")]
        public string name { get; set; } = null!;
        [DisplayName("Цена")]
        [UIHint("Boolean")]
        public Boolean recipeCheck { get; set; }

        public List<ListPrepforTreatment> ListPrepforTreatments = new List<ListPrepforTreatment>();
    }
}
