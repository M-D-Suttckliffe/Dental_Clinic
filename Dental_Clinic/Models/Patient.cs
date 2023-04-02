using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dental_Clinic.Models
{
    public class Patient
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
        [DisplayName("День Рождения")]
        [DataType(DataType.Date)]
        public DateTime birthday { get; set; }
        [DisplayName("Пол")]
        public Int16 gender { get; set; }
        [DisplayName("Телефон")]
        [DataType(DataType.PhoneNumber)]
        public string phoneNumber { get; set; } = null!;
        [DisplayName("Адрес")]
        public string address { get; set; } = null!;

        public List<Visit> Visits { get; set; } = new List<Visit>();
    }
}
