using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dental_Clinic.Models
{
    public class Post
    {
        [Key]
        public int id { get; set; }
        [DisplayName("Название")]
        public string postName { get; set; } = null!;
        [DisplayName("Зарплата")]
        [DataType(DataType.Currency)]
        public int salary { get; set; }
        [ValidateNever]
        public bool isDeleted { get; set; }

        public List<Doctor> Doctors = new List<Doctor>();
    }
}
