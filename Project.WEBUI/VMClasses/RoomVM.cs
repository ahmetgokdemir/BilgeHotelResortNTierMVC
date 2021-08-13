using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WEBUI.VMClasses
{
    public class RoomVM
    {
        public List<Room> Rooms { get; set; }
        public Room Room { get; set; }
        public List<RoomDetail> RoomDetails { get; set; }
        public RoomDetail RoomDetail { get; set; }
        public List<BookingDetail> BookingDetails { get; set; }

    }
}