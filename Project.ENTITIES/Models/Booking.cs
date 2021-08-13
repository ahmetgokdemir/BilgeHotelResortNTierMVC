using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Booking : BaseEntity
    {
        public decimal TotalPrice { get; set; }
        //Sipariş işlemlerinin icerisindeki bilgileri daha rahat yakalamak adına actıgımız property'lerdir...
        public string UserName { get; set; } // UserName, Email
        public string Email { get; set; }

        public int CustomerID { get; set; }

        public virtual List<BookingDetail> BookingDetails { get; set; }
        public virtual Customer Customer { get; set; }


    }
}
