using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Customer : BaseEntity
    {
        [Required(ErrorMessage = "{0} alanı bos gecilemez")]
        [Display(Name = "Kullanıcı Adı"), MaxLength(15, ErrorMessage = "{0} en fazla {1} karakter olabilir")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Şifre boş bırakılamaz")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password Must Match")]
        public string RePassword { get; set; }
        public Guid ActivationCode { get; set; }
        public bool Active { get; set; }
        [EmailAddress(ErrorMessage = "{0} alanı sadece email formatında adres kabul edebilir")]
        public string Email { get; set; }
        public UserRole Role { get; set; }

        public Customer()
        {
            Role = UserRole.Customer;
            ActivationCode = Guid.NewGuid();
        }

        public virtual CustomerProfile CustomerProfile { get; set; }
        public virtual List<Booking> Bookings { get; set; }

    }
}
