using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class StaffProfile: UserSpec
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string JobTitle { get; set; }
        public decimal Salary { get; set; }
        public DateTime HireDate { get; set; }
        public UserRole Role { get; set; } // MyInit' te Staff olarak set edilecek..

        public int HotelID { get; set; }

        public virtual Hotel Hotel { get; set; }

    }
}
