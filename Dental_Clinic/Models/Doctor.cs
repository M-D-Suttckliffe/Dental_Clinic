using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dental_Clinic.Models
{
    public class Doctor
    {
        [Key]
        public int id { get; set; }
        [DisplayName("Фамилия")]
        public string surName { get; set; } = null!;
        [DisplayName("Имя")]
        public string name { get; set; } = null!;
        [DisplayName("Отчество")]
        public string middleName { get; set; } = null!;
        [DisplayName("ФИО")]
        public string fullName => $"{surName} {name} {middleName}";
        [DisplayName("ID Должности")]
        [Browsable(false)]
        public int Postid { get; set; }
        [DisplayName("День Рождения")]
        [DataType(DataType.Date)]
        public DateTime birthday { get; set; }

        public Post Post { get; set; } = null!;
        public List<ServicesProvided> ServicesProvideds { get; set; } = new List<ServicesProvided>();
    }
}
