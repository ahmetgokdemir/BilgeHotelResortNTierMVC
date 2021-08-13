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
        public string UserName { get; set; }
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Password Must Match")]
        public string RePassword { get; set; }
        public Guid ActivationCode { get; set; }
        public bool Active { get; set; }
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
