using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dental_Clinic.Models
{
    public class Register
    {
        [Required(ErrorMessage = "Не указан логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        public string surName { get; set; } = null!;

        [Required(ErrorMessage = "Не указано имя")]
        public string name { get; set; } = null!;

        [DisplayName("Отчество")]
        public string middleName { get; set; } = null!;

        [Required(ErrorMessage = "Не указана дата рождения")]
        [DataType(DataType.Date)]
        [DisplayName("День рождения")]
        public DateTime birthday { get; set; }

        //[Required(ErrorMessage = "Не указана должность")]
        //public int Postid { get; set; }

        [Required(ErrorMessage = "Не указан пол")]
        [DisplayName("Пол")]
        public Int16 gender { get; set; }

        [Required(ErrorMessage = "Не указан телефон")]
        [DataType(DataType.PhoneNumber)]
        public string phoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Не указан адрес")]
        [DisplayName("Адрес")]
        public string address { get; set; } = null!;
    }

    public class Login
    {
        [Required(ErrorMessage = "Не указан логин")]
        public string login { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool isRemember { get; set; }
    }
}
