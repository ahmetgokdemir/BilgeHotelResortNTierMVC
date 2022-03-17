using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Booking : BaseEntity
    {
        public decimal TotalPrice { get; set; } //t-sqlde aggrecate funct. ile stored procudered ile tasarlayıp total price yazdırabilir.C# tarafında bir classın içerisinde ayrı bir property olarak tutulmasının nedeni kolay ulaşım sağlamak içindir.. 

        //Sipariş işlemlerinin icerisindeki bilgileri daha rahat yakalamak adına actıgımız property'lerdir... (AppUser'dan ulaşabilsek bile linq performans kaybettirebileceği için aşağıdaki propertyler eklendi..)
        public string UserName { get; set; } // UserName, Email
        public string Email { get; set; }

        public int CustomerID { get; set; }

        public virtual List<BookingDetail> BookingDetails { get; set; }
        public virtual Customer Customer { get; set; }


    }
}
