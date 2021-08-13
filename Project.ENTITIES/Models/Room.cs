using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Room : BaseEntity
    {
        public string RoomType { get; set; }
        public string RoomDescription { get; set; }
        public short RoomCount { get; set; }
        public short RoomAvailable { get; set; }
        public string ImagePath { get; set; }
        public bool availableBalcony { get; set; }
        public bool availableMinibar { get; set; }
        public decimal Price { get; set; }

        public int HotelID { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual List<BookingDetail> BookingDetails { get; set; }
        public virtual List<RoomDetail> RoomDetails { get; set; }


    }
}
