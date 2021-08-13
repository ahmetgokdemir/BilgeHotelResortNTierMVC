using Project.ENTITIES.Models;
using Project.WEBUI.Models.BookingTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.WEBUI.VMClasses
{
    public class CartPageVM
    {
        public Cart Cart { get; set; }
        public BookingDetail BookingDetail { get; set; }
        public CustomerProfile CustomerProfile { get; set; }
    }
}