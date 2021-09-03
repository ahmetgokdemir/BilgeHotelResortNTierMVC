using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class BookingDetail : BaseEntity
    {
        [Required(ErrorMessage = "Giriş tarihi boş bırakılamaz")]
        public DateTime CheckIn { get; set; }
        [Required(ErrorMessage = "Çıkış tarihi boş bırakılamaz")]
        public DateTime CheckOut { get; set; }
        public decimal SubPrice { get; set; }
        public bool ReservationActive { get; set; }
        public Package BookingType { get; set; }
        public string RoomNo { get; set; } // RoomDetail'den geliyor..


        public int BookingID { get; set; }
        public int RoomID { get; set; }

        public virtual Room Room { get; set; }
        public virtual Booking Booking { get; set; }

        public enum Package
        {
            TamPansiyon = 1, HerseyDahil = 2
        }
    }
}
