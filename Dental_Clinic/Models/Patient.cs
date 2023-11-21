using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Dental_Clinic.Models
{
    [DataContract]
    public class Patient
    {
        [DataMember]
        [Key]
        public int id { get; set; }
        [DisplayName("Фамилия")]
        public string surName { get; set; } = null!;
        [DisplayName("Имя")]
        public string name { get; set; } = null!;
        [DisplayName("Отчество")]
        public string middleName { get; set; } = null!;
        [DataMember]
        [DisplayName("ФИО")]
        public string fullName => $"{surName} {name} {middleName}";
        [DisplayName("День Рождения")]
        [DataType(DataType.Date)]
        public DateTime birthday { get; set; }
        [DisplayName("Пол")]
        public Int16 gender { get; set; }
        [ValidateNever]
        [DisplayName("Пол")]
        public string genderName => gender == 0 ? "Мужской" : gender == 1 ? "Женский": "Не указано";
        [DisplayName("Телефон")]
        [DataType(DataType.PhoneNumber)]
        public string phoneNumber { get; set; } = null!;
        [DisplayName("Адрес")]
        public string address { get; set; } = null!;
        [ValidateNever]
        [DisplayName("Логин")]
        public string login { get; set; } = null!;
        [ValidateNever]
        [DisplayName("Пароль")]
        public string password { get; set; } = null!;
        [ValidateNever]
        public bool isDeleted { get; set; }
        [ValidateNever]
        public List<Visit> Visits { get; set; } = new List<Visit>();
    }
}
