using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        [DisplayName("По рецепту")]
        [UIHint("Boolean")]
        public Boolean recipeCheck { get; set; }
        [ValidateNever]
        public bool isDeleted { get; set; }

        public List<ListPrepforTreatment> ListPrepforTreatments = new List<ListPrepforTreatment>();
    }
}
