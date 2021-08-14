using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class StaffProfile: UserSpec
    {
        [Required(ErrorMessage = "{0} alanı bos gecilemez")]
        [Display(Name = "İsim"), MaxLength(15, ErrorMessage = "{0} en fazla {1} karakter olabilir")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Şifre boş bırakılamaz")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Meslek boş bırakılamaz")]
        public string JobTitle { get; set; }
        [Required(ErrorMessage = "Maaş boş bırakılamaz")]
        [Range(3000, 20000, ErrorMessage = "Maaş 3000 ile 20000 arasında olmalıdır.")]
        public decimal Salary { get; set; }
        [Required(ErrorMessage = "İşe giriş boş bırakılamaz")]
        public DateTime HireDate { get; set; }
        public UserRole Role { get; set; } // MyInit' te Staff olarak set edilecek..

        public int HotelID { get; set; }

        public virtual Hotel Hotel { get; set; }

    }
}
