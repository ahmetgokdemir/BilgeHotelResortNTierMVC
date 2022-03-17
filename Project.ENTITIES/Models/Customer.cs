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
        public string Password { get; set; } // hashlenecek
        [Compare("Password", ErrorMessage = "Password Must Match")]
        public string RePassword { get; set; }
        public Guid ActivationCode { get; set; } // Guid unique bir hash dir.. Bilgisayarın MAC adresi, Sistem tarihi, Markası vs her biri kendi içlerine bir algoritma girerek haslanerek 36/40 karakterli bir şifre çıkartır.. Her kullanıcaya özgü bir kod verir..
        public bool Active { get; set; }
        [Required(ErrorMessage = "{0} alanı bos gecilemez")]
        [EmailAddress(ErrorMessage = "{0} alanı sadece email formatında adres kabul edebilir")]
        public string Email { get; set; }
        public UserRole Role { get; set; }

        public Customer()
        {
            Role = UserRole.Customer;
            ActivationCode = Guid.NewGuid();
        }

        /*
            Sınıfınız Global alanda eğer sizin değerini atamadığınız bir yapı barındırıyorsa referans(string) tipinde davranan default olarak null ilkel olan tiplerde(int,bool) 0 veya olumsuzluk(false) alır.. Guid de bir değer (ilkel) tipidir, referans tipi değildir.. 00000 ... gibi bir değer alır..
        */

        public virtual CustomerProfile CustomerProfile { get; set; }
        public virtual List<Booking> Bookings { get; set; }

    }
}
