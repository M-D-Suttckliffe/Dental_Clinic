using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dental_Clinic.Models
{
    public class ServicesProvided
    {
        [Key]
        public int id { get; set; }
        [Browsable(false)]
        public int Visitid { get; set; }
        [Browsable(false)]
        public int Doctorid { get; set; }
        [Browsable(false)]
        public int MedServiceid { get; set; }

        public Doctor Doctor { get; set; } = null!;
        public Visit Visit { get; set; } = null!;
        public MedService MedService { get; set; } = null!;
    }
}
