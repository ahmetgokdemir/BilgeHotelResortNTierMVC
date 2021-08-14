using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public abstract class UserSpec : BaseEntity
    {
        [Required(ErrorMessage = "{0} alanı bos gecilemez")]
        [Display(Name = "İsim"), MaxLength(15, ErrorMessage = "{0} en fazla {1} karakter olabilir")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "{0} alanı bos gecilemez")]
        [Display(Name = "Soyad"), MaxLength(15, ErrorMessage = "{0} en fazla {1} karakter olabilir")]
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
    }
}
